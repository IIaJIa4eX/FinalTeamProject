using FinalProject.Interfaces;
using FinalProject.Models;
using FinalProject.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace FinalProject.Controllers
{
    [Route("[controller]")]
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
        public IActionResult Registration()
        {

            return View();
        }

        [Route("/[action]")]
        [HttpPost]
        public IActionResult Registration([FromForm] RegistrationRequest user)
        {


            if (!ModelState.IsValid)
            {
                return View(user);
            }

            RegistrationResponse registrationResponse = _registrationService.Registration(user);
            if (registrationResponse.Status == 0)
            {
                return RedirectToAction("Login", new AuthenticationRequest
                {
                    Email = user.Email,
                    Password = user.Password
                });
            }

            return Ok(registrationResponse);
        }

        [Route("/[action]")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/[action]")]
        [HttpPost]
        public IActionResult Login([FromForm] AuthenticationRequest authenticationRequest)
        {

            if (!ModelState.IsValid)
            {
                return View(authenticationRequest);
            }

            AuthenticationResponse authenticationResponse = _authenticateService.Login(authenticationRequest);
            if (authenticationResponse.Status == Models.AuthenticationStatus.Success)
            {
                Response.Headers.Add("X-Session-Token", authenticationResponse.SessionInfo.SessionToken);
                Response.Cookies.Append("X-Session-Token", authenticationResponse.SessionInfo.SessionToken);
                return Redirect("~/Home");
            }
            return View("Home/Index");
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
        [Route("/[action]")]
        [HttpGet]
        public IActionResult LogOut()
        {

            Response.Cookies.Delete("X-Session-Token");

            return Redirect("~/Home/Index");
        }
    }
}
