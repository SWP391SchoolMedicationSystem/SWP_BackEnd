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
        public Task<string> UploadStudent(IFormFile file)
        {
            try
            {
                var list = _studentService.ProcessExcelFile(file);
                
                return _studentService.UploadStudentList(list.Item1);
            }
            catch (Exception ex) {}
            return null;
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
        public async Task<IActionResult> AddStudent([FromBody] StudentDTO student)
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
        public IActionResult DeleteStudent([FromBody] int id)
        {
            _studentService.DeleteStudent(id);
            return Ok($"Student with ID {id} deleted successfully.");
        }

        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudent student)
        {
            if (student == null || student.Student == null)
                return BadRequest("Student data cannot be null.");
            var s = await _studentService.UpdateStudentAsync(student.Student, student.Id);
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
