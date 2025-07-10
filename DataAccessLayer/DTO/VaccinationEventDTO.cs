using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTO
{
    public class VaccinationEventDTO
    {
    public int EventId { get; set; }

    public string EventName { get; set; } = null!;

    public string? Organizer { get; set; }

    public DateTime EventDate { get; set; }

    public string Location { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedAt { get; set; }

        public int TotalStudents { get; set; }
        public int ConfirmedCount { get; set; }
        public int DeclinedCount { get; set; }
        public int PendingCount { get; set; }
    }

    public class CreateVaccinationEventDTO
    {
        [Required(ErrorMessage = "Vaccination event name is required")]
        [StringLength(255, ErrorMessage = "Event name cannot exceed 255 characters")]
        public string EventName { get; set; } = null!;

        [Required(ErrorMessage = "Location is required")]
        [StringLength(255, ErrorMessage = "Location cannot exceed 255 characters")]
        public string Location { get; set; } = null!;

        [Required(ErrorMessage = "Organized by is required")]
        [StringLength(255, ErrorMessage = "Organized by cannot exceed 255 characters")]
        public string? Organizer { get; set; }

        [Required(ErrorMessage = "Event date is required")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        public int? CreatedByUserId { get; set; }
    }

    public class UpdateVaccinationEventDTO
    {
        [Required(ErrorMessage = "Vaccination event ID is required")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Vaccination event name is required")]
        [StringLength(255, ErrorMessage = "Event name cannot exceed 255 characters")]
        public string EventName { get; set; } = null!;

        [Required(ErrorMessage = "Location is required")]
        [StringLength(255, ErrorMessage = "Location cannot exceed 255 characters")]
        public string Location { get; set; } = null!;

        [Required(ErrorMessage = "Organized by is required")]
        [StringLength(255, ErrorMessage = "Organized by cannot exceed 255 characters")]
        public string? Organizer { get; set; }

        [Required(ErrorMessage = "Event date is required")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        public int? ModifiedByUserId { get; set; }      
    }

    public class ParentVaccinationResponseDTO
    {
        [Required(ErrorMessage = "Parent ID is required")]
        public int ParentId { get; set; }

        [Required(ErrorMessage = "Vaccination event ID is required")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Parent consent is required")]
        public string ParentalConsentStatus { get; set; } = null!;

        [Required(ErrorMessage = "Responses are required")]
        public List<StudentVaccinationResponseDTO> Responses { get; set; } = new();

        public int ModifiedByUserId { get; set; }
    }

    public class StudentVaccinationResponseDTO
    {
        [Required]
        public int StudentId { get; set; }

        public string ParentalConsentStatus { get; set; } = null!;

        [Required]
        public DateTime? ConsentResponseDate { get; set; }

        public string? ReasonForDecline { get; set; }
    }

    public class VaccinationEventSummaryDTO
    {
        public int EventId { get; set; }

        public string EventName { get; set; } = null!;

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
        public string? ParentalConsentStatus { get; set; }
        public string? ReasonForDecline { get; set; }
        public DateTime? ConsentResponseDate { get; set; }
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
        public List<int> ParentIds { get; set; } = new();

        [Required(ErrorMessage = "Vaccination event ID is required")]
        public SendVaccinationEmailDTO sendVaccinationEmailDTO { get; set; } = new();
    }
} 