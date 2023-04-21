using DatabaseConnector.Interfaces;
using System.Linq.Expressions;

namespace FinalProject.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : IEntity
{
    /// <summary>Создать элемент в БД</summary>
    /// <param name="item">объект для добавления в БД</param>
    /// <returns>Id элемента в БД</returns>
    int CreateAndGetId(TEntity item);

    /// <summary>Создать элемент в БД</summary>
    /// <param name="item">объект для добавления в БД</param>
    /// <returns>результат сохранения</returns>
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
    IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

    /// <summary>Удалить элемент из БД</summary>
    /// <param name="item">объект БД</param>
    int Remove(TEntity item);

    /// <summary>Изменить объект в БД</summary>
    /// <param name="item">объект для изменения</param>
    int Update(TEntity item);

    /// <summary>Получить элементы из таблицы и связанные элементы</summary>
    /// <param name="includeProperties"></param>
    /// <returns>Список объектов</returns>
    IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);

    /// <summary>Получить отфильтрованные элементы из таблицы и связанные элементы</summary>
    /// <param name="includeProperties">Включение</param>
    /// <returns>Список объектов</returns>
    IEnumerable<TEntity> GetWithInclude(Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties);

    /// <summary>Получить отфильтрованный список из БД</summary>
    /// <param name="predicate">Фильтр</param>
    /// <param name="skip">Пропустить</param>
    /// <param name="take">Получить</param>
    /// <returns>Список объектов</returns>
    IEnumerable<TEntity> GetWithSkipAndTake(Expression<Func<TEntity, bool>> predicate, int skip, int take);

    /// <summary>Получить элементы из таблицы и связанные элементы</summary>
    /// <param name="skip">Пропустить</param>
    /// <param name="take">Получить</param>
    /// <param name="includeProperties">Включение</param>
    /// <returns>Список объектов</returns>
    IEnumerable<TEntity> GetWithSkipAndTakeWithInclude(int skip, int take, params Expression<Func<TEntity, object>>[] includeProperties);

    /// <summary>Получить отфильтрованные элементы из таблицы и связанные элементы</summary>
    /// <param name="skip">Пропустить</param>
    /// <param name="take">Получить</param>
    /// <param name="predicate">Фильтр</param>
    /// <param name="includeProperties">Включение</param>
    /// <returns>Список объектов</returns>
    IEnumerable<TEntity> GetWithSkipAndTakeWithInclude(int skip, int take, Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] includeProperties);
}