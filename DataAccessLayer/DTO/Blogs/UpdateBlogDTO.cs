using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Blogs
{
    public class UpdateBlogDTO
    {
        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;
        public string? ReasonForDecline { get; set; }



        public string Status { get; set; } = null!;

        public bool IsDeleted { get; set; }
        public int? ModifiedByUserId { get; set; }


        //        public string? Image { get; set; }
    }
}
