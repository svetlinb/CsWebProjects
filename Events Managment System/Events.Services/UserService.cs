using Events.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Events.Services
{
    public class UserService : BaseService
    {

        public IQueryable<ApplicationUser> GetAllUsers()
        {
            return Context.Users.OrderBy(u => u.FullName);
        }
        public SelectList GetUserRoles()
        {
            return new SelectList(Context.Roles.Where(r => !r.Name.Contains("Admin")).ToList(),"Name", "Name");
        }

        public void AddUser(User model)
        {
            var defaultUserPassword = "qwerty";

            var user = new ApplicationUser()
            {
                UserName = model.Email,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            var chkUser = this.userManager.Create(user, defaultUserPassword);
            
            if (chkUser.Succeeded)
            {
                var result = this.userManager.AddToRole(user.Id, model.UserRoles);
            }else
            {
                throw new ApplicationException("Unable to create user.");
            }
        }

        public ApplicationUser GetUserById(string id)
        {
            var user = this.userManager.FindById(id);

            return user;
        }

        public string GetUserRoleById(string id)
        {
            var user = this.userManager.GetRoles(id);

            return user.FirstOrDefault();
        }

        public void DeleteUser(string id)
        {
            var user = this.userManager.FindById(id);
            this.userManager.Delete(user);
        }
    }
}
