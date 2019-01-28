using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlMatches;
using Newtonsoft.Json.Linq;
using STC.API.Entities.UserEntity;
using STC.API.Models.Internal;
using STC.API.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace STC.API.Controllers
{
    [Route("internal")]
    public class InternalController : Controller
    {
        private IUserRoleData _userRoleData;
        private IUserData _userData;
        private IProductData _productData;
        private IProductAssignmentData _productAssignmentData;
        private IUtils _utils;

        public InternalController(IUserRoleData userRoleData, IUserData userData, IProductData productData, IProductAssignmentData productAssignmentData, IUtils utils)
        {
            _userRoleData = userRoleData;
            _userData = userData;
            _productData = productData;
            _productAssignmentData = productAssignmentData;
            _utils = utils;
        }
   
        [AllowAnonymous]
        [HttpPost("valuematchinregex")]
        public IActionResult CheckValueInMatchInRegex([FromBody] SubjectRegex regex)
        {

            Match match = Regex.Match(regex.Subject, $@"{regex.Regex}", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("userinthisproducts")]
        public IActionResult GeUserInThisProducts([FromBody] UserInThisProductsDto userInThisProducts)
        {
            var role = _userRoleData.GetRole(userInThisProducts.Role);
            if (role == null)
            {
                return Ok(new { Message = "No Role Found", Users = new List<User>() });
            }

            var users = _userData.GetUsersInThisRole(role.Id);
            if (users.Count == 0)
            {
                return Ok(new { Message = "No User Found", Users = new List<User>() });
            }

            var products = _productData.GetAllProductsWithThisName(userInThisProducts.Products);
            if (products.Count == 0)
            {
                return Ok(new { Message = "No Product Found", Users = new List<User>() });
            }

            var productAssignments = _productAssignmentData.GetAllUserInThisProducts(products);

            if (productAssignments.Count == 0)
            {
                return Ok(new {
                    Message = "No User Assigned",
                    Users = new List<User>()
                    });
            }

            var filteredUsers = users.Where(u => u.Products != null);

            return Ok(new
            {
                Message = "Success",
                Users = filteredUsers
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("test")]
        public async Task<string> ReadStringDataManual()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var content = await reader.ReadToEndAsync();
                Console.WriteLine(content);
                return content;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("testsend")]
        public IActionResult TestSend()
        {
            _utils.TestSend();
            return Ok();
        }
    }

}
