using DatabaseConnector.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnector;

[Table("User")]
public class User : IEntity
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
    public DateTime? Birthday { get; set; } = null;

    [Column]
    [StringLength(255)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string PasswordSalt { get; set; }

    [StringLength(100)]
    public string PasswordHash { get; set; }

    [Column]
    [StringLength(255)]
    public string? UserRole { get; set; } = "User";

    [Column]
    public bool IsBanned { get; set; } = false;

    [InverseProperty(nameof(Post.User))]
    public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();

    [InverseProperty(nameof(Comment.User))]
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

    [InverseProperty(nameof(Issue.User))]
    public virtual ICollection<Issue> Issues { get; set; } = new HashSet<Issue>();

    [InverseProperty(nameof(AccountSession.User))]
    public virtual ICollection<AccountSession> Sessions { get; set; } = new HashSet<AccountSession>();
}