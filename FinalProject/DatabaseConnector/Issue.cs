using DatabaseConnector.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnector;

[Table("Issue")]
public class Issue : IMessage
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    [ForeignKey(nameof(Content))]
    public int ContentId { get; set; }

    [Column]
    [StringLength(255)]
    public string? ContentText { get; set; }

    [Column]
    public DateTime CreationDate { get; set; }

    [Column]
    public bool IsVisible { get; set; }

    [Column]
    public short IssueType { get; set; }

    public virtual User User { get; set; } = null;

    public virtual Content Content { get; set; } = null;
}
