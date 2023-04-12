using DatabaseConnector.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnector;

[Table("Comment")]
public class Comment : IEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    [ForeignKey(nameof(Post))]
    public int PostId { get; set; }

    [ForeignKey(nameof(Content))]
    public int ContentId { get; set; }
    
    [Column]
    public int ParentId { get; set; } = -1;

    [Column]
    public bool IsVisible { get; set; }

    [Column]
    public DateTime CreationDate { get; set; }

    public virtual User? User { get; set; }

    public virtual Post? Post { get; set; }

    public virtual Content? Content { get; set; } 
}
