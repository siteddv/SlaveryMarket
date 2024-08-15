using SlaveryMarket.Data.Entity;

namespace SlaveryMarket.Data.Model;

public class Order : BaseEntity
{
    public long BuyerId { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreateDate { get; set; }
}