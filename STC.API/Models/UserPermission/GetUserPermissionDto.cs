using STC.API.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Models.UserPermission
{
    public class GetUserPermissionDto
    {
        public int UserId { get; set; }
        public Permission Permission { get; set; }
    }
}
