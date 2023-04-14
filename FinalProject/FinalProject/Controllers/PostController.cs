using DatabaseConnector;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatabaseConnector.DTO.Post;
using Microsoft.Net.Http.Headers;
using DatabaseConnector.DTO;
using FinalProject.Interfaces;
using System.Net.Http.Headers;

namespace FinalProject.Controllers;

[Route("Post")]
[Authorize]
public class PostController : Controller
{

    PostDataHandler _postDataHandler;
    private readonly IAuthenticateService _authenticateService;

    public PostController(PostDataHandler postDataHandler,IAuthenticateService authenticate)
    {
        _postDataHandler = postDataHandler;
        this._authenticateService = authenticate;
    }

    [HttpGet]
    [Route("{id}")]
    [AllowAnonymous]
    public IActionResult Index([FromRoute] int id)
    {
        var post = _postDataHandler.GetById(id);

        return View(post);
    }

    [HttpPost]
    [Route("/[action]")]
    public IActionResult AddPost(CreatePostDTO postData)
    {

        bool success = _postDataHandler.Create
        (
            postData,
            Request.Headers[HeaderNames.Authorization]!
        );

        return Redirect("~/Home/Index");
    }

    [HttpGet]
    [Route("/create/new")]
    [AllowAnonymous]
    public IActionResult CreateNew()
    {
        var userid = HttpContext.Request.Headers.SingleOrDefault(x => x.Key == "UserId").Value.ToString();
        ViewBag.UserId = userid;
        return View();
    }

    [HttpPost]
    [Route("/create/new")]
    [AllowAnonymous]
    public IActionResult CreateNew([FromForm] Content content)
    {
        return View(content);
    }

    [HttpGet]
    [Route("/[action]")]
    public IActionResult Edit()
    {
        return View();
    }

    [HttpPost]
    [Route("/[action]")]
    public IActionResult Edit(EditPostDTO postData)
    {
        bool success = _postDataHandler.Edit(postData);

        return Ok(success);
    }

    [HttpPost]
    [Route("/[action]")]
    public IActionResult Delete(EditPostDTO postData)
    {
        bool success = _postDataHandler.Delete(postData);

        return Ok(success);
    }

    [HttpGet]
    [Route("/[action]/{id}/{rating}")]
    public IActionResult PostRating([FromRoute]string rating,[FromRoute] int id)
    {
        if (_postDataHandler.Rating(rating, id))
        {
            return Redirect($"/Post/{id}");
        }
        return View();
    }
    [HttpPost]
    [Route("/[action]")]
    public IActionResult AddPostComment([FromForm] ContentDTO content)
    {
        SessionInfo sessionInfo = null!;
        var authorization = Request.Headers[HeaderNames.Authorization];
        if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
        {
            var sessionToken = headerValue.Parameter;
            if (!string.IsNullOrEmpty(sessionToken))
            {
                sessionInfo = _authenticateService.GetSessionInfo(sessionToken);
            }
        }
        if (_postDataHandler.AddComment(content,sessionInfo))
            return Redirect($"Post/{content.PostId}");
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("GetPosts")]
    public IActionResult GetPosts(string creationDate, string category, int skip, int take)
    {
        var posts = _postDataHandler.GetPostsByCategory(creationDate, category, skip, take);
        return PartialView("_PostsPartial", posts);
    }
}
