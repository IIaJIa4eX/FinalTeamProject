using DatabaseConnector.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnector;

[Table("Comment")]
public class Comment : IMessage
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(Post))]
    public Guid PostId { get; set; }

    [Column]
    [StringLength(255)]
    public string? ContentText { get; set; } 

    [Column]
    public bool IsVisible { get; set; }

    [Column]
    public DateTime CreationDate { get; set; }

    [ForeignKey(nameof(DatabaseConnector.Content))]
    public Content? Content { get; set; }

    public virtual User? Users { get; set; }
}
