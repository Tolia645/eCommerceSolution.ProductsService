using eCommerce.BLL.DTO;

namespace eCommerce.BLL.Validators;

using FluentValidation;

public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
{
    public ProductUpdateRequestValidator()
    {
        RuleFor(p => p.ProductId)
            .NotEmpty().WithMessage("Product Id is required");
        
        RuleFor(p => p.ProductName)
            .NotEmpty().WithMessage("Product name is required");
        
        RuleFor(p => p.Category)
            .IsInEnum().WithMessage("Category must be a valid category");
        
        RuleFor(p => p.UnitPrice)
            .InclusiveBetween(0, double.MaxValue).WithMessage($"Unit price must be between 0 and {double.MaxValue}");
        
        RuleFor(p => p.QuantityInStock)
            .InclusiveBetween(0, int.MaxValue).WithMessage($"Quantity in stock must be between 0 and {int.MaxValue}");
    }
}