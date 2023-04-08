namespace DatabaseConnector.DTO.Post;

public class CommentDTO
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int ParentId { get; set; }
    public ContentDTO Content { get; set; }
    public UserDto User { get; set; }
    public bool IsVisible { get; set; }
}
