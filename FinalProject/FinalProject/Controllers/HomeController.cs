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

        public HomeController(PostDataHandler postDataHandler)
        {
            _postDataHandler = postDataHandler;
        }

        public IActionResult Index(string creationDate, string category, int skip)
        {
            var posts = _postDataHandler.GetPostsByCategory(creationDate, category, skip);

            return View(posts);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("/[action]")]
        public IActionResult Categories()
        {
        
            return View();
        }

        public IActionResult Search([FromForm] string str)
        {
            var result = _postDataHandler.FindContent(str);
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()  //это страница с ошибками, ее можно убрать
        {
            return View();
        }
    }
}

