﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.ComponentEntity
{
    public class ComponentType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public bool Active { get; set; }
    }
}
