using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.Account
{
    public class AccountContactEditDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public string Designation { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(100)]
        public string ContactDetails { get; set; }

        [Required]
        public bool PrimaryContact { get; set; }

        public bool Active { get; set; }
    }
}
