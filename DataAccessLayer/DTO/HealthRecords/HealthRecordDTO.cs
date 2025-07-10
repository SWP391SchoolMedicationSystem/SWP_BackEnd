namespace DataAccessLayer.DTO.HealthRecords
{
    public class HealthRecordDto
    {
        public int HealthRecordId { get; set; }

        public int StudentId { get; set; }

        public int HealthCategoryId { get; set; }

        public DateTime HealthRecordDate { get; set; }

        public string HealthRecordTitle { get; set; } = null!;

        public string? HealthRecordDescription { get; set; }

        public int StaffId { get; set; }

        public string Status { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedByUserId { get; set; }

        public int? ModifiedByUserId { get; set; }
    }
}
