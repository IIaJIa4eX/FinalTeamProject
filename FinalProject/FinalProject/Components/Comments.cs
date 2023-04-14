using DatabaseConnector;
using FinalProject.DataBaseContext;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Components;

public class CommentsViewComponent : ViewComponent
{
    private readonly EFGenericRepository<Post> _posts;
    private readonly EFGenericRepository<Comment> _comments;
    private readonly EFGenericRepository<Content> _content;

    public CommentsViewComponent(
        EFGenericRepository<Post> posts,
        EFGenericRepository<Comment> comments,
        EFGenericRepository<Content> content)
    {
        _posts = posts;
        _comments = comments;
        _content = content;
    }
    //public Task<IViewComponentResult> InvokeAsync(int postId,int skip,int take)
    //{
    //    return View();
    //}
    //public IViewComponentResult Invoke(int postId, int skip, int take) 
    //{
    //    return View();
    //}
    //private Task<IEnumerable<Comment>> GetPosts(int postId)
    //{
    //    var posts = _posts.GetWithInclude(predicate:p=>p.Id==postId);
    //}
}
