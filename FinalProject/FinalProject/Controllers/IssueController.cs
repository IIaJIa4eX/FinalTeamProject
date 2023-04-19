using DatabaseConnector;
using DatabaseConnector.DTO;
using DatabaseConnector.DTO.Post;
using FinalProject.DataBaseContext;
using FinalProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace FinalProject.Controllers
{
    public class IssueController : Controller
    {
        EFGenericRepository<Issue> _issuesRepository;
        EFGenericRepository<User> _usersRepository;
        EFGenericRepository<Content> _contentRepository;
        private readonly IAuthenticateService _authenticateService;
        public IssueController(
            EFGenericRepository<Issue> issuesRepository,
            EFGenericRepository<User> usersRepository,
            EFGenericRepository<Content> contentRepository,
            IAuthenticateService authenticateService)
        {
            _issuesRepository = issuesRepository;
            _usersRepository = usersRepository;
            _authenticateService = authenticateService;
            _contentRepository = contentRepository;
        }

        [HttpGet]
        [Authorize]
        [Route("Issue/{id}")]
        public IActionResult Issue([FromRoute] int id)
        {
            var issue = _issuesRepository.GetWithInclude(
                i => i.Id == id,
                c => c.Content,
                u => u.User);
            if (issue.Any())
            {
                return View(issue.FirstOrDefault());
            }
            return BadRequest();
        }

        
        [HttpGet]
        [Authorize]
        [Route("/[action]")]
        public IActionResult CreateIssue()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [Route("/[action]")]
        public IActionResult CreateIssue([FromForm] ContentDTO contentDTO)
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            User user = null!;
            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                SessionInfo sessionInfo = _authenticateService.GetSessionInfo(headerValue.Parameter!);

                var users = _usersRepository.Get(x => x.Id == sessionInfo.Account.Id);

                if (users.Any())
                {
                    user = users.First();
                }
            }
            Content content = new Content()
            {
                CreationDate = DateTime.Now,
                Text = contentDTO.Text,
                IsVisible = true
            };
            if (user is not null)
            {
                var issue = new Issue()
                {
                    UserId = user.Id,
                    Content = content,
                    IssueType=1,
                    CreationDate= DateTime.Now,
                    IsVisible= true,
                    ContentId=_contentRepository.CreateAndGetId(content),
                    ContentText=contentDTO.Text,
                };
                var issueId=_issuesRepository.CreateAndGetId(issue);
                return Redirect("Issue/"+issueId);
            }
            return BadRequest();
        }
    }
}
