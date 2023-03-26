using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DatabaseConnector
{
    [Table("AccountSessions")]
    public class AccountSession
    {
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
}
