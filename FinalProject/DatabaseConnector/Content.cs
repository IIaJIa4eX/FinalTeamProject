using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DatabaseConnector;

[Table("Content")]
public class Content
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column]
    public DateTime CreationDate { get; set; }

    [Column]
    public bool IsVisible { get; set; }

}

