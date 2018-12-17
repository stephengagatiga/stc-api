using Microsoft.AspNetCore.Mvc;
using STC.API.Models.ProductAssignment;
using STC.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Controllers
{
    [Route("productassignment")]
    public class ProductAssignmentsController : Controller
    {
        private IProductAssignmentData _productAssignemtData;
        private IProductData _productData;
        private IUserData _userData;

        public ProductAssignmentsController(IProductAssignmentData productAssignmentData, IProductData productData, IUserData userData)
        {
            _productAssignemtData = productAssignmentData;
            _productData = productData;
            _userData = userData;
        }

        [HttpPost]
        public IActionResult AssignProduct([FromBody] NewProductAssignmentDto newProductAssignment)
        {
            var product = _productData.GetProduct(newProductAssignment.ProductId);
            var user = _userData.GetUser(newProductAssignment.UserId);

            if (product == null)
            {
                return StatusCode(404, "Product doesn't exist!");
            } else if (user == null)
            {
                return StatusCode(404, "User doesn't exist!");
            }

            var productAssignment = _productAssignemtData.AssignProduct(product, user);
            return Ok(productAssignment);
        }

        [HttpGet]
        public IActionResult GetAllUserProductAssignment()
        {
            var users = _productAssignemtData.GetAllUserProductAssignment();
            return Ok(users);
        }

    }
}
