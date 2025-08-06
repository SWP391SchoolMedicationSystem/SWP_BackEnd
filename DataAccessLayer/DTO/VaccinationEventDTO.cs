using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTO
{
    public class VaccinationEventDTO
    {
        public int VaccinationEventId { get; set; }
        public string VaccinationEventName { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string OrganizedBy { get; set; } = null!;
        public DateTime EventDate { get; set; }
        public string Description { get; set; } = null!;
        public string? DocumentFileName { get; set; }
        public string? DocumentAccessToken { get; set; }
        public string? DownloadUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string ModifiedBy { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public bool IsEnded { get; set; } = false;
        public int TotalStudents { get; set; }
        public int ConfirmedCount { get; set; }
        public int DeclinedCount { get; set; }
        public int PendingCount { get; set; }
    }



    public class CreateVaccinationEventDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập tên sự kiện tiêm chủng")]
        [StringLength(255, ErrorMessage = "Tên sự kiện không quá 255 ký tự")]
        public string VaccinationEventName { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập địa điểm tiêm chủng")]
        [StringLength(255, ErrorMessage = "Location cannot exceed 255 characters")]
        public string Location { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập tên người tổ chức")]
        [StringLength(255, ErrorMessage = "Organized by cannot exceed 255 characters")]
        public string OrganizedBy { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập ngày tiêm chủng")]
        [DataType(DataType.DateTime, ErrorMessage = "Ngày tiêm chủng không hợp lệ")]
        public DateTime EventDate { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string Description { get; set; } = null!;

        public IFormFile? DocumentFile { get; set; }
    }

    public class UpdateVaccinationEventDTO
    {
        [Required(ErrorMessage = "Vui lòng chọn sự kiện tiêm chủng")]
        public int VaccinationEventId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sự kiện tiêm chủng")]
        [StringLength(255, ErrorMessage = "Tên sự kiện không được quá 255 ký tự")]
        public string VaccinationEventName { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập địa điểm tiêm chủng")]
        [StringLength(255, ErrorMessage = "Địa điểm không quá 255 ký tự")]
        public string Location { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập tên người tổ chức")]
        [StringLength(255, ErrorMessage = "Tên người tổ chức không được quá 255 ký tự")]
        public string OrganizedBy { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập ngày tiêm chủng")]
        [DataType(DataType.DateTime, ErrorMessage = "Ngày tiêm chủng không hợp lệ")]
        public DateTime EventDate { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string Description { get; set; } = null!;

        public IFormFile? DocumentFile { get; set; }
        public bool DocumentDelete { get; set; } = false;
    }

    public class ParentVaccinationResponseDTO
    {
        [Required(ErrorMessage = "Vui lòng chọn phụ huynh")]
        public int ParentId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn sự kiện tiêm chủng")]
        public int VaccinationEventId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn tham gia hay không")]
        public bool ParentConsent { get; set; }

        [Required(ErrorMessage = "Vui lòng ghi lí do")]
        public List<StudentVaccinationResponseDTO> Responses { get; set; } = new();
    }

    public class StudentVaccinationResponseDTO
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public bool WillAttend { get; set; }

        public string? ReasonForDecline { get; set; }
    }

    public class VaccinationEventSummaryDTO
    {
        public int VaccinationEventId { get; set; }
        public string VaccinationEventName { get; set; } = null!;
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = null!;
        public int TotalStudents { get; set; }
        public int ConfirmedCount { get; set; }
        public int DeclinedCount { get; set; }
        public int PendingCount { get; set; }
        public double ConfirmationRate { get; set; }
        public List<StudentVaccinationStatusDTO> StudentResponses { get; set; } = new();
    }

    public class StudentVaccinationStatusDTO
    {
        public int StudentId { get; set; }
        public int ParentId { get; set; }
        public string StudentName { get; set; } = null!;
        public string ParentName { get; set; } = null!;
        public string ParentEmail { get; set; } = null!;
        public string ClassName { get; set; } = null!;
        public bool? WillAttend { get; set; }
        public string? ReasonForDecline { get; set; }
        public DateTime? ResponseDate { get; set; }
        public string Status { get; set; } = null!; // "Confirmed", "Declined", "Pending"
    }

    public class SendVaccinationEmailDTO
    {

        [Required(ErrorMessage = "Vui lòng chọn sự kiện tiêm chủng")]
        public int VaccinationEventId { get; set; }

        [Required(ErrorMessage = "Email template ID is required")]
        public int EmailTemplateId { get; set; }

        public string? CustomMessage { get; set; }
    }

    public class SendVaccineEmailListDTO
    {
        [Required(ErrorMessage = "Vui lòng chọn sự kiện tiêm chủng")]
        public List<int> Ids { get; set; } = new();

        [Required(ErrorMessage = "Vui lòng chọn sự kiện tiêm chủng")]
        public SendVaccinationEmailDTO sendVaccinationEmailDTO { get; set; } = new();
    }
} 