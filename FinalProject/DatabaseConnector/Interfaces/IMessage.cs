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
