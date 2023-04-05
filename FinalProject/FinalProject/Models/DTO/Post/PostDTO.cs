namespace FinalProject.Models.DTO.Post;

public class PostDTO
{
    public int Id { get; set; }
    public ContentDTO Content { get; set; }
    public UserDto User { get; set; }
    public DateTime CreationDate { get; set; }
    public ICollection<CommentDTO> Comments { get; set; } = new List<CommentDTO>(0);
    public int Rating { get; set; }
    public string Category { get; set; }
    public bool IsVisible { get; set; }
}
