namespace SlaveryMarket.Data.Entity;

public class Order : BaseEntity
{
    public long ClientId { get; set; }
    public Client Client { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreateDate { get; set; }
}