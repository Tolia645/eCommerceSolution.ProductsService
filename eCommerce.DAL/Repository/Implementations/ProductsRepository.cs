using System.Linq.Expressions;
using eCommerce.DAL.Entity;
using eCommerce.DAL.Persistence;
using eCommerce.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.DAL.Repository.Implementations;

public class ProductsRepository : IProductsRepository
{
    private readonly ApplicationDbContext _context;

    public ProductsRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Product>?> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product>?> GetProductsByCondition(Expression<Func<Product, bool>> condition)
    {
        return await _context.Products.Where(condition).ToListAsync();
    }

    public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> expression)
    {
        return await _context.Products.FirstOrDefaultAsync(expression);
    }
    
    public async Task<Product?> AddProduct(Product product)
    {
        product.ProductId = Guid.NewGuid();
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> UpdateProduct(Product product)
    {
        Product? existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == product.ProductId);

        if (existingProduct == null)
        {
            return null;
        }
        
        existingProduct.ProductName = product.ProductName;
        existingProduct.Category = product.Category;
        existingProduct.UnitPrice = product.UnitPrice;
        existingProduct.QuantityInStock = product.QuantityInStock;
        
        await _context.SaveChangesAsync();
        
        return existingProduct;
    }

    public async Task<bool> DeleteProduct(Product? product)
    {
        if (product == null)
        {
            return false;
        }
        
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}