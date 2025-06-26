namespace DataAccessLayer.DTO
{
    public class HealthRecordDto
    {
        public int StudentID { get; set; }
        public int HealthCategoryID { get; set; }
        public DateTime HealthRecordDate { get; set; }
        public string Healthrecordtitle { get; set; } = null!;
        public string Healthrecorddescription { get; set; } = null!;
        public int Staffid { get; set; }
        public bool IsConfirm { get; set; }
        public string? CreatedBy { get; set; } 
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
    }
}
