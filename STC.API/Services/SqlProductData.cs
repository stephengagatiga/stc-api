using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using STC.API.Data;
using STC.API.Entities.ProductEntity;
using STC.API.Models;
using Microsoft.EntityFrameworkCore;
using STC.API.Models.Product;

namespace STC.API.Services
{
    public class SqlProductData : IProductData
    {
        private STCDbContext _context;

        public SqlProductData(STCDbContext context)
        {
            _context = context;
        }

        public Principal AddPrincipal(string principalName)
        {
            var principal = new Principal { Name = principalName, Products = new List<Product>(), Active = true };
            _context.Principals.Add(principal);
            _context.Entry(principal).State = EntityState.Added;
            _context.SaveChanges();
            return principal;
        }

        public Product AddProduct(int principalId, string productName)
        {
            if (_context.Products.Where(p => p.PrincipalId == principalId && p.Name == productName).FirstOrDefault() == null)
            {
                var product = new Product { Name = productName, PrincipalId = principalId, Active = true };
                _context.Products.Add(product);
                _context.Entry(product).State = EntityState.Added;
                _context.SaveChanges();
                return product;
            }
            return null;
        }

        public void DeletePrincipal(Principal principal)
        {
            _context.Principals.Remove(principal);
            _context.Entry(principal).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.Entry(product).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void EditPrincipal(Principal principal, PrincipalEditDto principalEditDto)
        {
            principal.Name = principalEditDto.Name;
            _context.Entry(principal).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void EditProduct(Product product, ProductEditDto productEditDto)
        {
            product.Name = productEditDto.Name;
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Principal GetPrincipalById(int principalId)
        {
            var principal = _context.Principals.Where(p => p.Id == principalId)
                                .FirstOrDefault();
            return principal;
        }

        public Principal GetPrincipalByIdWithProducts(int principalId)
        {
            var principal = _context.Principals.Where(p => p.Id == principalId)
                                .Include(x => x.Products)
                                .FirstOrDefault();
            return principal;
        }

        public Principal GetPrincipalByName(string principalName)
        {
            var principal = _context.Principals.FirstOrDefault(item => item.Name == principalName);
            return principal;
        }

        public ICollection<Principal> GetPrincipals()
        {
            var principals = _context.Principals
                                    .Where(p => p.Active == true)
                                    .OrderBy(principal => principal.Name)
                                    .Include(item => item.Products)
                                    .ToList();
            return principals;
        }

        public Product GetProduct(int principalId, int productId)
        {
            var product = _context.Products.FirstOrDefault(item => item.Id == productId && item.PrincipalId == principalId);
            return product;
        }

        public Product GetProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            return product;
        }
    }
}
