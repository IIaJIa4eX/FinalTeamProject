namespace FinalProject.Models.Requests
{
    public class CreatePostResponse : IOperationResult
    {
        public string? PostId { get; set; }
        public int ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
