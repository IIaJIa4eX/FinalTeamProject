namespace DatabaseConnector.Interfaces;

public interface IEntity
{
    int Id { get; }
}
public interface IMessage:IEntity
{
    int Id { get;  }
    int UserId { get;  }
    Content Content { get;  }
    // int Rating { get;  }
    bool IsVisible { get; }
    DateTime CreationDate { get; }
}
public interface IGenericRepository<TEntity> where TEntity : class
{
    void Create(TEntity item);
    TEntity FindById(int id);
    IEnumerable<TEntity> Get();
    IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
    void Remove(TEntity item);
    void Update(TEntity item);
}