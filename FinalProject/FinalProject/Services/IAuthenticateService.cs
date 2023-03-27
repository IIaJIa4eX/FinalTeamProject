    using DatabaseConnector;
using FinalProject.Models.Requests;

namespace FinalProject.Services
{
    public interface IAuthenticateService
    {
        AuthenticationResponse Login(AuthenticationRequest authenticationRequest);
        public SessionInfo GetSessionInfo(string sessionToken); 
    }
}
