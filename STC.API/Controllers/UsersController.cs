using STC.API.Models;
using STC.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using STC.API.Models.User;
using STC.API.Entities.UserEntity;

namespace STC.API.Controllers
{

    [Route("users")]
    public class UsersController : Controller
    {
        private IUserData _userData;
        private IUserRoleData _userRoleData;

        public UsersController(IUserData userData, IUserRoleData userRoleData)
        {
            _userData = userData;
            _userRoleData = userRoleData;
        }


        [HttpPost("check")]
        public IActionResult CheckUser([FromBody] NewUserDto newUserDto)
        {
            if (ModelState.IsValid)
            {
                var user = _userData.GetUser(newUserDto.ObjectId);
                if (user != null) 
                {
                    return Ok(user);
                }

                user = _userData.AddUser(newUserDto);
                if (user != null)
                {
                    return Ok(user);
                } else
                {
                    return StatusCode(500);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] NewUserDto newUserDto)
        {
            if (ModelState.IsValid)
            {
                var isUserExist = _userData.CheckUserIfExistByEmailOrObjectId(newUserDto.Email, newUserDto.ObjectId);
                if (isUserExist != null)
                {
                    return BadRequest("Email/ObjectId is already taken");
                }

                var user = _userData.AddUser(newUserDto);
                if (user != null)
                {
                    _userData.SaveChanges();
                    return Ok(user);
                } else
                {
                    return StatusCode(500);
                }
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("batch")]
        public IActionResult UploadUsers([FromBody] BulkNewUserDto bulkNewUserDto)
        {
            if (ModelState.IsValid)
            {
                bulkNewUserDto.Users.ForEach(user =>
                {
                    _userData.AddUser(user);
                });
                _userData.SaveChanges();

                return Ok();
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userData.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            int intId = 0;
            User user = null;
            if (Int32.TryParse(id, out intId))
            {
                user = _userData.GetUserInfo(intId);
            } else
            {
                user = _userData.GetUserInfo(id);
            }
            return Ok(user);
        }

        [HttpPost("getbyroles")]
        public IActionResult GetUserInRole([FromBody] UserInRoleDto userInRoleDto)
        {
            if (ModelState.IsValid)
            {
                var roles = _userRoleData.GetRolesByName(userInRoleDto.Roles);
                var users = _userData.GetUsersInThisRoles(roles);
                return Ok(users);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            var users = _userData.GetAllUsers();
            return Ok(users);
        }

        [HttpPost("{userId}")]
        public IActionResult UpdateUser([FromBody] UpdateUserDto updateUser, int userId)
        {
            if (ModelState.IsValid)
            {
                var user = _userData.GetUser(updateUser, userId);
                _userData.UpdateUser(updateUser, user);
                return NoContent();
            }
            return BadRequest();
        }
    }
}
