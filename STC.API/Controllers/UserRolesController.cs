using Microsoft.AspNetCore.Mvc;
using STC.API.Models.UserRole;
using STC.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Controllers
{
    [Route("userroles")]
    public class UserRolesController : Controller
    {
        private IUserRoleData _userRole;

        public UserRolesController(IUserRoleData userRole)
        {
            _userRole = userRole;
        }

        [HttpGet("all")]
        public IActionResult GetAllUserRoles()
        {
            var roles = _userRole.GetAllUserRoles();

            return Ok(roles);
        }

        [HttpGet]
        public IActionResult GetUserRoles()
        {
            var roles = _userRole.GetUserRoles();

            return Ok(roles);
        }

        [HttpPost]
        public IActionResult AddUserRole([FromBody] NewUserRole newUserRole)
        {
            if (ModelState.IsValid)
            {
                if (_userRole.GetRole(newUserRole.Name) == null)
                {
                    var role = _userRole.AddRole(newUserRole);
                    return Ok(role);
                }
                return StatusCode(400, "User role already exist!");
            }

            return BadRequest();
        }

        [HttpPost("{userId}")]
        public IActionResult UpdateUserRole([FromBody] UpdateUserRole updateUserRole, int userId)
        {
            if (ModelState.IsValid)
            {
                var userRole = _userRole.GetRole(userId);
                if (userRole != null)
                {
                    _userRole.UpdateUserRole(userRole, updateUserRole);
                    return NoContent();
                }
                return NotFound();
            }

            return BadRequest();
        }

    }
}
