namespace FinalProject.Models.Requests
{
    public class GetPostsResponse : IOperationResult
    {
        public IList<PostsDto>? Posts { get; set; }
        public int ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
