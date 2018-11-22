using STC.API.Models;
using STC.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using STC.API.Models.User;
using STC.API.Entities.Users;

namespace STC.API.Controllers
{

    [Route("users")]
    public class UsersController : Controller
    {
        private IUserData _userData;

        public UsersController(IUserData userData)
        {
            _userData = userData;
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

        [HttpPost("role")]
        public IActionResult AddRole([FromBody] NewRole newRole)
        {
            if (ModelState.IsValid)
            {
                var role = _userData.AddRole(newRole);
                if (role != null)
                {
                    return Ok(role);
                }
                return StatusCode(500);
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
    }
}
