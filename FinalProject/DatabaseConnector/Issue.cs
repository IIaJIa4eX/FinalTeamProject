using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnector;

[Table("Issue")]
public class Issue
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(Content))]
    public Guid ContentId { get; set; }

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
