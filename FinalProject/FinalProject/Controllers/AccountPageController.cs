using DatabaseConnector;
using DatabaseConnector.DTO;
using DatabaseConnector.Extensions;
using FinalProject.DataBaseContext;
using FinalProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace FinalProject.Controllers
{
    [Authorize]
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
                SessionInfo sessionInfo = _authenticateService.GetSessionInfo(headerValue.Parameter!);

                var users = _userRepository.Get(x => x.Id == sessionInfo.Account.Id);

                if (users.Any())
                {
                    var user = users.First();

                    return View(user.Remap());
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
                    return View(user.Remap());
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
