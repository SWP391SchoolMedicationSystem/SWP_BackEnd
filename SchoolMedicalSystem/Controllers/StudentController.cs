using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using DataAccessLayer.Repository;
using DataAccessLayer.IRepository;
using DataAccessLayer.DTO.Students;
using Microsoft.AspNetCore.Authorization;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IClassRoomService _classroomservice;
        private readonly IParentService _parentservice;
        private readonly IStudentService _studentService;
        private readonly IStudentRepo _studentRepo;
        public StudentController(IStudentRepo studentrepo,
            IClassRoomService classRoomService, IParentService parentservice, IStudentService studentService)
        {
            _classroomservice = classRoomService;
            _parentservice = parentservice;
            _studentService = studentService;
            _studentRepo = studentrepo;

        }

        [HttpPost("student")]
        public async Task<IActionResult> UploadStudent(IFormFile file)
        {
            try
            {
                var list = _studentService.ProcessExcelFile(file);

                var result = await _studentService.UploadStudentList(list.Item1);
                return Ok(new
                {
                    result,
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding Student {ex.Message}");

            }
        }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("GetStudentById/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            return Ok(student);
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentDTO student)
        {
            if (student == null)
            {
                return BadRequest("Student data cannot be null.");
            }
            await _studentService.AddStudentAsync(student);
            _studentRepo.Save();
            return Ok(student);
        }

        [HttpDelete("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent([FromBody] int id)
        {
            await _studentService.DeleteStudent(id);
            return Ok($"Student with ID {id} deleted successfully.");
        }

        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentDTo student)
        {
            if (student == null)
                return BadRequest("Student data cannot be null.");
            var s = await _studentService.UpdateStudentAsync(student);
            if (s == null)
                return NotFound($"Student with ID {student.Id} not found.");
            return Ok(s);
        }

        [HttpGet("GetStudentByParentId/{parentId}")]
        public async Task<IActionResult> GetStudentByParentId(int parentId)
        {
            var students = await _studentService.GetStudentByParentId(parentId);
            if (students == null || !students.Any())
            {
                return NotFound($"No students found for parent ID {parentId}.");
            }
            return Ok(students);
        }
    }

    public class UpdateStudent
    {
        public int Id { get; set; }
        public StudentDTO? Student { get; set; }
    }
}
