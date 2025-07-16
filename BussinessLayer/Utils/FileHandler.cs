using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO.Form;


namespace BussinessLayer.Utils
{
    public class FileHandler
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _protectedFilesFolderPath;
        private const int maxFileSize = 15 * 1024 * 1024; // 15 MB

        public FileHandler(IWebHostEnvironment env)
        {
            _env = env;
            // Define the secure folder path ONCE in the constructor
            _protectedFilesFolderPath = Path.Combine(_env.WebRootPath, "protected_file");
        }

        public async Task<FileUploadResult> UploadAsync(IFormFile formFile)
        {
            // --- 1. Validation ---
            var allowedExtensions = new List<string> { ".pdf", ".docx", ".xlsx", ".png", ".jpg" };
            string extension = Path.GetExtension(formFile.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                return new FileUploadResult { Success = false, ErrorMessage = $"File extension '{extension}' is not supported." };
            }

            if (formFile.Length > (maxFileSize))
            {
                return new FileUploadResult { Success = false, ErrorMessage = "File is too large. Maximum size is 15MB." };
            }

            try
            {
                // --- 2. Save the file ---
                // Ensure the target directory exists
                if (!Directory.Exists(_protectedFilesFolderPath))
                {
                    Directory.CreateDirectory(_protectedFilesFolderPath);
                }

                // Generate a unique file name
                string storedFileName = Guid.NewGuid().ToString() + extension;

                // Use Path.Combine for security and correctness
                string filePath = Path.Combine(_protectedFilesFolderPath, storedFileName);

                // Use async stream operations in a web server
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                // --- 3. Return a successful result ---
                return new FileUploadResult
                {
                    Success = true,
                    StoredFileName = storedFileName,
                    OriginalFileName = formFile.FileName
                };
            }
            catch (Exception ex)
            {
                // Log the exception (ex.ToString()) with your logging framework
                return new FileUploadResult { Success = false, ErrorMessage = "An unexpected error occurred while saving the file." };
            }
        }

        public void Delete(string storedFileName)
        {
            // Prevent trying to delete a null or empty file name
            if (string.IsNullOrEmpty(storedFileName))
            {
                return;
            }

            var filePath = Path.Combine(_protectedFilesFolderPath, storedFileName);

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                // Log the error (e.g., using ILogger)
                // It's often better to log and continue than to crash the whole update process
                // if a file can't be deleted due to a permission issue.
                Console.WriteLine($"Error deleting file {filePath}: {ex.Message}");
            }
        }
    }
}
