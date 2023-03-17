using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostPageController : ControllerBase
    {
        private readonly ILogger<PostPageController> _logger;

        public PostPageController(ILogger<PostPageController> logger)
        {
            _logger = logger;
        }
        [HttpPost("create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreatePostRequest request)
        {
            return null;
        }
        [HttpGet("get-by-client-id")]
        [ProducesResponseType(typeof(GetPostsResponse), StatusCodes.Status200OK)]
        public IActionResult GetByClientId([FromQuery] string clientId)
        {
            return null;
        }
    }
}
