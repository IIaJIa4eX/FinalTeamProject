using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DatabaseConnector.Interfaces;

namespace DatabaseConnector
{
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

        [Column/*(TypeName = "datetime2")*/]
        public DateTime TimeCreated { get; set; }

        [Column/*(TypeName = "datetime2")*/]
        public DateTime TimeLastRequest { get; set; }

        public bool IsClosed { get; set; }

        [Column/*(TypeName = "datetime2")*/]
        public DateTime? TimeClosed { get; set; }

        public virtual User User { get; set; }
    }
}
