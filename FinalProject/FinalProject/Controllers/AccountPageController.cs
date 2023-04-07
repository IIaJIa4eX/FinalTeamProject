using DatabaseConnector;
using FinalProject.DataBaseContext;
using FinalProject.Interfaces;
using FinalProject.Models;
using FinalProject.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Net.Http.Headers;

namespace FinalProject.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("AccountPage")]
    public class AccountPageController : Controller
    {
        private readonly EFGenericRepository<User> _userRepository;
        private readonly IAuthenticateService _authenticateService;

        public AccountPageController(EFGenericRepository<User> userRepository, IAuthenticateService authenticateService)
        {
            _userRepository = userRepository;
            _authenticateService = authenticateService;
        }
        [Route("Index")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [Route("/[action]")]
        [HttpGet]
        public ActionResult Details()
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                //    var scheme = headerValue.Scheme;
                //    var sessionToken = headerValue.Parameter;
                SessionInfo sessionInfo = _authenticateService.GetSessionInfo(headerValue.Parameter!);
                var users = _userRepository.Get(x => x.Id == sessionInfo.Account.Id);
                if (users.Any())
                {
                    var user = users.First();

                    return View(new UserDto
                    {
                        Id = user.Id, //sessionInfo.Account.Id,
                        NickName = user.NickName, //sessionInfo.Account.NickName,
                        FirstName = user.FirstName, //sessionInfo.Account.FirstName,
                        LastName = user.LastName, //sessionInfo.Account.LastName,
                        Patronymic = user.Patronymic, //sessionInfo.Account.Patronymic,
                        Birthday = user.Birthday, //sessionInfo.Account.Birthday,
                        Email = user.Email // sessionInfo.Account.Email

                    });
                }
            }
            return View();
        }
        [Route("Edit")]
        [HttpGet]
        public ActionResult Edit()
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                SessionInfo sessionInfo = _authenticateService.GetSessionInfo(headerValue.Parameter!);
                var user = _userRepository.FindById(sessionInfo.Account.Id);
                if (user is not null)
                {
                    return View(new UserDto
                    {
                        Id = user.Id, //sessionInfo.Account.Id,
                        NickName = user.NickName, //sessionInfo.Account.NickName,
                        FirstName = user.FirstName, //sessionInfo.Account.FirstName,
                        LastName = user.LastName, //sessionInfo.Account.LastName,
                        Patronymic = user.Patronymic, //sessionInfo.Account.Patronymic,
                        Birthday = user.Birthday, //sessionInfo.Account.Birthday,
                        Email = user.Email // sessionInfo.Account.Email

                    });
                }
            }
                return View();
        }
        [Route("Edit")]
        [HttpPost]
        public ActionResult Edit([FromForm] UserDto user)
        {
            var updated = _userRepository.FindById(user.Id);
            if (updated is not null)
            {
                updated.Email = user.Email;
                updated.NickName = user.NickName;
                updated.FirstName = user.FirstName;
                updated.LastName = user.LastName;
                updated.Birthday = user.Birthday;
                updated.Patronymic = user.Patronymic;
                _userRepository.Update(updated);
                return RedirectToAction("Details");
            }
            return BadRequest();
        }
    }
}
