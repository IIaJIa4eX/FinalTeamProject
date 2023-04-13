namespace DatabaseConnector.DTO.Post;

public class ContentDTO
{
    public string? Text { get; set; }
    public int Id { get; set; }
    public int PostId { get; set; }
    public bool IsVisible { get; set; }
    public DateTime CreationDate { get; set; }
}
