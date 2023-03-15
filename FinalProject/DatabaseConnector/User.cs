using DatabaseConnector.Interfaces;

namespace DatabaseConnector;

public class User:IEntity
{
    public int Id { get; set; }
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
