namespace FinalProject.Models
{
    public class PostsDto
    {
        public int Rating { get; set; }
        public string? ContentText { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsVisible { get; set; }
        public string? Category { get; set; }
        public int UserId { get; set; }
    }
}
