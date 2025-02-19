﻿using STC.API.Entities.ProductAssignmentEntity;
using STC.API.Entities.ProductEntity;
using STC.API.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public interface IProductAssignmentData
    {
        ProductAssignment AssignProduct(Product product, User user);
        ProductAssignment GetProductAssignment(int productAssingmentId);
        ICollection<User> GetAllUserProductAssignment();
        ICollection<ProductAssignment> GetAllUserInThisProducts(ICollection<Product> products);
        void RemoveUserProduct(ProductAssignment productAssignment);
        ICollection<User> GetUsersInThisProductId(int productId);

    }
}
