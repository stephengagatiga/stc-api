using STC.API.Entities.Users;
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
        User AddUser(NewUserDto newUserDto);
        User GetUserById(int userId);
        User CheckUserIfExistByEmailOrObjectId(string email, string objectId);
        ICollection<User> GetUsers();

        UserRole AddRole(NewRole newRole);
        void SaveChanges();
    }
}
