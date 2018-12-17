using STC.API.Models;
using STC.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STC.API.Models.Product;
using STC.API.Models.Utils;

namespace STC.API.Controllers
{
    [Route("principals")]
    public class ProductsController : Controller
    {
        private IProductData _productData;
        private IGroupData _groupData;

        public ProductsController(IProductData productData, IGroupData groupData)
        {
            _productData = productData;
            _groupData = groupData;
        }

        [HttpGet]
        public IActionResult GetPrincipals()
        {
            return Ok(_productData.GetPrincipals());
        }

        [HttpGet("all")]
        public IActionResult GetAllPrincipals()
        {
            return Ok(_productData.GetAllPrincipals());
        }

        [HttpPost]
        public IActionResult AddPrincipal([FromBody] PrincipalNewDto principalNewDto)
        {
            if (ModelState.IsValid)
            {
                var principal = _productData.GetPrincipalByName(principalNewDto.Name);
                if (principal != null)
                {
                    return StatusCode(400, "Principal already exist!");
                }

                if (principalNewDto.GroupId != null)
                {
                    var group = _groupData.GetGroup(principalNewDto.GroupId ?? 0);
                    if (group == null)
                    {
                        return StatusCode(404, "Group does not exist!");
                    }
                }

                var newPrincipal = _productData.AddPrincipal(principalNewDto.Name, principalNewDto.GroupId);
                return Ok(newPrincipal);
            }
            return BadRequest();
        }

        [HttpPost("{principalId}/products")]
        public IActionResult AddProduct([FromBody] ProductNewDto productNewDto, int principalId)
        {
            if (ModelState.IsValid)
            {
                var principal = _productData.GetPrincipalById(principalId);

                if (principal == null)
                {
                    return StatusCode(400, "Principal not found");
                }

                var product = _productData.AddProduct(principalId, productNewDto.Name);
                if (product == null)
                {
                    return StatusCode(400, "Product already exist!");
                }
                return Ok(product);
            }
            return BadRequest();
        }

        [HttpPost("{principalId}/products/{productId}/active")]
        public IActionResult ChangeProductActiveState([FromBody] ActiveState activeState, int principalId, int productId)
        {
            var product = _productData.GetProduct(principalId, productId);
            if (product == null)
            {
                return StatusCode(400, "Principal or product not found!");
            }

            _productData.ChangeProductActiveState(product, activeState.Active);
            return NoContent();
        }

        [HttpPost("{principalId}/activate")]
        public IActionResult ChangePrincipalActiveState([FromBody] ActiveState activeState, int principalId)
        {
            var principal = _productData.GetPrincipalById(principalId);
            if (principal == null)
            {
                return StatusCode(400, "Principal not found!");
            }
            _productData.ChangePrincipalActiveState(principal, activeState.Active);
            return NoContent();
        }

        [HttpPost("{principalId}")]
        public IActionResult EditPrincipal(int principalId, [FromBody] PrincipalEditDto principalEditDto)
        {
            if (ModelState.IsValid)
            {
                var principal = _productData.GetPrincipalById(principalId);
                if (principal == null)
                {
                    return StatusCode(400, "Principal not found!");
                }

                if (principalEditDto.GroupId != null)
                {
                    var group = _groupData.GetGroup(principalEditDto.GroupId ?? 0);
                    if (group == null)
                    {
                        return StatusCode(404, "Group does not exist!");
                    }
                }

                _productData.EditPrincipal(principal, principalEditDto);
                return NoContent();
            }
            return BadRequest();
        }

        [HttpGet("{principalId}")]
        public IActionResult GetPrincipal(int principalId)
        {
            var principal = _productData.GetPrincipalByIdWithProducts(principalId);
            if (principal == null)
            {
                return StatusCode(400, "Principal not found!");
            }
            return Ok(principal);
        }

        [HttpPost("{principalId}/products/{productId}")]
        public IActionResult EditProduct(int principalId, int productId, [FromBody] ProductEditDto productEditDto)
        {
            if (ModelState.IsValid)
            {
                var product = _productData.GetProduct(principalId, productId);
                if (product == null)
                {
                    return StatusCode(400, "Principal not found!");
                }
                _productData.EditProduct(product, productEditDto);
                return NoContent();
            }
            return BadRequest();
        }
    }
}
