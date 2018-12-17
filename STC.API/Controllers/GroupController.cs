using STC.API.Entities.GroupEntity;
using STC.API.Models;
using STC.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STC.API.Models.Group;
using STC.API.Models.Utils;

namespace STC.API.Controllers
{
    [Route("groups")]
    public class GroupController : Controller
    {
        private IGroupData _groupData;
        
        public GroupController(IGroupData groupData)
        {
            _groupData = groupData;
        }

        [HttpPost]
        public IActionResult AddGroup([FromBody] GroupNewDto newGroup)
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

        [HttpGet]
        public IActionResult GetGroups()
        {
            var groups = _groupData.GetGroups();
            return Ok(groups);
        }

        [HttpGet("all")]
        public IActionResult GetAllGroups()
        {
            var groups = _groupData.GetAllGroups();
            return Ok(groups);
        }

        [HttpGet("{groupId}")]
        public IActionResult GetGroup(int groupId)
        {
            var group = _groupData.GetGroup(groupId);
            return Ok(group);
        }

        [HttpPost("{groupId}")]
        public IActionResult EditGroupname([FromBody] GroupUpdateDto groupUpdateDto, int groupId)
        {
            var group = _groupData.GetGroup(groupId);
            if (group == null)
            {
                return NotFound();
            }
            _groupData.EditGroupName(group, groupUpdateDto.Name);
            return NoContent();
        }

        [HttpPost("{groupId}/active")]
        public IActionResult ChangeGroupActiveState([FromBody] ActiveState activeState, int groupId)
        {
            var group = _groupData.GetGroup(groupId);
            if (group == null)
            {
                return NotFound();
            }
            _groupData.ChangGroupActivState(group, activeState.Active);
            return NoContent();
        }


    }
}
