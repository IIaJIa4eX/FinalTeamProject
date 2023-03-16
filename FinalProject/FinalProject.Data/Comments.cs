using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    [Table("Comments")]
    public class Comments
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [ForeignKey(nameof(Posts))]
        public int PostId { get; set; }

        [Column]
        [StringLength(255)]
        public string? ContentText { get; set; }

        [Column]
        public bool IsVisible { get; set; }

        [Column]
        public DateTime CreationDate { get; set; }

        public virtual User Users { get; set; }
    }
}
