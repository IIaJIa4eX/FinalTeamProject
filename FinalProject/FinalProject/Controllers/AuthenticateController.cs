using DatabaseConnector;
using FinalProject.Models.Requests;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace FinalProject.Controllers
{
    [Authorize]
    [Route("api/auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IRegistrationService _registrationService;
        public AuthenticateController(IAuthenticateService authenticateService, IRegistrationService registrationService)
        {
            _registrationService = registrationService;
            _authenticateService = authenticateService;
        }
        [AllowAnonymous]
        [HttpPost("registration")]
        public IActionResult Registration([FromQuery] RegistrationRequest registrationRequest)
        {
            RegistrationResponse registrationResponse = _registrationService.Registration(registrationRequest);
           
            return Ok(registrationResponse);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromQuery] AuthenticationRequest authenticationRequest)
        {
            AuthenticationResponse authenticationResponse = _authenticateService.Login(authenticationRequest);
            if (authenticationResponse.Status == Models.AuthenticationStatus.Success)
            {
                Response.Headers.Add("X-Session-Token", authenticationResponse.SessionInfo.SessionToken);
            }
            return Ok(authenticationResponse);
        }

        [HttpGet("session")]
        public IActionResult GetSessionInfo()
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var scheme = headerValue.Scheme;
                var sessionToken = headerValue.Parameter;
                if (string.IsNullOrEmpty(sessionToken))
                    return Unauthorized();
                SessionInfo sessionInfo = _authenticateService.GetSessionInfo(sessionToken);
                if (sessionInfo == null)
                    return Unauthorized();
                return Ok(sessionInfo);
            }
            return Unauthorized();
        }
    }
}
