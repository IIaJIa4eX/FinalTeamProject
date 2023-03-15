namespace DatabaseConnector.Interfaces
{
    public interface IMessage
    {
        int Id { get;  }
        int UserId { get;  }
        Content Content { get;  }
        // int Rating { get;  }
        bool IsVisible { get; }
        DateTime CreationDate { get; }
    }
}
