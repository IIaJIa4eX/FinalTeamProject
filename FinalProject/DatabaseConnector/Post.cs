using DatabaseConnector.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnector;

[Table("Posts")]
public class Post //: IMessage
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(Content))]
    public Guid ContentId { get; set; }

    [Column]
    [StringLength(255)]
    public int Rating { get; set; }

    [Column]
    public DateTime CreationDate { get; set; }

    [Column]
    public bool IsVisible { get; set; }

    [Column]
    [StringLength(255)]
    public string? Category { get; set; }

    public virtual User? User { get; set; } = null;

    public virtual Content? Content { get; set; } = null;

    [InverseProperty(nameof(Comment.Post))]
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
}
