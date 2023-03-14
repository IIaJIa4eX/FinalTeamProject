using DatabaseConnector.Interfaces;

namespace DatabaseConnector;


public class Post : IMessage
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public Content Content { get; set; }
    public int Rating { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsVisible { get; set; }
    public string Category { get; set; }
}
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
public class Content
{
    public string Text { get; set; }
}

public class User
{
    public string Pseudo { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Patronimic { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public short Role { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsBanned { get;set; }
}
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