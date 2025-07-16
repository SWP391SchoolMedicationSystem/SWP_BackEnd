using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Form
{
    public class FormDTO
    {
        public int FormId { get; set; }
        public int ParentId { get; set; }
        public string ParentName { get; set; } = null!;
        public int? Studentid { get; set; }
        public string? StudentName { get; set; } = null!;

        public int FormCategoryId { get; set; }
        public string FormCategoryName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Reason { get; set; } = null!;
        public string? Originalfilename { get; set; } = null!;
        public string? Storedpath { get; set; } = null!;

        public int? Staffid { get; set; }
        public string? StaffName { get; set; } = null!;

        public bool? Isaccepted { get; set; }

        public string? Reasonfordecline { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string ModifiedBy { get; set; } = null!;
    }

    public class CreateFormDTO
    {
        public int ParentId { get; set; }
        public int StudentID { get; set; }
        public string Title { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public IFormFile? DocumentFile { get; set; }
        public string CreatedBy { get; set; } = null!;

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
        public string ModifiedBy { get; set; } = null!;

    }

    public class ResponseFormDTO
    {

        public int FormId { get; set; }

        public int? Staffid { get; set; }
        public string? Reasonfordecline { get; set; }
        public string Modifiedby { get; set; }
    }

    public class FileUploadResult
    {
        public string? OriginalFileName { get; set; }
        public string? StoredFileName { get; set; }
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
