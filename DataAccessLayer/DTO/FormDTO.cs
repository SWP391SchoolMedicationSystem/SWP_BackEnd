using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO
{
    public class FormDTO
    {
        public int FormId { get; set; }
        public int ParentId { get; set; }
        public int FormCategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public string? OriginalFileName { get; set; } = null!;
        public string? StoredPath { get; set; } = null!;

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string ModifiedBy { get; set; } = null!;
        public bool IsAccepted { get; set; }
    }

    public class CreateFormDTO
    {
        public int ParentId { get; set; }
        public int FormCategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public IFormFile? formFile { get; set; }
    }

    public class UpdateFormDTO
    {
        public int FormId { get; set; }
        public int ParentId { get; set; }
        public int FormCategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public string? OriginalFileName { get; set; } = null!;
        public string? StoredPath { get; set; } = null!;
    }

    public class ResponseFormDTO
    {
        public int FormId { get; set; }
        public int StaffId { get; set; }
        public string Reason { get; set; } = null!;
        public bool IsAccepted { get; set; } = false;
        public string ParentName { get; set; } = null!;
        public string ParentEmail { get; set; } = null!;
    }

    public class FileUploadResult
    {
        public string? OriginalFileName { get; set; }
        public string? StoredFileName { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
