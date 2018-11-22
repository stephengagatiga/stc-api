using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Group
{
    public class GroupMemberNewDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Role { get; set; }
    }
}
