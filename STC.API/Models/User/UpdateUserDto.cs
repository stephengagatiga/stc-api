using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.User
{
    public class UpdateUserDto
    {

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int SupervisorId { get; set; }
    }
}
