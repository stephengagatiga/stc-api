using Microsoft.EntityFrameworkCore;
using STC.API.Data;
using STC.API.Entities.ProductAssignmentEntity;
using STC.API.Entities.ProductEntity;
using STC.API.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public class SqlProductAssignmentData : IProductAssignmentData
    {
        private STCDbContext _context;

        public SqlProductAssignmentData(STCDbContext context)
        {
            _context = context;

        }

        public ProductAssignment AssignProduct(Product product, User user)
        {
            var productAssignment = new ProductAssignment()
            {
                UserId = user.Id,
                ProductId = product.Id
            };

            _context.ProductAssignments.Add(productAssignment);
            _context.Entry(productAssignment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _context.SaveChanges();
            return productAssignment;
        }

        public ICollection<ProductAssignment> GetAllUserInThisProducts(ICollection<Product> products)
        {
            var productAssignments = _context.ProductAssignments.Where(pa => products.FirstOrDefault(pi => pi.Id == pa.ProductId) != null)
                                                    .ToList();
            return productAssignments;
        }

        public ICollection<User> GetAllUserProductAssignment()
        {
            var users = _context.Users
                                    .Where(u => u.Active == true)
                                    .OrderBy(u => u.FirstName)
                                    .ToList();

            var productAssingments = _context.ProductAssignments
                                                            .Include(pa => pa.Product)
                                                            .ToList();

            return users;
        }

        public ProductAssignment GetProductAssignment(int productAssingmentId)
        {
            var productAssignment = _context.ProductAssignments.FirstOrDefault(pa => pa.Id == productAssingmentId);
            return productAssignment;
        }

        public void RemoveUserProduct(ProductAssignment productAssignment)
        {
            _context.ProductAssignments.Remove(productAssignment);
            _context.Entry(productAssignment).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
