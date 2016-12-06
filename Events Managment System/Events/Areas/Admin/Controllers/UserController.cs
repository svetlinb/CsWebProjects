using Events.Areas.Admin.ViewModels;
using Events.Data;
using Events.Models;
using Events.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events;
using Events.Controllers;

namespace Events.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : BaseController
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
            ViewBag.UserRoles = (IEnumerable<SelectListItem>)this.service.GetUserRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsersViewModels model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                User user = new User()
                {
                    UserName = model.UserName,
                    FullName = model.FullName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserRoles = model.UserRoles
                };

                this.service.AddUser(user);

                return RedirectToAction("Index");
            }

            ViewBag.UserRoles = (IEnumerable<SelectListItem>)this.service.GetUserRoles();

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var user = this.service.GetUserById(id);
            var userRole = this.service.GetUserRoleById(id);

            if (user == null || userRole == null)
            {
                return RedirectToAction("Index");
            }

            UsersViewModels userView = new UsersViewModels()
            {
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserRoles = userRole
            };

            ViewBag.userRoles = (IEnumerable<SelectListItem>)this.service.GetUserRoles();

            return View(userView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, UsersViewModels model)
        {
            if ((model != null && id != null) && this.ModelState.IsValid)
            {
                User editedUser = new User()
                {
                    Id = id,
                    UserName = model.UserName,
                    FullName = model.FullName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserRoles = model.UserRoles
                };

                this.service.EditUser(id, editedUser);

                return RedirectToAction("Index");
            }

            ViewBag.userRoles = (IEnumerable<SelectListItem>)this.service.GetUserRoles();

            return View(model);
        }

        public ActionResult Delete(string id)
        {
            this.service.DeleteUser(id);

            return RedirectToAction("Index");
        }
    }
}