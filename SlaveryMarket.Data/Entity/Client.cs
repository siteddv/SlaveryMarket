using System.Text.Json.Serialization;

namespace SlaveryMarket.Data.Entity;

public class Client : BaseEntity
{
    public string Name { get; set; }
    public decimal CashAmount { get; set; }
    public string Phone { get; set; }
    public List<Order> Orders { get; set; }
}