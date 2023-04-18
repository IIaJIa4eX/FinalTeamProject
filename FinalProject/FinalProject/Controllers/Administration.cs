using DatabaseConnector;
using FinalProject.DataBaseContext;
using FinalProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Authorize]
    [Route("/[controller]")]
    public class AdministrationController : Controller
    {
        IAuthenticateService _authService;
        EFGenericRepository<Issue> _issuesRepository;
        EFGenericRepository<User> _usersRepository;
        EFGenericRepository<Post> _postsRepository;
        EFGenericRepository<Comment> _commentsRepository;
        EFGenericRepository<Content> _contentRepository;
        public AdministrationController(
            IAuthenticateService authService,
            EFGenericRepository<Issue> issuesRepository,
            EFGenericRepository<User> usersRepository,
            EFGenericRepository<Post> postsRepository,
            EFGenericRepository<Comment> commentsRepository,
            EFGenericRepository<Content> contentRepository)
        {
            _authService = authService;
            _issuesRepository = issuesRepository;
            _usersRepository = usersRepository;
            _postsRepository = postsRepository;
            _commentsRepository = commentsRepository;
            _contentRepository = contentRepository;
        }

        [HttpGet]
        [Route("/[action]")]
        public IActionResult GetIssues()
        {
            var issues = _issuesRepository.GetWithInclude(
                i => i.IsVisible,
                c=>c.Content,
                u=>u.User);
            return View();
        }
    }
}
