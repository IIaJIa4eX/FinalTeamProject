using FinalProject.Models.DTO;

namespace DatabaseConnector;

public class SessionInfo
{
    public int SessionId { get; set; }
    public string SessionToken { get; set; }
    public UserDto Account { get; set; }
}
