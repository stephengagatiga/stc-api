using STC.API.Entities.UserEntity;
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
        User AddUser(NewUserDto newUserDto);
        User GetUserInfo(int userId);
        User GetUser(UpdateUserDto updateUser, int userId);
        void UpdateUser(UpdateUserDto updateUser, User user);
        User CheckUserIfExistByEmailOrObjectId(string email, string objectId);
        ICollection<User> GetUsers();
        ICollection<User> GetUsersInThisRole(int roleId);
        ICollection<User> GetAllUsers();

        void SaveChanges();
    }
}
