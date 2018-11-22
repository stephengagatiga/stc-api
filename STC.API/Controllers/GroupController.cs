using STC.API.Entities.GroupEntity;
using STC.API.Models;
using STC.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STC.API.Models.Group;

namespace STC.API.Controllers
{
    [Route("groups")]
    public class GroupController : Controller
    {
        private IGroupData _groupData;
        private IProductData _productData;
        private IUserData _userData;

        public GroupController(IGroupData groupData, IProductData productData, IUserData userData)
        {
            _groupData = groupData;
            _productData = productData;
            _userData = userData;
        }

        [HttpGet]
        public IActionResult GetGroups()
        {
            return Ok(_groupData.GetGroups());
        }

        [HttpGet("{id}")]
        public IActionResult GetGroup(int id)
        {
            var technology = _groupData.GetGroupAllData(id);
            if (technology == null)
            {
                return StatusCode(404, "Group not found!");
            }
            return Ok(technology);
        }

        [HttpPost]
        public IActionResult AddGroup([FromBody] GroupNew newGroup)
        {
            if (ModelState.IsValid)
            {
                var group = _groupData.AddGroup(newGroup.Name);
                if (group == null)
                {
                    return StatusCode(400, "Group already exist!");
                }
                return Ok(group);
            }
            return BadRequest();
        }

        [HttpPost("{id}")]
        public IActionResult UpdateGroup([FromBody] GroupUpdateDto groupUpdateDto, int id)
        {
            if (ModelState.IsValid)
            {
                var group = _groupData.GetGroup(id);
                if (group == null)
                {
                    return StatusCode(404, "Group not found!");
                }
                _groupData.UpdateGroup(group, groupUpdateDto);
                return StatusCode(204);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGroup(int id)
        {

            var group = _groupData.GetGroup(id);
            if (group == null)
            {
                return StatusCode(404, "Group not found!");
            }
            _groupData.DeleteGroup(group);
            return NoContent();
        }

        [HttpPost("{groupId}/members")]
        public IActionResult AddGroupMember([FromBody] GroupMemberNewDto groupMemberNewDto, int groupId)
        {
            if (ModelState.IsValid)
            {
                var group = _groupData.GetGroup(groupId);
                if (group == null)
                {
                    return StatusCode(404, "Group not found!");
                }

                var user = _userData.GetUserById(groupMemberNewDto.UserId);
                if (user == null)
                {
                    return StatusCode(404, "User not found!");
                }

                var checkMember = _groupData.GetGroupMember(groupId, groupMemberNewDto.UserId);
                if (checkMember != null)
                {
                    return StatusCode(400, "Member already existed!");
                }

                _groupData.AddMember(groupMemberNewDto, groupId);
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{groupId}/members/{userId}")]
        public IActionResult DeleteGroupMember(int groupId, int userId)
        {
            var group = _groupData.GetGroupAllData(groupId);
            if (group == null)
            {
                return NotFound();
            }

            var groupMember = _groupData.GetGroupMember(groupId, userId);
            if (groupMember == null)
            {
                return NotFound();
            }

            _groupData.DeleteMember(groupMember);
            return NoContent();
        }

    }
}
