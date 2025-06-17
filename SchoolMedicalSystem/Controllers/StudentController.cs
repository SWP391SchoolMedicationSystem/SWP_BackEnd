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

        //[HttpPost("student")]
        //public async Task<IActionResult> UploadStudent(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return BadRequest("Student data is null.");
        //    if (Path.GetExtension(file.FileName).ToLower() != ".xlsx")
        //    {
        //        return BadRequest("Invalid file format. Please upload an .xlsx file.");
        //    }
        //    try
        //    {
        //        var studentlist = new List<StudentDTO>();
        //        var stream = new MemoryStream();
        //        await file.CopyToAsync(stream);
        //        var package = new ExcelPackage(stream);
        //        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
        //        int rowCount = worksheet.Dimension.Rows;
        //        for (int row = 2; row < rowCount; row++) {
        //            {
        //                StudentDTO student = new()
        //                {
        //                    Fullname = worksheet.Cells[row, 1].Text.Trim(),
        //                     = worksheet.Cells[row, 2].Text.Trim(),    
        //                }
        //            }
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest($"Error processing file: {e.Message}");
        //    }
        //}


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
            if (student == null)
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
        public StudentDTO Student { get; set; }
    }
}
