using DatabaseConnector;
using DatabaseConnector.DTO;
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
    public IEnumerable<Post> GetPostsByCategory(string creationDate = "Asc", string category = "", int skip = 0,int take = 10)
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
                posts = posts.OrderBy(time => time.CreationDate);
                break;
            case "Desc":
            default:
                posts = posts.OrderByDescending(time => time.CreationDate);
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

    public IEnumerable<Post>? FindContent(string str)
    {
        var res = _postRepository.GetWithInclude(
            p => p.Content!.Text!.Contains(str),
            comm => comm.Comments,
            cont => cont.Content!,
            usr => usr.User!);
        foreach (var item in res)
        {
            item.Comments = _commentRepository.GetWithInclude(
                                               com => com.PostId == item.Id,
                                               content => content.Content!)
                                              .OrderBy(time => time.CreationDate)
                                              .ToArray();
        }
        return res.Count() > 0 ? res : null;
    }

    public IEnumerable<Post> GetUserPostsByCategory(string token, string creationDate = "Desc", string category = "", int skip = 0, int take = 10)
    {
        IEnumerable<Post> posts;

        if (AuthenticationHeaderValue.TryParse(token, out var headerValue))
        {
            SessionInfo sessionInfo = _authenticateService.GetSessionInfo(headerValue.Parameter!);

            posts =
            !string.IsNullOrEmpty(category) ?
            _postRepository.GetWithInclude(
                            post => post.Category == category && post.UserId == sessionInfo.Account.Id,
                            comm => comm.Comments,
                            cont => cont.Content!,
                            usr => usr.User!) :
            _postRepository.GetWithInclude(
                        post => post.UserId == sessionInfo.Account.Id,
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
                    posts = posts.OrderBy(time => time.CreationDate);
                    break;
                case "Desc":
                default:
                    posts = posts.OrderByDescending(time => time.CreationDate);
                    break;
            }

            posts = posts.Skip(skip).Take(take);
        }
        else
        {
            posts = null;
        }

        return posts;

    }
}
