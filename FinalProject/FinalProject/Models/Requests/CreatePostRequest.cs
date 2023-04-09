using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Requests;

public class CreatePostRequest
{
    public Guid UserId { get; set; }
    public Guid ContentId { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsVisible { get; set; }
    [StringLength(255)]
    public string? Category { get; set; }
}
