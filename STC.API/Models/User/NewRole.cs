﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.User
{
    public class NewRole
    {
        [Required]
        public string Name { get; set; }
    }
}
