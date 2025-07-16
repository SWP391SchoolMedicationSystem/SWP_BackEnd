using BussinessLayer.IService;
using BussinessLayer.Utils;
using DataAccessLayer.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace SchoolMedicalSystem.Controllers
{
    [Route("api/files")]
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

            // 1. Get the correct MIME type for the file.
            var contentType = GetMimeTypeForFile(eventRecord.DocumentFileName);

            // 2. Create the Content-Disposition header.
            var contentDisposition = new ContentDisposition
            {
                // Set the friendly file name for the user
                FileName = $"KeHoach_{eventRecord.VaccinationEventName.Replace(" ", "_")}{Path.GetExtension(eventRecord.DocumentFileName)}"
            };

            // 3. Decide whether to show inline or as an attachment.
            if (contentType == "application/pdf" || contentType.StartsWith("image/"))
            {
                // For PDFs and images, tell the browser to display them inline.
                contentDisposition.Inline = true;
            }
            else
            {
                // For all other types (DOCX, XLSX), force a download.
                contentDisposition.Inline = false;
            }

            // 4. Add the header to the response.
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            // 5. Return the File with the correct Content-Type.
            // We no longer pass the filename here, as the header handles it.
            return File(memoryStream, contentType);
        }

        // Helper method to determine the MIME type
        private string GetMimeTypeForFile(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                _ => "application/octet-stream"
            };
        }
    }
}
