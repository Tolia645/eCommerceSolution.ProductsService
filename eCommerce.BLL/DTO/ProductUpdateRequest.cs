namespace eCommerce.BLL.DTO;

using eCommerce.BLL.Enums;

public record ProductUpdateRequest(
    Guid ProductId,
    string ProductName,
    CategoryOptions Category,
    double? UnitPrice,
    int? QuantityInStock)
{
    public ProductUpdateRequest() : this(default, default, default, default, default)
    {
    }
}