using DatabaseConnector;
using DatabaseConnector.DTO;
using Microsoft.AspNetCore.Components.Authorization;

namespace FinalProject.Models.Requests
{
    public class AuthenticationResponse
    {
        public AuthenticationStatus Status { get; set; }
        public SessionInfo SessionInfo { get; set; }
    }
}
