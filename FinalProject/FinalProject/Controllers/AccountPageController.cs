using DatabaseConnector;
using FinalProject.DataBaseContext;
using FinalProject.Models;
using FinalProject.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.IdentityModel.Tokens.Jwt;

namespace FinalProject.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    public class AccountPageController : Controller
    {
        private readonly EFGenericRepository<User> _userRepository;

        public AccountPageController(EFGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /*[Route("/[action]")]
        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }*/


        [Route("/[action]")]
        [HttpGet]
        public IActionResult Details()
        {
            /*var user = _userRepository.Get();
            return View(user);
            var user = _userRepository.FindById(id);
            if (user is null)
                return NotFound();*/
            return View(new UserDto
            {
                Id = 2,
                LastName ="ewgwegewgwge",
                FirstName = "ewgwegewgwge",
                Patronymic = "ewgwegewgwge"
            });
        }
    }
}
