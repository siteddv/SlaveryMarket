using System.ComponentModel.DataAnnotations;

namespace SlaveryMarket.Data.Entity;

public class Product : BaseEntity
{   
    [MaxLength(50)]
    public string Name { get; set; }
    
    [Length(3,15)]
    public string? Articul { get; set; }
    
    [MaxLength(500)]
    public string Description { get; set; }
    
    [Range(0, 1000000)]
    public decimal Price { get; set; }
    
    public List<OrderItem> OrderItems { get; set; }
}

