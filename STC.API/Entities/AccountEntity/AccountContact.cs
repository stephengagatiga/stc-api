using Microsoft.AspNetCore.Identity;
using STC.API.Entities.UserRoleEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.AccountEntity
{
    public class AccountContact 
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public string Designation { get; set; }

        [Required]
        public string Email { get; set; }

        [MaxLength(100)]
        public string ContactDetails { get; set; }

        [Required]
        public bool PrimaryContact { get; set; }

        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}