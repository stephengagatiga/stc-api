using STC.API.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.UserPermission
{
    public class UpdateUserPermissionDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public ICollection<Permissions> Permissions { get; set; }
    }

    public class Permissions
    {
        [Required]
        public Permission Permission { get; set; }
        [Required]
        public int value { get; set; }
    }
}
