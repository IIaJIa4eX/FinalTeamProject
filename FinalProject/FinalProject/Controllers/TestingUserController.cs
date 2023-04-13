using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FinalProject.DataBaseContext;
using DatabaseConnector;
using FinalProject.Interfaces;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace FinalProject.Controllers;
[Route("UserTest")]
public class TestingUserController : Controller
{
    private readonly EFGenericRepository<User> _repository;
    private readonly IAuthenticateService _authenticateService;
    public TestingUserController(EFGenericRepository<User> repository,IAuthenticateService authenticateService)
    {
        _repository=repository;
        _authenticateService=authenticateService;
    }
    [HttpGet]
    [Route("/[action]")]
    [Authorize]
    public IActionResult GetUserFromDB()
    {
        var authorization = Request.Headers[HeaderNames.Authorization];
        if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
        {
            var sessionToken = headerValue.Parameter;
            if (!string.IsNullOrEmpty(sessionToken)){
                SessionInfo sessionInfo = _authenticateService.GetSessionInfo(sessionToken);
                var users = _repository.Get(u=>u.Id==sessionInfo.Account.Id);
                if (users.Count()>0)
                {
                    return View(users.First());
                }
            }
        }
        throw new Exception(message:"No such user in DB.");        
    }
}