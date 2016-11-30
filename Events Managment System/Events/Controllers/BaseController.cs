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
        protected ApplicationDbContext Context
        {
            get
            {
                return new ApplicationDbContext();
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