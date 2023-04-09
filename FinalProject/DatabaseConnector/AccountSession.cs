using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DatabaseConnector.Interfaces;

namespace DatabaseConnector;
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

[Table("AccountSessions")]
public class AccountSession : IEntity
{
    public int Id { get => SessionId; set => SessionId = value; }
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SessionId { get; set; }

    [Required]
    [StringLength(384)]
    public string SessionToken { get; set; }

    [ForeignKey(nameof(User))]
    public int AccountId { get; set; }

    [Column(TypeName = "Timestamp")]
    public DateTime TimeCreated { get; set; }

    [Column(TypeName = "Timestamp")]
    public DateTime TimeLastRequest { get; set; }

    public bool IsClosed { get; set; }

    [Column(TypeName = "Timestamp")]
    public DateTime? TimeClosed { get; set; }

    public virtual User User { get; set; }
}

#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.