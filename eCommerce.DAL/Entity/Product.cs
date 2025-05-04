using System.ComponentModel.DataAnnotations;

namespace eCommerce.DAL.Entity;

public class Product
{
    [Key]
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? Category { get; set; }
    public double UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
}