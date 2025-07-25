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
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string ModifiedBy { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public int TotalStudents { get; set; }
        public int ConfirmedCount { get; set; }
        public int DeclinedCount { get; set; }
        public int PendingCount { get; set; }
    }

    public class CreateVaccinationEventDTO
    {
        [Required(ErrorMessage = "Vaccination event name is required")]
        [StringLength(255, ErrorMessage = "Event name cannot exceed 255 characters")]
        public string VaccinationEventName { get; set; } = null!;

        [Required(ErrorMessage = "Location is required")]
        [StringLength(255, ErrorMessage = "Location cannot exceed 255 characters")]
        public string Location { get; set; } = null!;

        [Required(ErrorMessage = "Organized by is required")]
        [StringLength(255, ErrorMessage = "Organized by cannot exceed 255 characters")]
        public string OrganizedBy { get; set; } = null!;

        [Required(ErrorMessage = "Event date is required")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = null!;

        public IFormFile? DocumentFile { get; set; }
    }

    public class UpdateVaccinationEventDTO
    {
        [Required(ErrorMessage = "Vaccination event ID is required")]
        public int VaccinationEventId { get; set; }

        [Required(ErrorMessage = "Vaccination event name is required")]
        [StringLength(255, ErrorMessage = "Event name cannot exceed 255 characters")]
        public string VaccinationEventName { get; set; } = null!;

        [Required(ErrorMessage = "Location is required")]
        [StringLength(255, ErrorMessage = "Location cannot exceed 255 characters")]
        public string Location { get; set; } = null!;

        [Required(ErrorMessage = "Organized by is required")]
        [StringLength(255, ErrorMessage = "Organized by cannot exceed 255 characters")]
        public string OrganizedBy { get; set; } = null!;

        [Required(ErrorMessage = "Event date is required")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = null!;

        public IFormFile? DocumentFile { get; set; }
        public bool DocumentDelete { get; set; } = false;
    }

    public class ParentVaccinationResponseDTO
    {
        [Required(ErrorMessage = "Parent ID is required")]
        public int ParentId { get; set; }

        [Required(ErrorMessage = "Vaccination event ID is required")]
        public int VaccinationEventId { get; set; }

        [Required(ErrorMessage = "Parent consent is required")]
        public bool ParentConsent { get; set; }

        [Required(ErrorMessage = "Responses are required")]
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

        [Required(ErrorMessage = "Vaccination event ID is required")]
        public int VaccinationEventId { get; set; }

        [Required(ErrorMessage = "Email template ID is required")]
        public int EmailTemplateId { get; set; }

        public string? CustomMessage { get; set; }
    }

    public class SendVaccineEmailListDTO
    {
        [Required(ErrorMessage = "Vaccination event ID is required")]
        public List<int> Ids { get; set; } = new();

        [Required(ErrorMessage = "Vaccination event ID is required")]
        public SendVaccinationEmailDTO sendVaccinationEmailDTO { get; set; } = new();
    }
} 