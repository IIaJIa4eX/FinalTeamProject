namespace DatabaseConnector.DTO.Post;

public class CreatePostDTO
{

    public int UserId { get; set; }

    public string? Category { get; set; }

    //Content Properties__
    public string? Description { get; set; } = string.Empty;


}
