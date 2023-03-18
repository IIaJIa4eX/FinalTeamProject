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

    [ForeignKey(nameof(Content))]
    public Guid ContentId { get; set; }

    [Column]
    public bool IsVisible { get; set; }

    [Column]
    public DateTime CreationDate { get; set; }

    public virtual User? User { get; set; } = null;

    public virtual Post? Post { get; set; } = null;

    public virtual Content? Content { get; set; } = null;
}
