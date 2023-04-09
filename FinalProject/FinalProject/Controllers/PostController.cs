using DatabaseConnector;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Services;
using Microsoft.Net.Http.Headers;
using FinalProject.Interfaces;
using System.Net.Http.Headers;
using DatabaseConnector.DTO.Post;

namespace FinalProject.Controllers;

[Route("Post")]
[Authorize]
public class PostController : Controller
{

    PostDataHandler _postDataHandler;

    public PostController(PostDataHandler postDataHandler)
    {
        _postDataHandler = postDataHandler;
    }

        PostDataHandler _postDataHandler;
        IAuthenticateService _authenticateService;

        public PostController(PostDataHandler postDataHandler, IAuthenticateService authenticateService)
        {
            _postDataHandler = postDataHandler;
            _authenticateService = authenticateService;
        }


        [HttpGet]
        [Route("/[action]")]
        public IActionResult Index(int id)
        {
            Content = new Content()
            {
                Id = 1,
                CreationDate = DateTime.Now,
                IsVisible = true,
                Text = "Visible text"
            },
            CreationDate = DateTime.Now,
            IsVisible = true,
            Id = 1,
            UserId = 1
        });
        //return Ok($"{post.CreationDate}, {post.ContentId}, {post.User.NickName}");
        return View(post);
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
    [HttpPost]
    [Route("/[action]")]
    [AllowAnonymous]
    public IActionResult AddPost(CreatePostDTO postData)
    {
        bool success = _postDataHandler.Create(postData);

        [HttpPost]
        [Route("/[action]")]
        public IActionResult AddPost(CreatePostDTO postData)
        {

                bool success = _postDataHandler.Create
                (
                    postData,
                    Request.Headers[HeaderNames.Authorization]
                );

            return Ok();
        }

        return Ok(success);
    }

    [HttpPost]
    [Route("/[action]")]
    public IActionResult Delete(EditPostDTO postData)
    {
        bool success = _postDataHandler.Delete(postData);

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

    [HttpGet]
    [Route("/[action]")]
    public IActionResult PostRating(string rating, int id)
    {
        bool success = _postDataHandler.Rating(rating, id);

        return Ok(success);
    }

    [HttpPost]
    [Route("/[action]")]
    public IActionResult AddPostComment(CommentDTO comment)
    {
        bool success = _postDataHandler.AddComment(comment);

        return Ok(success);
    }

}
