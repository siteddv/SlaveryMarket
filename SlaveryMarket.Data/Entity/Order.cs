namespace SlaveryMarket.Data.Model;

public class Order
{
    public long Id { get; set; }
    public long BuyerId { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreateDate { get; set; }
}