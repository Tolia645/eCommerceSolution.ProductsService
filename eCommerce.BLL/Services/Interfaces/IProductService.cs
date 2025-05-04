using System.Linq.Expressions;
using eCommerce.BLL.DTO;
using eCommerce.DAL.Entity;

namespace eCommerce.BLL.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductResponse>?> GetProducts();
    Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> condition);
    Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> condition);
    Task<ProductResponse?> AddProduct(ProductAddRequest request);
    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest request);
    Task<bool> DeleteProduct(Guid productId);
}