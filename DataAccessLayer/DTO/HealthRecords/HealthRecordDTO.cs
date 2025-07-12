namespace DataAccessLayer.DTO.HealthRecords
{
    public class HealthRecordDto
    {
        public int healthrecordid { get; set; }

        public int studentid { get; set; }
        public string StudentName { get; set; } = null!;

        public int healthcategoryid { get; set; }
        public string HealthCategoryName { get; set; } = null!;

        public DateTime healthrecorddate { get; set; }

        public string healthrecordtitle { get; set; } = null!;

        public string? healthrecorddescription { get; set; }

        public int staffID { get; set; }
        public string staffName { get; set; } = null!;

        public int parentID { get; set; }
        public string ParentName { get; set; } = null!;

        public string Status { get; set; } = null!;

        public bool Isdeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedByUserId { get; set; }

        public string? CreatedByUserName { get; set; } = null!;

        public int? ModifiedByUserId { get; set; }

        public string? ModifiedByUserName { get; set; } = null!;
    }
}
