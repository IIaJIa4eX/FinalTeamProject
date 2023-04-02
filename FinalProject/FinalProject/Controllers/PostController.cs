using DatabaseConnector;
using FinalProject.BusinessLogicLayer;
using FinalProject.DataBaseContext;
using FinalProject.Models.DTO;
using FinalProject.Models.DTO.PostDTO;
using FinalProject.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime;

namespace FinalProject.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class PostController : Controller
    {

        PostDataHandler _postDataHandler;

        public PostController(PostDataHandler postDataHandler)
        {
            _postDataHandler = postDataHandler;
        }

       
        [HttpGet]
        [Route("/[action]")]
        public IActionResult Index(int id)
        {
            var post = _postDataHandler.GetById(id);

            return Ok($"{post.CreationDate}, {post.ContentId}, {post.User.NickName}");
        }


        [HttpPost]
        [Route("/[action]")]
        public IActionResult AddPost(CreatePostDTO postData)
        {
            bool success = _postDataHandler.Create(postData);

            return Ok(success);
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
}
