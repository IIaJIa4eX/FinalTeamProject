namespace DatabaseConnector.Interfaces;

/*public interface IEntity
{
    int Id { get; set; }
}*/
public interface IMessage                 
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public string? ContentText { get; }
    public bool IsVisible { get; }
    public DateTime CreationDate { get; }
    public Content IssueContent { get;  }
}
