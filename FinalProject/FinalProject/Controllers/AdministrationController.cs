using DatabaseConnector;
using FinalProject.DataBaseContext;
using FinalProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FinalProject.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
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
                c => c.Content,
                u => u.User);
            return View(issues);
        }

        [HttpGet]
        [Route("Hide/{issueId}")]
        public IActionResult Hide([FromRoute] int issueId)
        {
            var issue = _issuesRepository.FindById(issueId);
            if (issue is not null)
            {
                issue.IsVisible = false;
                _issuesRepository.Update(issue);
            }
            return RedirectToAction("GetIssues");
        }
    }
}
