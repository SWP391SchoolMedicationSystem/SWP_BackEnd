﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTO.SpecialNeedCategory
{
    public class UpdateSpecialNeedCategoryDTO
    {
        public int SpecialNeedCategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? Description { get; set; }
        public bool IsDelete { get; set; }
    }
}
