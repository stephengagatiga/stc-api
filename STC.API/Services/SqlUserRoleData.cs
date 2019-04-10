using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using STC.API.Data;
using STC.API.Entities.UserRoleEntity;
using STC.API.Models.UserRole;

namespace STC.API.Services
{
    public class SqlUserRoleData : IUserRoleData
    {
        private STCDbContext _context;

        public SqlUserRoleData(STCDbContext context)
        {
            _context = context;
        }

        public UserRole AddRole(NewUserRole newUserRole)
        {
            var userRole = new UserRole()
            {
                Name = newUserRole.Name,
                Active = true
            };

            _context.UserRoles.Add(userRole);
            _context.Entry(userRole).State = EntityState.Added;
            _context.SaveChanges();
            return userRole;
        }

        public ICollection<UserRole> GetAllUserRoles()
        {
            var roles = _context.UserRoles.OrderBy(r => r.Name).ToList();

            return roles;
        }

        public UserRole GetRole(string name)
        {
            var role = _context.UserRoles.FirstOrDefault(r => r.Name.ToUpper() == name.ToUpper());
            return role;
        }

        public UserRole GetRole(int roleId)
        {
            var role = _context.UserRoles.FirstOrDefault(r => r.Id == roleId);
            return role;
        }

        public ICollection<UserRole> GetRolesByName(string[] roleNames)
        {
            var roles = _context.UserRoles
                                        .Where(u => roleNames.Any(rn => rn.ToUpper().Trim() == u.Name.ToUpper().Trim()) && u.Active == true)
                                        .ToList();
            return roles;
        }

        public ICollection<UserRole> GetUserRoles()
        {
            var roles = _context.UserRoles
                                    .Where(r => r.Active == true)
                                    .OrderBy(r => r.Name).ToList();
            return roles;
        }

        public void UpdateUserRole(UserRole userRole, UpdateUserRole updateUserRole)
        {
            userRole.Name = updateUserRole.Name;
            userRole.Active = updateUserRole.Active;
            _context.UserRoles.Update(userRole);
            _context.Entry(userRole).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
