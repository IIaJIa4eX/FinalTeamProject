using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FinalProject.Services;

namespace FinalProject.Controllers;

//[Route("[controller]")]
//[AllowAnonymous]
public class HomeController : Controller  //если удалите, то никакого Index page не будет
{
    private PostDataHandler _postDataHandler { get; set; }

    #region Constructor

    public HomeController(PostDataHandler postDataHandler)
    {
        _postDataHandler = postDataHandler;
    }

    #endregion

    public IActionResult Index(string creationDate, string category, int skip)
    {
        var posts = _postDataHandler.GetPostsByCategory(creationDate, category, skip);

        return View(posts);
    }
    [Authorize]
    [HttpGet]
    [Route("/[action]")]
    public IActionResult Categories()
    {
        return View();
    }

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //public IActionResult Error()  //это страница с ошибками, ее можно убрать
    //{
    //    return View("~/");
    //}
}

