using System.Linq.Expressions;
using AutoMapper;
using eCommerce.BLL.DTO;
using eCommerce.BLL.Services.Interfaces;
using eCommerce.DAL.Entity;
using eCommerce.DAL.Repository.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace eCommerce.BLL.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductsRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
    private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;
    public ProductService(IProductsRepository repository, 
        IMapper mapper, 
        IValidator<ProductAddRequest> productAddRequestValidator, 
        IValidator<ProductUpdateRequest> productUpdateRequestValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _productAddRequestValidator = productAddRequestValidator;
        _productUpdateRequestValidator = productUpdateRequestValidator;
    }
    
    public async Task<List<ProductResponse>?> GetProducts()
    {
        IEnumerable<Product?> products = await _repository.GetProducts();
        
        List<ProductResponse> productsResponse = _mapper.Map<IEnumerable<ProductResponse>>(products).ToList();
        return productsResponse;
    }

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> condition)
    {
        Product? product = await _repository.GetProductByCondition(condition);

        if (product == null)
        {
            return null;
        }
        
        ProductResponse productResponse = _mapper.Map<ProductResponse>(product);
        return productResponse;
    }

    public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> condition)
    {
        IEnumerable<Product?> product = await _repository.GetProductsByCondition(condition);
        
        IEnumerable<ProductResponse?> productsResponse = _mapper.Map<IEnumerable<ProductResponse>>(product);
        return productsResponse.ToList();
    }

    public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
    {
        if (productAddRequest == null)
        {
            throw new ArgumentNullException(nameof(productAddRequest));
        }
        
        ValidationResult validationResult = await _productAddRequestValidator.ValidateAsync(productAddRequest);

        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
            throw new ValidationException(errors);
        }
        
        Product? product = _mapper.Map<Product>(productAddRequest);
        
        Product? addedProduct = await _repository.AddProduct(product);

        if (productAddRequest is null)
        {
            return null;
        }
        
        return _mapper.Map<ProductResponse>(addedProduct);
    }
    
    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        Product? existingProduct = await _repository.GetProductByCondition(x => x.ProductId == productUpdateRequest.ProductId);
        
        if(existingProduct == null)
        {
            throw new ArgumentException("Invalid Product ID");
        }
        
        if (productUpdateRequest == null)
        {
            throw new ArgumentNullException(nameof(productUpdateRequest));
        }
        
        ValidationResult validationResult = await _productUpdateRequestValidator.ValidateAsync(productUpdateRequest);

        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
            throw new ValidationException(errors);
        } 
        
        Product? product = _mapper.Map<Product>(productUpdateRequest);
        
        Product? updatedProduct = await _repository.UpdateProduct(product);
        
        ProductResponse? updatedProductResponse = _mapper.Map<ProductResponse>(updatedProduct);
        return updatedProductResponse;
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        Product? existingProduct = await _repository.GetProductByCondition(x => x.ProductId == productId);
        if (existingProduct == null)
        {
            return false;
        }
        
        bool isDeleted = await _repository.DeleteProduct(existingProduct);
        return isDeleted;
    }
}