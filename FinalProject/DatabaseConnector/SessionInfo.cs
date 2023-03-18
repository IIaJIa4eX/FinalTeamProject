using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnector;

[Table("SessionInfo")]
public class SessionInfo
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    [Column]
    public string? Token { get; set; }

    [Column]
    public string? RefreshToken { get; set; }
}
