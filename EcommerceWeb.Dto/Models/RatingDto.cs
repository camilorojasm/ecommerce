﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWeb.Dto.Models
{
    public class RatingDto
    {
        public Guid AUserAUserId { get; set; }
        public Guid ProductProductId { get; set; }
        public int Star { get; set; }
        public string Comment { get; set; }
    }
}
