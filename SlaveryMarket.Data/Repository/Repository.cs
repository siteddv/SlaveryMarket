using Microsoft.EntityFrameworkCore;
using SlaveryMarket.Data.Entity;

namespace SlaveryMarket.Data.Repository;

public class Repository<TEntity> where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
        CheckEntityPresence();
    }

    private void CheckEntityPresence()
    {
        try
        {
            var _ = _dbSet.EntityType;
        }
        catch
        {
            throw new NotImplementedException($"DbSet {typeof(TEntity).Name} is not present in the db");
        }
    }

    public virtual TEntity? GetById(long id)
    {
        return _dbSet.Find(id);
    }
    
    public virtual List<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }
    
    public virtual TEntity Add(TEntity entity)
    {
        var createdEntity = _dbSet.Add(entity).Entity;
        _context.SaveChanges();
        return createdEntity;
    }
    
    public virtual void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }
    
    public virtual void Delete(long id)
    {
        var entity = GetById(id);
        if (entity == null)
        {
            throw new NotImplementedException($"Entity with id {id} not found");
        }
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }
    
    public virtual void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
    
    public virtual void SaveChanges()
    {
        _context.SaveChanges();
    }
}