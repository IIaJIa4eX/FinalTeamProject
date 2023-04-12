using DatabaseConnector;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using FinalProject.Interfaces;
using System.Net.Http.Headers;
using DatabaseConnector.DTO.Post;
using DatabaseConnector.DTO;

namespace FinalProject.Controllers
{
    [Route("Post")]
    [Authorize]
    public class PostController : Controller
    {

        PostDataHandler _postDataHandler;
        IAuthenticateService _authenticateService;

        public PostController(PostDataHandler postDataHandler, IAuthenticateService authenticateService)
        {
            _postDataHandler = postDataHandler;
            _authenticateService = authenticateService;
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

        [HttpPost]
        [Route("/[action]")]
        public IActionResult Edit(EditPostDTO postData)
        {
            bool success = _postDataHandler.Edit(postData);

            return Ok(success);
        }

        [HttpGet]
        [Route("/[action]")]
        public IActionResult Edit()
        { 

            return View();
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
        public IActionResult Delete(EditPostDTO postData)
        {
            bool success = _postDataHandler.Delete(postData);

            return Ok(success);
        }

        [HttpGet]
        [Route("/[action]")]
        public IActionResult PostRating(string rating, int id)
        {
            bool success = _postDataHandler.Rating(rating, id);

            return Ok(success);
        }

        [HttpPost]
        [Route("/[action]")]
        public IActionResult AddPostComment([FromForm] CommentCreationDTO content)
        {
            bool success = _postDataHandler.AddComment(new CommentDTO()
            {
                IsVisible = true,
                PostId = content.PostId,
                Content = new ContentDTO()
                {
                    CreationDate = DateTime.Now,
                    IsVisible = true,
                    Text = content.Text
                }
            });

            return View();
        }

    }
}
