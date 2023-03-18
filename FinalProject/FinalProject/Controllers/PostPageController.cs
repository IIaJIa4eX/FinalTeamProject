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
    }
}
