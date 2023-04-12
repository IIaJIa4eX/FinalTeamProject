using DatabaseConnector.Interfaces;
using FinalProject.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinalProject.DataBaseContext;



public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
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

    public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.AsNoTracking().Where(predicate).ToList();
    }
    public TEntity? FindById(int id)
    {
        return _dbSet.Find(id);
    }

    public int Create(TEntity item)
    {
        _dbSet.Add(item);
        return _context.SaveChanges();
    }

    public int CreateAndGetId(TEntity item)
    {
        _dbSet.Add(item);
        _context.SaveChanges();

        return item.Id;
    }

    public int Update(TEntity item)
    {
        _context.Entry(item).State = EntityState.Modified;

        return _context.SaveChanges();
    }
    public int Remove(TEntity item)
    {
        _dbSet.Remove(item);
        return _context.SaveChanges();
    }

    public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return Include(includeProperties).ToList();
    }

    public IEnumerable<TEntity> GetWithInclude(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = Include(includeProperties);
        return query.Where(predicate).ToList();
    }

    public IEnumerable<TEntity> GetWithSkipAndTake(Expression<Func<TEntity, bool>> predicate, int skip, int take)
    {
        return _dbSet
            .Where(predicate)
            .Skip(skip)
            .Take(take)
            .AsNoTracking();
    }
    public IEnumerable<TEntity> GetWithSkipAndTakeWithInclude(
        int skip,
        int take,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();
        return includeProperties
            .Aggregate(query, (current, includeProperties) => current.Include(includeProperties))
            .Skip(skip)
            .Take(take);
    }
    public IEnumerable<TEntity> GetWithSkipAndTakeWithInclude(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = Include(includeProperties);
        return query
            .Where(predicate)
            .Skip(skip)
            .Take(take);
    }

    private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();
        return includeProperties
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }


}