﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.POEntity
{
    public class POGuidStatusAttachment
    {
        [Key]
        public int Id { get; set; }
        public int POGuidStatusId { get; set; }
        [ForeignKey("POGuidStatusId")]
        public POGuidStatus POGuidStatus { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public byte[] File { get; set; }
        public long Size { get; set; }
        public string ContentType { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
