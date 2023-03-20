using DatabaseConnector;
using FinalProject.Models.Requests;
using FinalProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _clientRepositoryService;
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger, IUserRepository clientRepositoryService)
        {
            _clientRepositoryService = clientRepositoryService;
            _logger = logger;
        }
        [HttpPost("create")]
        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateUserRequest request)
        {
            try
            {
                var clientId = _clientRepositoryService.Create(new User
                {
                    NickName = request.NickName,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Patronymic = request.Patronymic,
                    Email = request.Email,
                    Password = request.Password,
                    Birthday = request.Birthday
                });
                return Ok(new CreateUserResponse
                {
                    UserId = clientId
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create client error.");
                return Ok(new CreateUserResponse
                {
                    ErrorCode = 912,
                    ErrorMessage = "Create client error."
                });
            }
        }
    }
}

/*"nickName": "Anatoly911",
  "firstName": "Anatolii",
  "lastName": "Revutskyi",
  "patronymic": "Yaroslavovich",
  "birthday": "1994-04-19T12:28:55.958Z",
  "email": "revutskyanatoly@gmail.com",
  "password": "Cgfcfntkm911"
}*/