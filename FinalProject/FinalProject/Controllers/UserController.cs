using FinalProject.Data;
using FinalProject.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace FinalProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    //private readonly IUserRepository _userRepository;
    public UserController(ILogger<UserController> logger/*, IUserRepository userRepository*/)
    {
        //_userRepository = userRepository;
        _logger = logger;
    }
    //[HttpPost("create")]
    //[ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status200OK)]
    //public IActionResult Create([FromBody] CreateUserRequest request)
    //{
    //    try
    //    {
    //        var userId = _userRepository.Create(new User
    //        {
    //            NickName = request.NickName,
    //            FirstName = request.FirstName,
    //            SurName = request.SurName,
    //            Patronymic = request.Patronymic,
    //            Birthday = request.Birthday,
    //            Email = request.Email

    //        });
    //        return Ok(new CreateUserResponse
    //        {
    //            UserId = userId
    //        });
    //    }
    //    catch (Exception e)
    //    {
    //        _logger.LogError(e, "Create client error.");
    //        return Ok(new CreateUserResponse
    //        {
    //            ErrorCode = 912,
    //            ErrorMessage = "Create client error."
    //        });
    //    }
    //}
}
