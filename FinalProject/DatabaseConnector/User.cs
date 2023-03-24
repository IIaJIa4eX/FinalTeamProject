using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnector;

[Table("User")]
public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column]
    [StringLength(255)]
    public string? NickName { get; set; }

    [Column]
    [StringLength(255)]
    public string? FirstName { get; set; }

    [Column]
    [StringLength(255)]
    public string? LastName { get; set; }

    [Column]
    [StringLength(255)]
    public string? Patronymic { get; set; }

    [Column]
    public DateTime Birthday { get; set; }

    [Column]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column]
    [StringLength(255)]
    public string? Password { get; set; }

    [Column]
    [StringLength(255)]
    public string? UserRole { get; set; }

    [Column]
    public bool IsBanned { get; set; } = false;

}