namespace FinalProject.Models.DTO.Post;

public class CommentDTO
{
    public int Id { get; set; }
    public ContentDTO Content { get; set; }
    public UserDto User { get; set; }
    public bool IsVisible { get; set; }
}
