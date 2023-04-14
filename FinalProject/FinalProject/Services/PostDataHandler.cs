using DatabaseConnector;
using DatabaseConnector.DTO;
using DatabaseConnector.DTO.Post;
using DatabaseConnector.Extensions;
using FinalProject.DataBaseContext;
using FinalProject.Interfaces;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.EntityFrameworkCore.Diagnostics;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using Microsoft.Extensions.Hosting;

namespace FinalProject.Services;

public class PostDataHandler
{
    EFGenericRepository<Post> _postRepository;
    EFGenericRepository<Content> _contentRepository;
    EFGenericRepository<Comment> _commentRepository;
    IAuthenticateService _authenticateService;


    public PostDataHandler(EFGenericRepository<Post> postRepository,
        EFGenericRepository<Content> contentRepository,
        EFGenericRepository<Comment> commentRepository,
        IAuthenticateService authenticateService)
    {
        _postRepository = postRepository;
        _contentRepository = contentRepository;
        _commentRepository = commentRepository;
        _authenticateService = authenticateService;
    }
    //.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    public Post GetById(int id)
    {
        var tmpPost = _postRepository
            .GetWithInclude(post => post.Id == id,
                            comms => comms.Comments,
                            cont => cont.Content!,
                            usr => usr.User!)
            .FirstOrDefault();
        tmpPost.Comments = _commentRepository.GetWithInclude(
                                                com => com.PostId == tmpPost.Id,
                                                content => content.Content!,
                                                u=>u.User!)
                                             .ToArray();
        
        return tmpPost!;
    }

    public bool Create(CreatePostDTO postData, string token)
    {
        if (AuthenticationHeaderValue.TryParse(token, out var headerValue))
        {
            SessionInfo sessionInfo = _authenticateService.GetSessionInfo(headerValue.Parameter!);
            if (sessionInfo is not null)
                postData.UserId = sessionInfo.Account.Id;
            else
                return false;
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
            return entitiesNumb > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool Edit(EditPostDTO postData)
    {
        var post = _postRepository.FindById(postData.Id);
        if (post is not null)
        {
            var content = _contentRepository.FindById(post.ContentId);
            if (content is not null)
            {
                content.CreationDate = DateTime.UtcNow;
                content.IsVisible = true;
                content.Text = postData.Description;
                return _contentRepository.Update(content) > 0;
            }
        }
        return false;
    }

    public bool Delete(EditPostDTO postData)
    {
        var post = _postRepository.FindById(postData.Id);
        if (post is not null)
        {
            post.IsVisible = false;
            return _postRepository.Update(post) > 0;
        }
        return false;
    }

    public bool Rating(string postData, int Id)
    {
        if (postData == "plus" || postData == "minus")
        {
            var post = _postRepository.FindById(Id);
            if (post is not null)
            {
                post.Rating = postData == "plus" ? post.Rating + 1 : post.Rating - 1;
                return _postRepository.Update(post) > 0;
            }
        }
        return false;
    }

    public bool AddContentToComment(CommentCreationDTO dto)
    {
        return true;
    }
    public bool AddComment(ContentDTO content, SessionInfo sessionInfo)
    {
        try
        {
            var post = _postRepository.FindById(content.PostId);
            int commentId = _commentRepository.CreateAndGetId(new Comment
            {
                PostId = content.PostId,
                CreationDate = content.CreationDate,
                IsVisible = true,
                ContentId = _contentRepository.CreateAndGetId(new Content
                {
                    CreationDate = DateTime.UtcNow,
                    IsVisible = true,
                    Text = content.Text
                }),
                UserId = sessionInfo.Account.Id
            });
            post!.Comments.Add(_commentRepository.FindById(commentId)!);
            return _postRepository.Update(post) > 0;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    public bool AddComment(CommentDTO comment)
    {
        try
        {
            return _commentRepository.Create(new Comment
            {
                PostId = comment.PostId,
                CreationDate = DateTime.UtcNow,
                ParentId = comment.ParentId,
                ContentId = _contentRepository.CreateAndGetId(new Content
                {
                    CreationDate = DateTime.UtcNow,
                    IsVisible = true,
                    Text = comment.Content.Text
                })
            }) > 0;
        }
        catch
        {
            return false;
        }

    }

    public IEnumerable<PostDTO> GetLast(int count)
    {
        return Remap(_postRepository.Get(p => p.Content!.IsVisible).TakeLast(count));
    }
    public IEnumerable<Post> GetPostsByCategory(string creationDate = "Desc", string category = "", int skip = 0,int take = 10)
    {
        IEnumerable<Post> posts =
            !string.IsNullOrEmpty(category) ?
            _postRepository.GetWithInclude(
                            post => post.Category == category,
                            comm => comm.Comments,
                            cont => cont.Content!,
                            usr => usr.User!) :
            _postRepository.GetWithInclude(
                        comm => comm.Comments,
                        cont => cont.Content!,
                        usr => usr.User!);
        foreach (var item in posts)
        {
            item.Comments = _commentRepository.GetWithInclude(
                                               com => com.PostId == item.Id,
                                               content => content.Content!)
                                              .OrderBy(time => time.CreationDate)
                                              .ToArray();
        }
        switch (creationDate)
        {
            case "Asc":
                posts.OrderBy(time => time.CreationDate);
                break;
            case "Desc":
            default: posts.OrderByDescending(time => time.CreationDate);
                break;
        }
        return posts.Skip(skip).Take(take);
    }
    private IEnumerable<PostDTO> Remap(IEnumerable<Post> posts)
    {
        List<PostDTO> dtos = new List<PostDTO>(posts.Count());
        foreach (var post in posts)
        {
            dtos.Add(post.Remap());
        }
        return dtos;
    }

    
}
