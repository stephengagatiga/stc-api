﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Group
{
    public class GroupNewDto
    {
        [Required]
        public string Name { get; set; }
    }
}
