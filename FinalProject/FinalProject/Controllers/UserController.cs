using DatabaseConnector;
using DatabaseConnector.DTO;
using FinalProject.Interfaces;
using FinalProject.Models;
using FinalProject.Models.Requests;
using FinalProject.Models.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace FinalProject.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }

        [Route("/[action]")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Registration([FromForm] RegistrationRequest user)
        {
            if (ModelState.IsValid)
            {
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
            return View(user);
        }

        [Route("/[action]")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/[action]")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromForm] AuthenticationRequest authenticationRequest)
        {

            if (ModelState.IsValid)
            {
                AuthenticationResponse authenticationResponse = _authenticateService.Login(authenticationRequest);
                if (authenticationResponse.Status == AuthenticationStatus.Success)
                {
                    Response.Headers.Add("X-Session-Token", authenticationResponse.SessionInfo.SessionToken);
                    var option = new CookieOptions
                    {
                        //option.Expires = DateTime.Now.AddHours(24);
                        SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                        HttpOnly = true,
                        //Secure = true,//works only for https
                        IsEssential = true
                    };
                    Response.Cookies.Append("X-Session-Token", authenticationResponse.SessionInfo?.SessionToken!, option);
                    return Redirect("~/Home/Index");
                }
                return View("Home/Index");
            }
            return View(authenticationRequest);
        }

        [HttpGet("session")]
        public IActionResult GetSessionInfo()
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var sessionToken = headerValue.Parameter;
                if (!string.IsNullOrEmpty(sessionToken))
                {
                    SessionInfo sessionInfo = _authenticateService.GetSessionInfo(sessionToken);
                    return sessionInfo == null ? Unauthorized() : Ok(sessionInfo);
                }
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
