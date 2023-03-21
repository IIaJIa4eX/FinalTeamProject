using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinalProject.Controllers
{
    public class HomeController : Controller  //если удалите, то никакого Index page не будет
    {


        #region Constructor

        public HomeController()
        {
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Categories()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()  //это страница с ошибками, ее можно убрать
        {
            return View();
        }
    }
}

