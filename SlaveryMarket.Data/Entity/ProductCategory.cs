namespace SlaveryMarket.Data.Entity;

/// <summary>
/// Represents a many-to-many relationship between a product and a category.
/// </summary>
public class ProductCategory
{
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
}