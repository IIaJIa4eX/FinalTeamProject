namespace FinalProject.Models.Requests;
#pragma warning disable CS8618

public class AuthenticationResponse
{
    public AuthenticationStatus Status { get; set; }
    public DatabaseConnector.SessionInfo SessionInfo { get; set; }
}
#pragma warning restore CS8618