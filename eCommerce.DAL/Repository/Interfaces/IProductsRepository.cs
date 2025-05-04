using System.Linq.Expressions;
using eCommerce.DAL.Entity;

namespace eCommerce.DAL.Repository.Interfaces;

public interface IProductsRepository
{
    Task<IEnumerable<Product>?> GetProducts();
    Task<IEnumerable<Product>?> GetProductsByCondition(Expression<Func<Product, bool>> predicate); 
    Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> condition);
    Task<Product?> AddProduct(Product product);
    Task<Product?> UpdateProduct(Product product);
    Task<bool> DeleteProduct(Product product);
}