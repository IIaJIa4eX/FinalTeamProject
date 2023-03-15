using DatabaseConnector.Interfaces;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Extensions;

namespace DatabaseConnector;

public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    DbContext _context;
    DbSet<TEntity> _dbSet;

    public EFGenericRepository(Context context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public IEnumerable<TEntity> Get()
    {
        return _dbSet.AsNoTracking().ToList();
    }

    public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
    {
        return _dbSet.AsNoTracking().Where(predicate).ToList();
    }
    public TEntity FindById(int id)
    {
        return _dbSet.Find(id);
    }

    public void Create(TEntity item)
    {
        _dbSet.Add(item);
        _context.SaveChanges();
    }
    public void Update(TEntity item)
    {
        _context.Entry(item).State = EntityState.Modified;
        _context.SaveChanges();
    }
    public void Remove(TEntity item)
    {
        _dbSet.Remove(item);
        _context.SaveChanges();
    }
}