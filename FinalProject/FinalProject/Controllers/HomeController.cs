using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using FinalProject.DataBaseContext;

namespace FinalProject.Controllers
{
    //[Route("[controller]")]
    //[AllowAnonymous]
    public class HomeController : Controller  //если удалите, то никакого Index page не будет
    {


        #region Constructor

        public HomeController()
        {
        }

        #endregion
        //[HttpGet]
        //[Route("/[action]")]
        public IActionResult Index()
        {
            bool ss = User.Identity.IsAuthenticated;
            return View();
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize]
        [HttpGet]
        [Route("/[action]")]
        public IActionResult Categories()
        {
            bool ss = User.Identity.IsAuthenticated;
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()  //это страница с ошибками, ее можно убрать
        //{
        //    return View("~/");
        //}
    }
}

