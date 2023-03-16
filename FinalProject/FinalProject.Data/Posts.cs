using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    [Table("Posts")]
    public class Posts
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PostId { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Column]
        [StringLength(255)]
        public int Rating { get; set; }

        [Column]
        [StringLength(255)]
        public string? ContentText { get; set; }

        [Column]
        public DateTime CreationDate { get; set; }

        [Column]
        public bool IsVisible { get; set; }

        [Column]
        [StringLength(255)]
        public string? Category { get; set; }

        public virtual User Users { get; set; }
    }
}
