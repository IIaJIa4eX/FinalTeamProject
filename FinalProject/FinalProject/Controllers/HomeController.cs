using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FinalProject.Services;

namespace FinalProject.Controllers;

public class HomeController : Controller
{
    PostDataHandler _postDataHandler;

    public HomeController(PostDataHandler postDataHandler)
    {
        _postDataHandler = postDataHandler;
    }

    public IActionResult Index(string creationDate, string category)
    {
        var posts = _postDataHandler.GetPostsByCategory(creationDate, category);
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
    public IActionResult Error()
    {
        return View();
    }
}