namespace DatabaseConnector;
public class SessionInfo
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public string Token { get; set; }

    public string RefreshToken { get; set; }
}
