using DatabaseConnector;
using FinalProject.DataBaseContext;
using FinalProject.Interfaces;
using FinalProject.Models.DTO;
using FinalProject.Models.DTO.PostDTO;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System.Net.Http.Headers;
using System.Net;
using FinalProject.Models;

namespace FinalProject.Services
{
    public class PostDataHandler
    {
        EFGenericRepository<Post> _postRepository;
        EFGenericRepository<Content> _contentRepository;
        EFGenericRepository<Comment> _commentRepository;
        IAuthenticateService _authenticateService;

        public PostDataHandler(EFGenericRepository<Post> postRepository, EFGenericRepository<Content> contentRepository, IAuthenticateService authenticateService)
        {
            _postRepository = postRepository;
            _contentRepository = contentRepository;
            _authenticateService = authenticateService;
        }

        public Post GetById(int id)
        {

            var tmpPost = _postRepository.GetWithInclude
                (
                post => post.Id == id,
                comms => comms.Comments.Take(10),
                cont => cont.Content,
                usr => usr.User
                )
                .FirstOrDefault();

            return tmpPost;
        }

        public bool Create(CreatePostDTO postData, string token)
        {
            bool result;

            if (AuthenticationHeaderValue.TryParse(token, out var headerValue))
            {
                SessionInfo sessionInfo = _authenticateService.GetSessionInfo(headerValue.Parameter!);
                if (sessionInfo != null)
                {
                    postData.UserId = sessionInfo.Account.Id;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
             
            try
            {
                int contentId = _contentRepository.CreateAndGetId(new Content
                {
                    CreationDate = DateTime.UtcNow,
                    IsVisible = true,
                    Text = postData.Description
                });

                int entitiesNumb = _postRepository.Create(new Post
                {
                    Category = postData.Category,
                    ContentId = contentId,
                    Rating = 0,
                    UserId = postData.UserId,
                    CreationDate = DateTime.UtcNow,
                });

                if (entitiesNumb > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

            }
            catch
            {
                result = false;
            }


            return result;
        }

        public bool Edit(EditPostDTO postData)
        {

            var post = _postRepository.FindById(postData.Id);

            if (post == null)
            {
                return false;
            }

            var content = _contentRepository.FindById(post.ContentId);

            if (content == null)
            {
                return false;
            }

            content.CreationDate = DateTime.UtcNow;
            content.IsVisible = true;
            content.Text = postData.Description;

            int res = _contentRepository.Update(content);

            if (res > 0)
            {
                return true;
            }

            return false;
        }

        public bool Delete(EditPostDTO postData)
        {
            var post = _postRepository.FindById(postData.Id);

            if (post == null)
            {
                return false;
            }
            post.IsVisible = false;

            int res = _postRepository.Update(post);

            if (res > 0)
            {
                return true;
            }


            return false;

        }

        public bool Rating(string postData, int Id)
        {
            if (postData == "plus" || postData == "minus")
            {
                var post = _postRepository.FindById(Id);

                if (post != null)
                {
                    if (postData == "plus")
                    {
                        post.Rating += 1;
                    }
                    else
                    {
                        post.Rating -= 1;
                    }

                    int res = _postRepository.Update(post);

                    if (res > 0)
                    {
                        return true;
                    }

                    return false;
                }

            }

            return false;

        }

        public bool AddComment(CommentDTO comment)
        {

            try
            {
                int contentId = _contentRepository.CreateAndGetId(new Content
                {
                    CreationDate = DateTime.UtcNow,
                    IsVisible = true,
                    Text = comment.Text
                });

                int res = _commentRepository.Create(new Comment
                {
                    PostId = comment.PostId,
                    CreationDate = DateTime.UtcNow,
                    ParentId = comment.ParentId,
                    ContentId = contentId
                });

                if (res > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        public IEnumerable<Post> GetPostsByCategory(string creationDate = "Desc", string category = "", int skip = 0)
        {
            if(creationDate == "Desc")
            {
                if (!string.IsNullOrEmpty(category))
                {

                    return _postRepository
                           .Get(post => post.Category == category)
                           .OrderByDescending(time => time.CreationDate).Skip(skip).Take(10);
             
                }
                   
            }


            if (creationDate == "Asc")
            {
                if (string.IsNullOrEmpty(category))
                {
                    return _postRepository
                           .Get()
                           .OrderBy(time => time.CreationDate).Skip(skip).Take(10);
                }

                    return _postRepository
                            .Get(post => post.Category == category)
                            .OrderBy(time => time.CreationDate).Skip(skip).Take(10);
            }

            return _postRepository
                           .Get()
                           .OrderByDescending(time => time.CreationDate).Skip(skip).Take(10);

        }


    }
}
