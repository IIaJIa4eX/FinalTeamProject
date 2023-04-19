namespace FinalProject.Interfaces
{
    public interface IOperationResult<T> where T : class
    {
        int ErrorCode { get; set; }
        string? ErrorMessage { get; }
        T? ObjectData { get; }
    }
}
