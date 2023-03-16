namespace FinalProject.Models
{
    public class CommentsDto
    {
        public string? ContentText { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
