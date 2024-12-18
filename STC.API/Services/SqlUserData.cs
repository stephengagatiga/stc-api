using Microsoft.EntityFrameworkCore;
using STC.API.Data;
using STC.API.Entities.UserEntity;
using STC.API.Entities.UserRoleEntity;
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

        public User AddUser(NewUserDto newUserDto)
        {
            try
            {
                User user = new User {
                    ObjectId = newUserDto.ObjectId,
                    FirstName = newUserDto.FirstName,
                    LastName = newUserDto.LastName,
                    Email = newUserDto.Email,
                    RoleId = newUserDto.RoleId,
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

        public User GetUserInfo(int userId)
        {
            var user = _context.Users
                                    .Where(u => u.Id == userId)
                                    .Include(u => u.Role)
                                    .Include(u => u.Supervisor)
                                    .FirstOrDefault();
            var userProducts = _context.ProductAssignments
                                                    .Where(pa => pa.UserId == userId)
                                                    .Include(pa => pa.Product)
                                                    .ToList();
            user.Products = userProducts;

            return user;
        }

        public User GetUserInfo(string objectId)
        {
            var user = _context.Users
                                    .Where(u => u.ObjectId == objectId)
                                    .Include(u => u.Role)
                                    .Include(u => u.Supervisor)
                                    .FirstOrDefault();

            return user;
        }

        public User GetUser(UpdateUserDto updateUser, int userId)
        {
            var user = _context.Users
                                    .Where(u => u.Id == userId)
                                    .FirstOrDefault();
            return user;
        }

        public void UpdateUser(UpdateUserDto updateUser, User user)
        {
            user.RoleId = updateUser.RoleId;
            user.SupervisorId = updateUser.SupervisorId;
            user.Active = updateUser.Active;
            _context.Users.Update(user);
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void EnableUser(User user, bool isEnable)
        {
            user.Active = isEnable;
            _context.Users.Update(user);
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public ICollection<User> GetUsers()
        {
            var users = _context.Users
                                    .Where(u => u.Active == true)
                                    .Include(u => u.Role)
                                    .OrderBy(u => u.FirstName)
                                    .ToList();
            return users;
        }

        public ICollection<User> GetAllUsers()
        {
            var users = _context.Users
                                    .OrderBy(u => u.FirstName)
                                    .Include(u => u.Role)
                                    .ToList();
            return users;
        }

        public User GetUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            return user;
        }

        public ICollection<User> GetUsersInThisRole(int roleId)
        {
            var users = _context.Users.Where(u => u.RoleId == roleId && u.Active == true)
                                        .ToList();
            return users;
        }

        public ICollection<User> GetUsersInThisRoles(ICollection<UserRole> userRoles)
        {
            var users = _context.Users
                                    .Where(u => userRoles.Any(ur => ur.Id == u.RoleId))
                                    .ToList();
            return users;
        }

        public ICollection<User> GetUsers(int[] userIds)
        {
            List<User> users = new List<User>();
            for (int i = 0; i < userIds.Length; i++)
            {
                users.Add( _context.Users.FirstOrDefault(u => u.Id == userIds[i]));
            }
            return users;
        }

        public ICollection<User> GetAllUsersWithPermissions()
        {
            return _context.Users
                            .Select(u => new User{ Id = u.Id, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email,UserPermissions = u.UserPermissions })
                            .Include(u => u.UserPermissions)
                            .OrderBy(u => u.FirstName)
                            .ToList();
        }

        public ICollection<UserPermission> GetAllUsersThisPermission(Permission permission)
        {
            return _context.UserPermissions
                        .Where(p => p.Permission == permission)
                        .Include(p => p.User)
                        .ToList();

        }

        public User GetUserWithPermission(string objectId)
        {
            return _context.Users
                            .Include(u => u.UserPermissions)
                            .FirstOrDefault(u => u.ObjectId == objectId);
        }
    }
}
