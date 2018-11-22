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

        public void AddMember(GroupMemberNewDto groupMemberNewDto, int groupId)
        {
            GroupMember member = new GroupMember { GroupId = groupId, UserId = groupMemberNewDto.UserId, Role = groupMemberNewDto.Role };
            _context.GroupMembers.Add(member);
            _context.Entry(member).State = EntityState.Added;
            _context.SaveChanges();
        }

        public Group AddGroup(string groupName)
        {
            if (_context.Groups.FirstOrDefault(item => item.Name == groupName) == null)
            {
                Group group = new Group {Name = groupName };
                _context.Groups.Add(group);
                _context.Entry(group).State = EntityState.Added;
                _context.SaveChanges();
                return group;
            }
            return null;
        }

        public void DeleteMember(GroupMember groupMember)
        {
            _context.GroupMembers.Remove(groupMember);
            _context.Entry(groupMember).State = EntityState.Deleted;
            SaveChanges();
        }

        public void DeleteGroup(Group group)
        {
            _context.Groups.Remove(group);
            SaveChanges();
        }

        public ICollection<Group> GetGroups()
        {
            var groups = _context.Groups.ToList();
            return groups;
        }

        public Group GetGroup(int groupId)
        {
            var group = _context.Groups
                    .FirstOrDefault(item => item.Id == groupId);
            return group;
        }

        public Group GetGroupAllData(int groupId)
        {
            var groupWithRelatedData = _context.Groups
                                .Where(item => item.Id == groupId)
                                .Include(item => item.Members)
                                .ThenInclude(u => u.User)
                                .FirstOrDefault();
            return groupWithRelatedData;
        }

        public void UpdateGroup(Group group, GroupUpdateDto groupUpdateDto)
        {
            group.Name = groupUpdateDto.Name;
            _context.Entry(group).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public GroupMember GetGroupMember(int groupId, int groupMemberId)
        {
            var groupMember = _context.GroupMembers.FirstOrDefault(g => g.GroupId == groupId && g.UserId == groupMemberId);
            return groupMember;
        }

    

        private void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
