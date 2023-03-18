using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnector;

[Table("User")]
public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

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
    public bool IsBanned { get; set; }

    [InverseProperty(nameof(DatabaseConnector.Post.Users))]
    public virtual ICollection<Post> Post { get; set; } = new HashSet<Post>();

    [InverseProperty(nameof(Comment.Users))]
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

    [InverseProperty(nameof(Issue.UserId))]
    public virtual ICollection<Issue> Issues { get; set; } = new HashSet<Issue>();
}