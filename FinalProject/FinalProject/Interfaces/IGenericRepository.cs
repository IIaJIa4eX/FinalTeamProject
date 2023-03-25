using System.Linq.Expressions;

namespace FinalProject.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    int Create(TEntity item);

    TEntity FindById(int id);
    IEnumerable<TEntity> Get();
    IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
    int Remove(TEntity item);
    int Update(TEntity item);

    public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
    public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties);
}