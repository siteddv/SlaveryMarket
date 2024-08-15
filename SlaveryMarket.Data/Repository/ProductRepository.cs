using SlaveryMarket.Data.Entity;

namespace SlaveryMarket.Data.Repository;

public class ProductRepository : Repository<Product>
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public override Product Add(Product entity)
    {
        var createdEntity = base.Add(entity);
        
        entity.Articul = $"{entity.Name[0..2]}-{createdEntity.Id}";
        base.Update(entity);
        
        base.SaveChanges();

        return entity;
    }
}