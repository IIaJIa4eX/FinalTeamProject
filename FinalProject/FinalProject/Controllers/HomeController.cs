using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

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
            return View();
        }
        //[HttpGet]
        //[Route("/[action]")]
        //[Authorize]        
        public IActionResult Categories()
        {
            //var auth = User.Identity.IsAuthenticated;
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()  //это страница с ошибками, ее можно убрать
        //{
        //    return View("~/");
        //}
    }
}

