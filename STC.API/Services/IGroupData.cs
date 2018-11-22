using STC.API.Entities.ProductEntity;
using STC.API.Entities.GroupEntity;
using STC.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STC.API.Models.Group;

namespace STC.API.Services
{
    public interface IGroupData
    {
        ICollection<Group> GetGroups();
        Group GetGroup(int groupId);
        Group GetGroupAllData(int groupId);
        Group AddGroup(string groupName);
        void UpdateGroup(Group group, GroupUpdateDto groupUpdateDto);
        void DeleteGroup(Group group);
        void DeleteMember(GroupMember groupMember);
        void AddMember(GroupMemberNewDto groupMemberNewDto, int groupId);
        GroupMember GetGroupMember(int groupId, int groupMemberId);
    }
}
