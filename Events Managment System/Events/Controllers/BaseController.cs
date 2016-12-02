using Events.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Events.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ApplicationDbContext _context = new ApplicationDbContext();

        protected ApplicationDbContext Context
        {
            get
            {
                return _context;
            }
        }

        public bool IsAdmin()
        {
            var currentUser = this.User.Identity.GetUserId();
            var isAdmin = (currentUser != null && this.User.IsInRole("Admin"));

            return isAdmin;
        }
    }
}