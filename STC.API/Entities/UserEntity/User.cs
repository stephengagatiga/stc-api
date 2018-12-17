using Microsoft.AspNetCore.Identity;
using STC.API.Entities.GroupEntity;
using STC.API.Entities.ProductAssignmentEntity;
using STC.API.Entities.UserRoleEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Entities.UserEntity
{
    public class User 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ObjectId { get; set; }

        [Required]
        [MaxLength(150)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(150)]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public int? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public UserRole Role { get; set; }

        public int? SupervisorId { get; set; }
        public User Supervisor { get; set; }

        public ICollection<ProductAssignment> Products { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
