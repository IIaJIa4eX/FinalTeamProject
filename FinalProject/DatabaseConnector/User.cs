using DatabaseConnector.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnector;

public class User:IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Pseudo { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; }
    public string Patronimic { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public short Role { get; set; } = 0;
    public DateTime? BirthDate { get; set; }
    public bool IsBanned { get; set; } = false;
}
