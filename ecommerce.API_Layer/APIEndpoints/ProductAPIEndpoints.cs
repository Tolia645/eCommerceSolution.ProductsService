using eCommerce.BLL.DTO;
using eCommerce.BLL.Services.Interfaces;
using eCommerce.BLL.Validators;
using eCommerce.DAL.Entity;
using FluentValidation;
using FluentValidation.Results;


namespace ecommerce.API_Layer.APIEndpoints;

public static class ProductAPIEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products", async (
            IProductService productService) =>
        {
            List<ProductResponse?> products = await productService.GetProducts();
            return Results.Ok(products);
        });

        app.MapGet("/api/products/search/product-id/{ProductId:guid}", async (
            IProductService productService, 
            Guid ProductId) =>
        {
            ProductResponse productResponse = await productService.GetProductByCondition(x => x.ProductId == ProductId);
            return Results.Ok(productResponse);
        });

        app.MapGet("/api/products/search/{SearchString}", async (IProductService productService, string SearchString) =>
        {
            string search = SearchString.ToLower();

            List<ProductResponse?> productsByProductName = await productService.GetProductsByCondition(
                x => x.ProductName != null && x.ProductName.ToLower().Contains(search));

            List<ProductResponse?> productsByProductCategory = await productService.GetProductsByCondition(
                x => x.Category != null && x.Category.ToLower().Contains(search));

            return productsByProductName.Union(productsByProductCategory);
        });


        app.MapPost("/api/products/", async (
            IProductService productService, 
            IValidator<ProductAddRequest> addRequestValidator, 
            ProductAddRequest productAddRequest) =>
        {
            ValidationResult validationResult = await addRequestValidator.ValidateAsync(productAddRequest);

            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, 
                        x => x.Select(x => x.ErrorMessage).ToArray());
                
                return Results.ValidationProblem(errors);
            }
            
            var addedProduct = await productService.AddProduct(productAddRequest);
            if (addedProduct is not null)
            { 
                return Results.Created($"/api/products/search/product-id/{addedProduct.ProductId}", addedProduct);
            }
            return Results.Problem("Error in adding product");
        });

        app.MapPut("/api/products", async (
            IProductService productService, 
            IValidator<ProductUpdateRequest> productAddValidator,
            ProductUpdateRequest productUpdateRequest) =>
        {
            ValidationResult validationResult = await productAddValidator.ValidateAsync(productUpdateRequest);

            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, 
                        x => x.Select(x => x.ErrorMessage).ToArray());
                return Results.ValidationProblem(errors);
            }
            
            ProductResponse? updatedProductResponse = await productService.UpdateProduct(productUpdateRequest);
            return Results.Ok(updatedProductResponse);
        });

        app.MapDelete("/api/products/{ProductId:guid}", async (IProductService productService, Guid ProductId) =>
        {
            bool isDeleted = await productService.DeleteProduct(ProductId);

            if (!isDeleted)
            {
                return Results.Problem("Product tha you try to delete does not exist");
            }
            return Results.NoContent();
        });
        
        return app;
    }
}