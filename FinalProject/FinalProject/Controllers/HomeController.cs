using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using FinalProject.DataBaseContext;
using FinalProject.Services;

namespace FinalProject.Controllers
{
    public class HomeController : Controller  //если удалите, то никакого Index page не будет
    {
        PostDataHandler _postDataHandler;

        #region Constructor

        public HomeController(PostDataHandler postDataHandler)
        {
            _postDataHandler = postDataHandler;
        }

        #endregion


        public IActionResult Index(string creationDate, string category)
        {
            var posts = _postDataHandler.GetPostsByCategory(creationDate, category);

            return View(posts);
        }

        [Authorize]
        [HttpGet]
        [Route("/[action]")]
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

