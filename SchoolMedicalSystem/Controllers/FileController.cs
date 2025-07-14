using BussinessLayer.IService;
using BussinessLayer.Utils;
using DataAccessLayer.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IVaccinationEventService _vaccinationEventService;

        public FileController(IWebHostEnvironment env, IVaccinationEventService vaccinationEventService)
        {
            _env = env;
            _vaccinationEventService = vaccinationEventService;
        }

        [HttpGet("download/{accessToken}")]
        public async Task<IActionResult> DownloadEventDocument(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return BadRequest("Access token is missing.");
            }

            // 1. Find the event record using the secure access token.
            // The index we created on this column makes this query very fast.
            var eventRecord = await _vaccinationEventService.GetEventByAccessToken(accessToken);

            // 2. Security Check: If no record is found, the token is invalid or expired.
            if (eventRecord == null || string.IsNullOrEmpty(eventRecord.DocumentFileName))
            {
                return NotFound("The requested file does not exist or your link is invalid.");
            }

            // 3. Construct the full, physical path to the file on the server.
            var protectedFolderPath = Path.Combine(_env.WebRootPath, "protected_file");
            var filePath = Path.Combine(protectedFolderPath, eventRecord.DocumentFileName);

            // 4. Verify that the file physically exists on the disk.
            if (!System.IO.File.Exists(filePath))
            {
                // This is an integrity issue. The database has a record, but the file is gone.
                // You should log this as a critical error.
                return NotFound("File could not be found on the server.");
            }

            // 5. Read the file into memory and return it to the user.
            var memoryStream = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0; // Reset the stream position to the beginning.

            // Determine a user-friendly file name for the download prompt.
            var fileExtension = Path.GetExtension(eventRecord.DocumentFileName);
            var friendlyFileName = $"KeHoach_{eventRecord.VaccinationEventName.Replace(" ", "_")}{fileExtension}";

            // Return the file. "application/octet-stream" is a generic content type for any file download.
            return File(memoryStream, "application/octet-stream", friendlyFileName);
        }
    }
}
