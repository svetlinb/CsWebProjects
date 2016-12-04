using Events.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Services
{
    public class UserService : BaseService
    {

        public IQueryable<ApplicationUser> GetAllUsers()
        {
            return Context.Users.OrderBy(u => u.FullName);
        }
    }
}
