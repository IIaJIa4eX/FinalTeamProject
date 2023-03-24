using DatabaseConnector;
using FinalProject.DataBaseContext;
using FinalProject.Models.CommonModels;
using FinalProject.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime;

namespace FinalProject.Controllers
{
    [Route("[controller]")]
    public class PostController : Controller
    {

        EFGenericRepository<CommonPostModel> _postRepository;
        EFGenericRepository<Content> _contentRepository;

        public PostController(EFGenericRepository<CommonPostModel> postRepository, EFGenericRepository<Content> contentRepository)
        {
            _postRepository = postRepository;
            _contentRepository = contentRepository;
        }



        [HttpGet]
        [Route("/[action]/{id?}")]
        public IActionResult Index(Guid id)
        {
            var post =  _postRepository.FindByGUID(id);

            return Ok(post);
        }

        [HttpPost]
        [Route("/[action]")]
        public IActionResult AddPost(CommonPostModel postData)
        {
            var id = _contentRepository.CreateAndGetGuid(new Content { Text = postData.Description });
            //_postRepository.Create(postData);

            return Ok();
        }

    }
}
