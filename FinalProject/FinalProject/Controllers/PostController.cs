using DatabaseConnector;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DatabaseConnector.DTO.Post;
using Microsoft.Net.Http.Headers;
using DatabaseConnector.DTO;
using FinalProject.Interfaces;
using System.Net.Http.Headers;
using MarketPracticingPlatform.Attributes;

namespace FinalProject.Controllers;

[Route("Post")]
public class PostController : Controller
{
    PostDataHandler _postDataHandler;
    private readonly IAuthenticateService _authenticateService;

    public PostController(PostDataHandler postDataHandler, IAuthenticateService authenticate)
    {
        _postDataHandler = postDataHandler;
        _authenticateService = authenticate;
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
    public IActionResult CreateNew([FromForm] CreatePostDTO content)
    {
        bool success = _postDataHandler.Create
        (
            content,
            Request.Headers[HeaderNames.Authorization]!
        );
        return Redirect("/");
    }

    [HttpGet]
    [Route("/[action]")]
    public IActionResult Edit()
    {
        return View();
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
    public IActionResult PostRating([FromRoute] string rating, [FromRoute] int id)
    {
        if (_postDataHandler.Rating(rating, id))
        {
            return Redirect($"/Post/{id}");
        }
        return View();
    }
    
    [HttpPost]
    [Route("/[action]")]
    [UnAuthorizedRedirect]
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
        if (_postDataHandler.AddComment(content, sessionInfo))
            return Redirect($"Post/{content.PostId}");
        return Redirect($"Post/{content.PostId}");
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("GetPosts")]
    public IActionResult GetPosts(string creationDate, string category, int skip, int take)
    {
        var posts = _postDataHandler.GetPostsByCategory(creationDate, category, skip, take);
        return PartialView("_PostsPartial", posts);
    }

    [HttpGet]
    [Route("GetUserPosts")]
    [UnAuthorizedRedirect]
    public IActionResult UserPosts()
    {
        var posts = _postDataHandler.GetUserPostsByCategory(Request.Headers[HeaderNames.Authorization]);
        ViewData["scriptsLoaded"] = false;
        return View(posts);
    }

    [HttpPost]
    [Route("GetUserPosts")]
    [UnAuthorizedRedirect]
    public IActionResult GetUserPosts(string creationDate, string category, int skip, int take)
    {
        var posts = _postDataHandler.GetUserPostsByCategory(Request.Headers[HeaderNames.Authorization], creationDate, category, skip, take);
        return PartialView("_UserPostsPartial", posts);
    }

    [HttpGet]
    [Route("comment/{id}")]
    [UnAuthorizedRedirect]
    public IActionResult Comment(int id) 
    {
        return View(_postDataHandler.GetComment(id));
    }

    [Route("comment/Hide/{id}")]
    [UnAuthorizedRedirect]
    public IActionResult HideComment([FromRoute] int id)
    {
        var comment = _postDataHandler.GetComment(id)!;
        comment.IsVisible = false;
        return _postDataHandler.UpdateComment(comment) > 0 ?
                Redirect("/GetIssues") :
                BadRequest();
    }
    [Route("post/Hide/{id}")]
    [UnAuthorizedRedirect]
    public IActionResult HidePost([FromRoute] int id)
    {
        return _postDataHandler.HidePost(id)>0 ?
                Redirect("/GetIssues") :
                BadRequest();
    }
}
