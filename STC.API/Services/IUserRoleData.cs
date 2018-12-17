using STC.API.Entities.UserRoleEntity;
using STC.API.Models.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public interface IUserRoleData
    {
        ICollection<UserRole> GetAllUserRoles();
        ICollection<UserRole> GetUserRoles();
        UserRole AddRole(NewUserRole newUserRole);
        UserRole GetRole(string name);
        UserRole GetRole(int roleId);
        void UpdateUserRole(UserRole userRole, UpdateUserRole updateUserRole);
    }
}
