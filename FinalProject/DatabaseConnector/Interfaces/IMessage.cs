namespace DatabaseConnector.Interfaces;

/*public interface IEntity
{
    int Id { get; set; }
}*/
public interface IMessage /*: IEntity*/
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public string? ContentText { get; }
    public bool IsVisible { get; }
    public DateTime CreationDate { get; }
    //public Content Content { get;  }
}
