
namespace SlaveryMarket.Data.Entity;

public class Product
{   
    public Product(string articul, string name, string description, decimal price)
    {
        Name = name;
        Articul = articul;
        Description = description;
        Price = price;
    }
    
    public Product(long id, string articul, string name, string description, decimal price) 
        : this(articul, name, description, price)
    {
        Id = id;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string Articul { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}

