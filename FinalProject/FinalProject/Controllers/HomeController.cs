using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;

public class HomeController : Controller
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
    public IActionResult Privacy()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()  //это страница с ошибками, ее можно убрать
    {
        return View();
    }
}
