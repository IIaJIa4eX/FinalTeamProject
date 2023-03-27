namespace FinalProject.Models.Requests
{
    public class CreateUserResponse : IOperationResult
    {
        public Guid UserId { get; set; }
        public int ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
