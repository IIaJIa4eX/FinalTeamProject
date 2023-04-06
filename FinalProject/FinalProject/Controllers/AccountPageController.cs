using DatabaseConnector;
using FinalProject.DataBaseContext;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]")]
    public class AccountPageController : Controller
    {
        private readonly EFGenericRepository<User> _userRepository;
        public readonly Context _context;

        public AccountPageController(EFGenericRepository<User> userRepository, Context context)
        {
            _context = context;
            _userRepository = userRepository;
        }

        [Route("/[action]")]
        [HttpGet]
        public async Task<User> Details(Guid id)
        {
            return await _context.Users.FindAsync(id);







            //  return View(employee);
            //return View(new UserDto
            //{
            //    Id = user.Id,
            //    LastName = user.LastName,
            //    FirstName = user.FirstName,
            //    Patronymic = user.Patronymic,
            //    Birthday = user.Birthday,
            //});
        }
    }
}
