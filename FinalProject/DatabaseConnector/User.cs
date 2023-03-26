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
    public bool IsBanned { get; set; } = false;

    [InverseProperty(nameof(Post.User))]
    public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();

    [InverseProperty(nameof(Comment.User))]
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

    [InverseProperty(nameof(Issue.User))]
    public virtual ICollection<Issue> Issues { get; set; } = new HashSet<Issue>();

}