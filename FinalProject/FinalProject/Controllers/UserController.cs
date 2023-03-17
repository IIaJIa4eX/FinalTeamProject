using FinalProject.Data;
using FinalProject.Models.Requests;
using FinalProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

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
        [HttpPost("create")]
        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateUserRequest request)
        {
            return null;
        }
    }
}
