using DatabaseConnector.Interfaces;
using System.Linq.Expressions;

namespace FinalProject.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : IEntity
{
    /// <summary>Создать элемент в БД</summary>
    /// <param name="item">объект для добавления в БД</param>
    /// <returns>Id элемента в БД</returns>
    int Create(TEntity item);

    /// <summary>Поиск элемента в БД по Id</summary>
    /// <param name="id">Id элемента</param>
    /// <returns>объект из БД</returns>
    TEntity? FindById(int id);
    
    /// <summary>Получить все элементы из БД</summary>
    /// <returns>Список объектов</returns>
    IEnumerable<TEntity> Get();

    /// <summary>Получить отфильтрованный список из БД</summary>
    /// <param name="predicate">Фильтр</param>
    /// <returns>Список объектов</returns>
    IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);

    /// <summary>Удалить элемент из БД</summary>
    /// <param name="item">объект БД</param>
    int Remove(TEntity item);

    /// <summary>Изменить объект в БД</summary>
    /// <param name="item">объект для изменения</param>
    int Update(TEntity item);

    /// <summary>Join</summary>
    /// <param name="includeProperties"></param>
    /// <returns>Список объектов</returns>
    public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
    public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties);
}