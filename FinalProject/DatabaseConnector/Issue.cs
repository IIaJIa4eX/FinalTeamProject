using DatabaseConnector.Interfaces;

namespace DatabaseConnector;

public class Issue : IMessage
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public Content Content { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsVisible { get; set; }
    public short IssueType { get; set; }
    //public int Rating => throw new NotImplementedException();
}
