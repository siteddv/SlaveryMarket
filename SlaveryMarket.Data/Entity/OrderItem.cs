namespace SlaveryMarket.Data.Model;

public class OrderItem
{
    public long Id { get; set; }

    // public long Id
    // {
    //     get => _id;
    //     set => _id = value;
    // }
    //
    // public long GetId()
    // {
    //     return _id;
    // }
    //
    // public void SetId(long value)
    // {
    //     _id = value;
    // }
    //
    // private long _id;
    
    public long ProductId { get; set; }
    public int Amount { get; set; }
}