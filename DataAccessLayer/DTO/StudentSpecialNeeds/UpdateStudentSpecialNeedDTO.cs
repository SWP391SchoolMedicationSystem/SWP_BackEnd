using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entity;

namespace DataAccessLayer.DTO.StudentSpecialNeeds
{
    public class UpdateStudentSpecialNeedDTO
    {
        public int StudentSpecialNeedId { get; set; }
        public int StudentId { get; set; }

        public int SpecialNeedCategoryId { get; set; }

        public string? Notes { get; set; }
    }
}
