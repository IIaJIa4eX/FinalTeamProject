﻿using DatabaseConnector;
using FinalProject.DataBaseContext;
using FinalProject.Models.DTO;
using FinalProject.Models.DTO.PostDTO;
using FinalProject.Models.Requests;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime;

namespace FinalProject.Controllers
{
    [Route("Post")]
    [Authorize]
    public class PostController : Controller
    {

        PostDataHandler _postDataHandler;

        public PostController(PostDataHandler postDataHandler)
        {
            _postDataHandler = postDataHandler;
        }

       
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public IActionResult Index([FromRoute]int id)
        {
            //var post = _postDataHandler.GetById(id);
            Post post = new Post()
            {
                CreationDate = DateTime.Now,
                Id = id,
                ContentId = 1,
                Rating = 1,
                IsVisible = true,
                Category = "Cat",
                User = new User()
                {
                    Id = 1,
                    FirstName = "fName",
                    LastName = "lName",
                    NickName = "nName",
                    Birthday = DateTime.Now,
                    Email = "asd@asd.as",
                    PasswordHash = "qwer",
                    PasswordSalt = "asdf",
                    IsBanned = false,
                    UserRole = "a",
                    Patronymic = "patronymic"
                },
                Content = new Content() 
                {
                    Text = "Content",
                    Id=1,
                    CreationDate= DateTime.Now,
                    IsVisible=true
                },
                Comments = new List<Comment>(),
                UserId = 1
            };
            //return Ok($"{post.CreationDate}, {post.ContentId}, {post.User.NickName}");
            return View(post);
        }


        [HttpPost]
        [Route("/[action]")]
        [AllowAnonymous]
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
