using DatabaseConnector;
using FinalProject.DataBaseContext;
using FinalProject.Models.DTO;
using FinalProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("Admin")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AdministratorController : Controller
    {
        private readonly EFGenericRepository<User> _users;
        //private readonly AdministratorService _service; /*AdministratorService service*/
        public AdministratorController(EFGenericRepository<User> users)
        {
            _users = users;
            //_service = service;
        }
        public IActionResult Admin()
        {
            return View();
        }
        [Route("UsersList")]
        [HttpGet]
        public IActionResult AllUsers()
        {
            return View(MapUser(_users.Get()));
        }
        private IEnumerable<UserDto> MapUser(IEnumerable<User> array)
        {
            List<UserDto> result = new List<UserDto>(array.Count());
            foreach (var user in array)
            {
                result.Add(new UserDto()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    NickName = user.NickName,
                    Email = user.Email,
                    LastName = user.LastName,
                    Patronymic = user.Patronymic,
                    Birthday = user.Birthday,
                    IsBanned = user.IsBanned
                });
            }
            return result;
        }
    }
}
