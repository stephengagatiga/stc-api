using STC.API.Entities.UserEntity;
using STC.API.Models.UserPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public interface IUserPermissionData
    {
        UserPermission GetPermission(int userId, Permission permission);
        UserPermission AddPermission(User user, Permission permission);
        void RemovePermission(UserPermission userPermission);
        void UpdateUserPermission(User user, UpdateUserPermissionDto updateUserPermissionDto);
    }
}
