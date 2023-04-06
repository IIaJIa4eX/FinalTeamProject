using DatabaseConnector;
using FinalProject.DataBaseContext;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using FinalProject.Services;
using FinalProject.Interfaces;

namespace FinalProject.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]")]
    public class AccountPageController : Controller
    {
        private readonly EFGenericRepository<User> _userRepository;
        private readonly IAuthenticateService _authenticateService;

        public AccountPageController(EFGenericRepository<User> userRepository, IAuthenticateService authenticateService)
        {
            _userRepository = userRepository;
            _authenticateService = authenticateService;
        }

        [Route("/[action]")]
        [HttpGet]
        public ActionResult Details()
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var scheme = headerValue.Scheme;
                var sessionToken = headerValue.Parameter;
                if (string.IsNullOrEmpty(sessionToken))
                    return Unauthorized();
                SessionInfo sessionInfo = _authenticateService.GetSessionInfo(sessionToken);
                var users = _userRepository.Get(x => x.Id == sessionInfo.Account.Id);
                if (users.Any())
                   return View(new UserDto
                   {
                       Id = sessionInfo.Account.Id,
                       NickName = sessionInfo.Account.NickName,
                       FirstName = sessionInfo.Account.FirstName,
                       LastName = sessionInfo.Account.LastName,
                       Patronymic = sessionInfo.Account.Patronymic,
                       Birthday = sessionInfo.Account.Birthday,
                       Email = sessionInfo.Account.Email
                   });
            }
            return View();
        }
    }
}
