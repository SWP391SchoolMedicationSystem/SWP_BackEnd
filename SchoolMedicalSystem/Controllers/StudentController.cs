using BussinessLayer.IService;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;

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
    }
}
