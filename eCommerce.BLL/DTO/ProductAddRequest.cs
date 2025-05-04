namespace eCommerce.BLL.DTO;

using eCommerce.BLL.Enums;

public record ProductAddRequest(
    string ProductName,
    CategoryOptions Category,
    double? UnitPrice,
    int? QuantityInStock)
{
    public ProductAddRequest() : this(default, default, default, default)
    {
    }
}