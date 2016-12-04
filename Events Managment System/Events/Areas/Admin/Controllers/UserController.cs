using Events.Areas.Admin.ViewModels;
using Events.Data;
using Events.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Events.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private UserService service;
        public UserController(UserService _service)
        {
            this.service = _service;
        }

        public ActionResult Index()
        {
            IEnumerable<UsersViewModels> users = this.service.GetAllUsers()
                .OrderBy(u => u.FullName)
                .Select(u => new UsersViewModels()
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber
                });
            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UsersViewModels model)
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}