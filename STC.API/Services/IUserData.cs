using STC.API.Entities.UserEntity;
using STC.API.Entities.UserRoleEntity;
using STC.API.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public interface IUserData
    {
        User GetUser(string objectId);
        User GetUser(int userId);
        ICollection<User> GetUsers(int[] userIds);
        User AddUser(NewUserDto newUserDto);
        User GetUserInfo(int userId);
        User GetUserInfo(string objectId);
        User GetUser(UpdateUserDto updateUser, int userId);
        User GetUserWithPermission(string objectId);
        void UpdateUser(UpdateUserDto updateUser, User user);
        void EnableUser(User user, bool isEnable);
        User CheckUserIfExistByEmailOrObjectId(string email, string objectId);
        ICollection<User> GetUsers();
        ICollection<User> GetUsersInThisRole(int roleId);
        ICollection<User> GetUsersInThisRoles(ICollection<UserRole> userRoles);
        ICollection<User> GetAllUsers();
        ICollection<User> GetAllUsersWithPermissions();
        ICollection<UserPermission> GetAllUsersThisPermission(Permission permission);
        void SaveChanges();
    }
}
