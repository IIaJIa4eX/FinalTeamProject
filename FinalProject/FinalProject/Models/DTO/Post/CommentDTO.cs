namespace FinalProject.Models.DTO.Post;
#pragma warning disable CS8618
public class CommentDTO
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int ParentId { get; set; }
    public ContentDTO Content { get; set; }
    public UserDto User { get; set; }
    public bool IsVisible { get; set; }
}
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.