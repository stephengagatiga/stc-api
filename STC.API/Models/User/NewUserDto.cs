using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.User
{
    public class NewUserDto
    {
        [Required]
        public string ObjectId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int? RoleId { get; set; }
        public int? GroupId { get; set; }
        public int? SupervisorId { get; set; }

    }
}
