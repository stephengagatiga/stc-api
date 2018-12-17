using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STC.API.Data;
using STC.API.Entities.ProductEntity;
using STC.API.Entities.GroupEntity;
using STC.API.Models;
using Microsoft.EntityFrameworkCore;
using STC.API.Models.Group;

namespace STC.API.Services
{
    public class SqlGroupData : IGroupData
    {
        private STCDbContext _context;

        public SqlGroupData(STCDbContext context)
        {
            _context = context;
        }

        public Group AddGroup(string groupName)
        {
            if (_context.Groups.FirstOrDefault(item => item.Name == groupName) == null)
            {
                Group group = new Group {Name = groupName, Active = true };
                _context.Groups.Add(group);
                _context.Entry(group).State = EntityState.Added;
                _context.SaveChanges();
                return group;
            }
            return null;
        }

        public void ChangGroupActivState(Group group, bool active)
        {
            group.Active = active;
            _context.Groups.Update(group);
            _context.Entry(group).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void EditGroupName(Group group, string groupName)
        {
            group.Name = groupName;
            _context.Groups.Update(group);
            _context.Entry(group).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public ICollection<Group> GetAllGroups()
        {
            var groups = _context.Groups
                                .OrderBy(g => g.Name);
            return groups.ToList();
        }

        public Group GetGroup(int groupId)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Id == groupId);
            return group;
        }

        public ICollection<Group> GetGroups()
        {
            var groups = _context.Groups
                                .Where(g => g.Active == true)
                                .OrderBy(g => g.Name);
            return groups.ToList();
        }
    }
}
