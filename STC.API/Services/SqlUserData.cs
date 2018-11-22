using Microsoft.EntityFrameworkCore;
using STC.API.Data;
using STC.API.Entities.Users;
using STC.API.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public class SqlUserData : IUserData
    {
        private STCDbContext _context;

        public SqlUserData(STCDbContext context)
        {
            _context = context;
        }

        public UserRole AddRole(NewRole newRole)
        {
            try
            {
                var role = new UserRole() { Name = newRole.Name };
                _context.UserRoles.Add(role);
                _context.SaveChanges();
                return role;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User AddUser(NewUserDto newUserDto)
        {
            try
            {
                User user = new User{
                    ObjectId = newUserDto.ObjectId,
                    FirstName = newUserDto.FirstName,
                    LastName = newUserDto.LastName,
                    Email = newUserDto.Email,
                    RoleId = newUserDto.RoleId,
                    GroupId = newUserDto.GroupId,
                    SupervisorId = newUserDto.SupervisorId,
                    Active = true,
                    CreatedOn = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time"))
                };

                _context.Users.Add(user);
                _context.Entry(user).State = EntityState.Added;
                return user;
            } catch (Exception ex)
            {
                return null;
            }
        }

        public User GetUser(string objectId)
        {
            var user = _context.Users.FirstOrDefault(u => u.ObjectId == objectId);
            return user;
        }

        public User CheckUserIfExistByEmailOrObjectId(string email, string objectId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email || u.ObjectId == objectId);
            return user;
        }

        public User GetUserById(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            return user;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public ICollection<User> GetUsers()
        {
            var users = _context.Users.Where(u => u.Active == true).ToList();
            return users;
        }
    }
}
