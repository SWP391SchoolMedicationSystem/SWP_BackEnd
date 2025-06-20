﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.Blogs
{
    public class CreateBlogDTO
    {
        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public int CreatedBy { get; set; } //FE return 

        public string? Image { get; set; }
    }
}
