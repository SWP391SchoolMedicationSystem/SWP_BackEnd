using BussinessLayer.IService;
using DataAccessLayer.DTO;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IClassRoomService _classroomservice;
        private readonly IParentService _parentservice;
        private readonly IStudentService _studentService;
        public StudentController(IClassRoomService classRoomService, IParentService parentservice, IStudentService studentService)
        {
            _classroomservice = classRoomService;
            _parentservice = parentservice;
            _studentService = studentService;
        }

        [HttpPost("student")]
        public async Task<IActionResult> UploadStudent(List<InsertStudent> studentlist)
        {
            if (studentlist != null)
            {
                await _studentService.UploadStudentList(studentlist);
            }
            return Ok("Students uploaded successfully.");
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
            return Ok(student);
        }

        [HttpDelete("DeleteStudent/{id}")]
        public IActionResult DeleteStudent(int id)
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
    }

    public class UpdateStudent
    {
        public int Id { get; set; }
        public StudentDTO? Student { get; set; }
    }
}
