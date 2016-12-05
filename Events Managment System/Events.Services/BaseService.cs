using Events.Data;
using Events.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Services
{
    public abstract class BaseService : IBaseService
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public ApplicationDbContext Context
        {
            get
            {
                return _context;
            }
        }

        public RoleManager<IdentityRole> roleManager
        {
            get
            {
                return new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
            }
        }

        public UserManager<ApplicationUser> userManager
        {
            get
            {
                return new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            }
        }
    }
}
