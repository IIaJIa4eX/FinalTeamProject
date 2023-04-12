using DatabaseConnector;
using DatabaseConnector.DTO.Post;
using DatabaseConnector.Extensions;
using FinalProject.DataBaseContext;
using FinalProject.Interfaces;
using System.Net.Http.Headers;

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

    public Post GetById(int id)
    {
        var tmpPost = _postRepository
            .GetWithInclude(post => post.Id == id,
                            comms => comms.Comments.Take(50),
                            cont => cont.Content!,
                            usr => usr.User!)
            .FirstOrDefault();

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

    public bool AddComment(CommentDTO comment)
    {
        try
        {
            int contentId = _contentRepository.CreateAndGetId(new Content
            {
                CreationDate = DateTime.UtcNow,
                IsVisible = true,
                Text = comment.Content.Text
            });

            int res = _commentRepository.Create(new Comment
            {
                PostId = comment.PostId,
                CreationDate = DateTime.UtcNow,
                ParentId = comment.ParentId,
                ContentId = contentId
            });

            return res > 0;
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
    private IEnumerable<PostDTO> Remap(IEnumerable<Post> posts)
    {
        List<PostDTO> dtos=new List<PostDTO>(posts.Count());
        foreach (var post in posts)
        {
            dtos.Add(post.Remap());
        }
        return dtos;
    }
    public IEnumerable<Post> GetPostsByCategory(string creationDate = "Desc", string category = "", int skip = 0)
    {
        List<Post> posts = new List<Post>();
        if (!string.IsNullOrEmpty(category))
        {
            posts = _postRepository
                           .GetWithInclude(
                            post => post.Category == category,
                            comm => comm.Comments,
                            cont => cont.Content!,
                            usr => usr.User!).ToList();
        }
        else
        {
            posts = _postRepository
                           .GetWithInclude(
                            comm => comm.Comments,
                            cont => cont.Content!,
                            usr => usr.User!).ToList();
        }
        switch (creationDate)
        {
            case "Desc":posts.OrderByDescending(time => time.CreationDate);
                break;
            case "Asc":
                posts.OrderBy(time => time.CreationDate);
                break;
        }
        switch (creationDate)
        {
            case "Desc":
                if (!string.IsNullOrEmpty(category))
                {
                    return _postRepository
                           .GetWithInclude(
                            post => post.Category == category,
                            comm => comm.Comments,
                            cont => cont.Content!,
                            usr => usr.User!)
                           .OrderByDescending(time => time.CreationDate).Skip(skip).Take(10);
                }
                break;
            case "Asc":
                if (string.IsNullOrEmpty(category))
                {
                    return _postRepository
                           .GetWithInclude(
                            comm => comm.Comments,
                            cont => cont.Content!,
                            usr => usr.User!)
                           .OrderBy(time => time.CreationDate).Skip(skip).Take(10);
                }

                return _postRepository
                       .GetWithInclude(
                        post => post.Category == category,
                        comm => comm.Comments,
                        cont => cont.Content!,
                        usr => usr.User!)
                       .OrderBy(time => time.CreationDate).Skip(skip).Take(10);
            default:
                break;
        }

        return _postRepository
                       .GetWithInclude(
                        comm => comm.Comments,
                        cont => cont.Content!,
                        usr => usr.User!)
                       .OrderByDescending(time => time.CreationDate).Skip(skip).Take(10);

    }
}
