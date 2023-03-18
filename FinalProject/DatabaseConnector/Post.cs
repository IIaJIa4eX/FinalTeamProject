using DatabaseConnector.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnector;

[Table("Posts")]
public class Post : IMessage
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    [Column]
    [StringLength(255)]
    public int Rating { get; set; }

    [Column]
    [StringLength(255)]
    public string? ContentText { get; set; }

    [Column]
    public DateTime CreationDate { get; set; }

    [Column]
    public bool IsVisible { get; set; }

    [Column]
    [StringLength(255)]
    public string? Category { get; set; }

    [ForeignKey(nameof(DatabaseConnector.Content))]
    public Content? IssueContent { get; set; }

    public virtual User? Users { get; set; }
}
