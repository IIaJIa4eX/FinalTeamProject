using DatabaseConnector.Interfaces;

namespace DatabaseConnector;

public class Comment : IMessage
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public Content Content { get; set; }
    public int Rating { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsVisible { get; set; }

}
