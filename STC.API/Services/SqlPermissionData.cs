using STC.API.Data;
using STC.API.Entities.UserEntity;
using STC.API.Models.UserPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public class SqlPermissionData : IUserPermissionData
    {
        private STCDbContext _context;

        public SqlPermissionData(STCDbContext context)
        {
            _context = context;
        }

        public UserPermission AddPermission(User user, Permission permission)
        {
            var p = new UserPermission()
            {
                User = user,
                Permission = permission
            };

            _context.UserPermissions.Add(p);
            _context.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            _context.SaveChanges();

            return p;

        }

        public UserPermission GetPermission(int userId, Permission permission)
        {
           return _context.UserPermissions.FirstOrDefault(u => u.UserId == userId && u.Permission == permission);
        }

        public void RemovePermission(UserPermission userPermission)
        {
            _context.UserPermissions.Remove(userPermission);
            _context.Entry(userPermission).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            _context.SaveChanges();
        }

        public void UpdateUserPermission(User user, UpdateUserPermissionDto updateUserPermissionDto)
        {
            foreach(var p in updateUserPermissionDto.Permissions)
            {
                var up = _context.UserPermissions.FirstOrDefault(u => u.UserId == user.Id && u.Permission == p.Permission);
                //1 means add
                if (p.value == 1)
                {
                    if (up == null)
                    {
                        var nup = new UserPermission()
                        {
                            User = user,
                            Permission = p.Permission
                        };

                        _context.UserPermissions.Add(nup);
                        _context.Entry(nup).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                    }    
                } else if (p.value == 0)
                {
                    if (up != null)
                    {
                        _context.UserPermissions.Remove(up);
                        _context.Entry(up).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    }
                }
            }

            _context.SaveChanges();
        }
    }
}
