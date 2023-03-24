﻿using FinalProject.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinalProject.DataBaseContext;

public interface TEntity
{
    Guid Id { get; set; }
}


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
    public int CreateAndGetGuid(TEntity item)
    {
        _dbSet.Add(item);
        _context.SaveChanges();

        dynamic dataTmp = item;

        return (int)dataTmp.Id;

    }

    public int CreateAndGetGuidPK(TEntity item)
    {
       
        _dbSet.Add(item);
        _context.SaveChanges();

        dynamic dataTmp = item;

        return dataTmp.Id;
        //var idProperty = item.GetType().GetProperty("Id").GetValue(item, null);

        //return item.Id;

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

    public TEntity FindByGUID(Guid id)
    {
        return _dbSet.Find(id);
    }

    public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        return Include(includeProperties).ToList();
    }

    public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = Include(includeProperties);
        return query.Where(predicate).ToList();
    }

    private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();
        return includeProperties
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }

}