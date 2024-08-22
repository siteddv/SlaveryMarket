namespace SlaveryMarket.Data.Entity;

public class OrderItem : BaseEntity
{
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public int Amount { get; set; }
    public long OrderId { get; set; }
    public Order Order { get; set; }
}