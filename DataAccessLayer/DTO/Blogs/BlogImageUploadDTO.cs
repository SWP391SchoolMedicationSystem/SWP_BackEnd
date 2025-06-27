using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DataAccessLayer.DTO.Blogs
{
    public class BlogImageUploadDTO
    {
        public int BlogId { get; set; }
        public IFormFile ImageFile { get; set; } = null!;
    }
}
