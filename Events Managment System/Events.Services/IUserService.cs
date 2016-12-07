using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Events.Services
{
    public interface IUserService
    {
        IQueryable<ApplicationUser> GetAllUsers();
        void AddUser(User user);
        IEnumerable<SelectListItem> GetUserRoles();
        ApplicationUser GetUserById(string id);
        string GetUserRoleById(string id);
        void EditUser(string id, User editedUser);
        void DeleteUser(string id);
    }
}
