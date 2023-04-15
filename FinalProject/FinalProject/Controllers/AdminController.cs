﻿using DatabaseConnector;
using DatabaseConnector.DTO;
using DatabaseConnector.Extensions;
using FinalProject.DataBaseContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NuGet.Protocol.Core.Types;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        [Route("Edit")]
        [HttpGet]
        public ActionResult Edit()
        {
            var user = _userRepository.Get();

            return View(user);
        }

        [Route("Edit")]
        [HttpPost]
        public ActionResult Edit(User user)
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
                _userRepository.Update(updated);
                return RedirectToAction("Details");
            }
            return BadRequest();
        }
    }
}