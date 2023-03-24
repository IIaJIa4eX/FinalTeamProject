using System.Linq.Expressions;

namespace FinalProject.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    void Create(TEntity item);

    int CreateAndGetGuid(TEntity item);

    TEntity FindById(int id);
    TEntity FindByGUID(Guid id);
    IEnumerable<TEntity> Get();
    IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
    void Remove(TEntity item);
    void Update(TEntity item);

    public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
    public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties);
}