using DatabaseConnector;
using DatabaseConnector.DTO;
using FinalProject.DataBaseContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace FinalProject.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly EFGenericRepository<User> _userRepository;
        public AdminController(EFGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [Route("/[action]")]
        [HttpGet]
        public ActionResult Admin()
        {
            var user = _userRepository.Get();
            return View(user);
        }

        [Route("Edit/{id}")]
        [HttpGet]
        public ActionResult Edit([FromRoute] int id)
        {
            var user = _userRepository.Get(g => g.Id == id).FirstOrDefault();
            return View(user);
        }

        [Route("Edit/{id}")]
        [HttpPost]
        public ActionResult Edit([FromForm] User user)
        {
            var updated = _userRepository.FindById(user.Id);
            if (updated is not null)
            {
                updated.Email = user.Email;
                updated.NickName = user.NickName;
                updated.FirstName = user.FirstName;
                updated.LastName = user.LastName;
                updated.Birthday = user.Birthday;
                updated.Patronymic = user.Patronymic;
                updated.IsBanned = user.IsBanned;
                _userRepository.Update(updated);
                return Redirect("/Admin");
            }
            return BadRequest();
        }

        [Route("Details/{id}")]
        [HttpGet]
        public ActionResult Details([FromRoute] int id)
        {
            var user = _userRepository.Get(g => g.Id == id).FirstOrDefault();
            return View(user);
        }

        [Route("Delete/{id}")]
        [HttpGet]
        public ActionResult Delete([FromRoute] int id)
        {
            var user = _userRepository.Get(g => g.Id == id).FirstOrDefault();
            return View(user);
        }

        [Route("Delete/{id}")]
        [HttpPost]
        public ActionResult Delete([FromRoute] User user)
        {
            var delete = _userRepository.Remove(user);
            return Redirect("/Admin");
        }
    }
}