using DatabaseConnector.DTO;
using FinalProject.Models.Requests;

namespace FinalProject.Interfaces
{
    public interface IAuthenticateService
    {
        AuthenticationResponse Login(AuthenticationRequest authenticationRequest);
        public SessionInfo GetSessionInfo(string sessionToken);
    }
}
