using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
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
