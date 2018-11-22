using STC.API.Models;
using STC.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STC.API.Models.Product;

namespace STC.API.Controllers
{
    [Route("principals")]
    public class ProductsController : Controller
    {
        private IProductData _productData;

        public ProductsController(IProductData productData)
        {
            _productData = productData;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productData.GetPrincipals());
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
                var newPrincipal = _productData.AddPrincipal(principalNewDto.Name);
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

        [HttpDelete("{principalId}/products/{productId}")]
        public IActionResult DeleteProduct(int principalId, int productId)
        {

            var product = _productData.GetProduct(principalId, productId);
            if (product == null)
            {
                return StatusCode(400, "Principal or product not found!");
            }

            _productData.DeleteProduct(product);
            return NoContent();
        }

        [HttpDelete("{principalId}")]
        public IActionResult DeletePrincipal(int principalId)
        {
            var principal = _productData.GetPrincipalById(principalId);
            if (principal == null)
            {
                return StatusCode(400, "Principal not found!");
            }
            _productData.DeletePrincipal(principal);
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
