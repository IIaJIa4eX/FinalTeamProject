using FinalProject.Models;
using FinalProject.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostPageController : ControllerBase
{
    private readonly ILogger<PostPageController> _logger;
    //private readonly IPostsRepository _postsRepository;
    public PostPageController(ILogger<PostPageController> logger /*IPostsRepository postsRepository*/)
    {
        _logger = logger;
        
    }
}
    //[HttpPost("create")]
    //[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    //public IActionResult Create([FromBody] CreatePostRequest request)
    //{
    //    //try
    //    //{
    //    //    //var postId = //_postsRepository.Create(new Posts
    //    //    {
    //    //        UserId = request.UserId,
    //    //        ContentText = request.ContentText,
    //    //        CreationDate = request.CreationDate,
    //    //        Category = request.Category,
    //    //        Rating = request.Rating,
    //    //        IsVisible = request.IsVisible
    //    //    });
    //    //    return Ok(new CreatePostResponse
    //    //    {
    //    //        PostId = postId.ToString(),
    //    //    });
    //    //}
    //    //catch (Exception e)
    //    //{
    //    //    _logger.LogError(e, "Create post error.");
    //    //    return Ok(new CreatePostResponse
    //    //    {
    //    //        ErrorCode = 1012,
    //    //        ErrorMessage = "Create post error."
    //    //    });
    //    //}
    //}
    //[HttpGet("get-by-client-id")]
    //[ProducesResponseType(typeof(GetPostsResponse), StatusCodes.Status200OK)]
    //public IActionResult GetByClientId([FromQuery] string clientId)
    //{
    //    try
    //    {
    //        var posts = _postsRepository.GetByClientId(clientId);
    //        return Ok(new GetPostsResponse
    //        {
    //            Posts = posts.Select(posts => new PostsDto
    //            {
    //                Rating = posts.Rating,
    //                ContentText = posts.ContentText,
    //                CreationDate = posts.CreationDate,
    //                IsVisible = posts.IsVisible,
    //                Category = posts.Category,
    //                UserId = posts.UserId
    //            }).ToList()
    //        });
    //    }
    //    catch (Exception e)
    //    {
    //        _logger.LogError(e, "Get posts error.");
    //        return Ok(new GetPostsResponse
    //        {
    //            ErrorCode = 1013,
    //            ErrorMessage = "Get posts error."
    //        });
    //    }
    //}