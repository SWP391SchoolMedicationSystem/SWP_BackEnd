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
    }

    public class ParentVaccinationResponseDTO
    {
        [Required(ErrorMessage = "Parent ID is required")]
        public int ParentId { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Vaccination event ID is required")]
        public int VaccinationEventId { get; set; }

        [Required(ErrorMessage = "Response is required")]
        public bool WillAttend { get; set; }

        public string? ReasonForDecline { get; set; }

        [Required(ErrorMessage = "Parent consent is required")]
        public bool ParentConsent { get; set; }
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

    public class EmailReplyDTO
    {
        [Required(ErrorMessage = "From email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string FromEmail { get; set; } = null!;

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; } = null!;

        [Required(ErrorMessage = "Body is required")]
        public string Body { get; set; } = null!;
    }
} 