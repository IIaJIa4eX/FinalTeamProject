using DatabaseConnector;
using FinalProject.Models.Requests;
using FinalProject.Models.Validations;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace FinalProject.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IRegistrationService _registrationService;
        public UserController(IAuthenticateService authenticateService, IRegistrationService registrationService)
        {
            _registrationService = registrationService;
            _authenticateService = authenticateService;
        }



        [Route("/[action]")]        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registration()
        {
            
            return View();
        }

        [Route("/[action]")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult UserRegistration([FromForm] RegistrationRequest user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            RegistrationResponse registrationResponse = _registrationService.Registration(user);
            return Ok(registrationResponse);
        }

        [Route("/[action]")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login([FromForm] RegistrationRequest user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            RegistrationResponse registrationResponse = _registrationService.Registration(user);
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
