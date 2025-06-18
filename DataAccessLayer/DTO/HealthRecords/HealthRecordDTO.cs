namespace DataAccessLayer.DTO.HealthRecords
{
    public class HealthRecordDTO
    {
        public int StudentID { get; set; }
        public int HealthCategoryID { get; set; }
        public DateTime HealthRecordDate { get; set; }
        public string Healthrecordtitle { get; set; }
        public string Healthrecorddescription { get; set; }
        public int Staffid { get; set; }
        public bool IsConfirm { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
    }
}
