namespace FinalProject.Models.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }

        public int PostId { get; set; }
        public int ParentId { get; set; }

        public string? Text { get; set; }
    }
}
