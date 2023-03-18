using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    /*[Table("User")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Column]
        [StringLength(255)]
        public string? NickName { get; set; }

        [Column]
        [StringLength(255)]
        public string? FirstName { get; set; }

        [Column]
        [StringLength(255)]
        public string? SurName { get; set; }

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
        public string? UserRole { get; set; }

        [Column]
        public bool IsBanned { get; set; }

        [InverseProperty(nameof(Posts.Users))]
        public virtual ICollection<Posts> Post { get; set; } = new HashSet<Posts>();

        [InverseProperty(nameof(Comments.Users))]
        public virtual ICollection<Comments> Comment { get; set; } = new HashSet<Comments>();
    }*/
}
