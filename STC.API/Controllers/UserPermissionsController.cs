using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STC.API.Entities.UserEntity;
using STC.API.Models.UserPermission;
using STC.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Controllers
{
    [Route("userpermissions")]

    public class UserPermissionsController : Controller
    {
        private IUserPermissionData _userPermissionData;
        private IUserData _userData;

        public UserPermissionsController(IUserPermissionData userPermissionData, IUserData userData)
        {
            _userPermissionData = userPermissionData;
            _userData = userData;
        }

        [HttpPost]
        public IActionResult GetPermission([FromBody] GetUserPermissionDto userPermissionDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid request");
            }

            var result = _userPermissionData.GetPermission(userPermissionDto.UserId, userPermissionDto.Permission);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "No permission found");
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult UpdateUserPermission([FromBody] UpdateUserPermissionDto updateUserPermissionDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid request");
            }

            var user = _userData.GetUser(updateUserPermissionDto.UserId);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "User does not exist");
            }

            _userPermissionData.UpdateUserPermission(user, updateUserPermissionDto);

            return Ok();
        }
    }
}
