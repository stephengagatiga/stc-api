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
        Group AddGroup(string groupName);
        ICollection<Group> GetGroups();
        ICollection<Group> GetAllGroups();
        Group GetGroup(int groupId);
        void EditGroupName(Group group, string groupName);
        void ChangGroupActivState(Group group, bool active);
    }
}
