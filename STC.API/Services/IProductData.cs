using STC.API.Entities.ProductEntity;
using STC.API.Models;
using STC.API.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public interface IProductData
    {
        Principal GetPrincipalByName(string principalName);
        Principal GetPrincipalById(int principalId);
        Principal GetPrincipalByIdWithProducts(int principalId);
        Principal AddPrincipal(string principalName);
        ICollection<Principal> GetPrincipals();
        Product AddProduct(int principalId, string productName);
        void DeleteProduct(Product product);
        Product GetProduct(int principalId, int productId);
        Product GetProduct(int productId);
        void DeletePrincipal(Principal principal);
        void EditPrincipal(Principal principal, PrincipalEditDto principalEditDto);
        void EditProduct(Product product, ProductEditDto principalEditDto);
    }
}
