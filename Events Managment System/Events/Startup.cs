using Events.Data;
using Events.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(Events.Startup))]
namespace Events
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
            ApplicationDbContext context = new ApplicationDbContext();

            if (!context.Users.Any())
            {
                this.CreateRolesandUsers(context);
                this.CreateSomeTestEvents(context);
            }
        }


        private void CreateSomeTestEvents(ApplicationDbContext context)
        {
            context.Events.Add(new Event()
            {
                Title = "Chill Out Party",
                StartDate = DateTime.Now.Date.AddDays(5),
                Duration = TimeSpan.FromHours(5.0),
                Author = context.Users.FirstOrDefault(),
                Comments = new HashSet<Comment>()
                {
                    new Comment() {Text = "Some Comment" },
                    new Comment() {Text = "Some User Comment", Author = context.Users.FirstOrDefault() }
                },
                IsPublic = true,
            });

            context.Events.Add(new Event()
            {
                Title = "Dire straits Sofia",
                StartDate = DateTime.Now.Date.AddDays(3),
                Duration = TimeSpan.FromHours(5.0),
                Author = context.Users.FirstOrDefault(),
                Comments = new HashSet<Comment>()
                {
                    new Comment() {Text = "The most eagerly anticipated release from Dire Straits -- their seminal live concert" },
                    new Comment() {Text = "Dire Straits were a British rock band that formed in Deptford, London, in 1977 by Mark Knopfler his younger brother David Knopfler John Illsley and Pick Withers", Author = context.Users.FirstOrDefault() }
                },
                IsPublic = true,
            });

            context.Events.Add(new Event()
            {
                Title = "Dire straits Plovdiv",
                StartDate = DateTime.Now.Date.AddDays(3),
                Duration = TimeSpan.FromHours(5.0),
                Author = context.Users.FirstOrDefault(),
                Comments = new HashSet<Comment>()
                {
                    new Comment() {Text = "The most eagerly anticipated release from Dire Straits -- their seminal live concert" },
                    new Comment() {Text = "Dire Straits were a British rock band that formed in Deptford, London, in 1977 by Mark Knopfler his younger brother David Knopfler John Illsley and Pick Withers", Author = context.Users.FirstOrDefault() }
                },
                IsPublic = true,
            });

            context.Events.Add(new Event()
            {
                Title = "Dire straits Varna",
                StartDate = DateTime.Now.Date.AddDays(3),
                Duration = TimeSpan.FromHours(5.0),
                Author = context.Users.FirstOrDefault(),
                Comments = new HashSet<Comment>()
                {
                    new Comment() {Text = "The most eagerly anticipated release from Dire Straits -- their seminal live concert" },
                    new Comment() {Text = "Dire Straits were a British rock band that formed in Deptford, London, in 1977 by Mark Knopfler his younger brother David Knopfler John Illsley and Pick Withers", Author = context.Users.FirstOrDefault() }
                },
                IsPublic = true,
            });

            context.Events.Add(new Event()
            {
                Title = "Dire straits Ruse",
                StartDate = DateTime.Now.Date.AddDays(3),
                Duration = TimeSpan.FromHours(5.0),
                Author = context.Users.FirstOrDefault(),
                Comments = new HashSet<Comment>()
                {
                    new Comment() {Text = "The most eagerly anticipated release from Dire Straits -- their seminal live concert" },
                    new Comment() {Text = "Dire Straits were a British rock band that formed in Deptford, London, in 1977 by Mark Knopfler his younger brother David Knopfler John Illsley and Pick Withers", Author = context.Users.FirstOrDefault() }
                },
                IsPublic = true,
            });

            context.Events.Add(new Event()
            {
                Title = "Dire straits Burgas",
                StartDate = DateTime.Now.Date.AddDays(3),
                Duration = TimeSpan.FromHours(5.0),
                Author = context.Users.FirstOrDefault(),
                Comments = new HashSet<Comment>()
                {
                    new Comment() {Text = "The most eagerly anticipated release from Dire Straits -- their seminal live concert" },
                    new Comment() {Text = "Dire Straits were a British rock band that formed in Deptford, London, in 1977 by Mark Knopfler his younger brother David Knopfler John Illsley and Pick Withers", Author = context.Users.FirstOrDefault() }
                },
                IsPublic = true,
            });

            context.Events.Add(new Event()
            {
                Title = "WDSF WORLD OPEN 2016",
                StartDate = DateTime.Now.Date.AddDays(5),
                Duration = TimeSpan.FromHours(5.0),
                Author = context.Users.FirstOrDefault(),
                Comments = new HashSet<Comment>()
                {
                    new Comment() {Text = "The most eagerly anticipated release from Dire Straits -- their seminal live concert" },
                    new Comment() {Text = "Additional information: Tickets will be reserved for 20 minutes when you add them to your shopping cart.", Author = context.Users.FirstOrDefault() }
                },
                IsPublic = true,
            });


            context.Events.Add(new Event()
            {
                Title = "Snooker European Open 2016",
                StartDate = DateTime.Now.Date.AddDays(-1),
                Duration = TimeSpan.FromHours(5.0),
                IsPublic = true,
            });

            context.Events.Add(new Event()
            {
                Title = "Snooker European Open2 2016",
                StartDate = DateTime.Now.Date.AddDays(-1),
                Duration = TimeSpan.FromHours(5.0),
                IsPublic = true,
            });

            context.Events.Add(new Event()
            {
                Title = "Snooker European Open3 2016",
                StartDate = DateTime.Now.Date.AddDays(-1),
                Duration = TimeSpan.FromHours(5.0),
                IsPublic = true,
            });

            context.Events.Add(new Event()
            {
                Title = "Snooker European Open4 2016",
                StartDate = DateTime.Now.Date.AddDays(-1),
                Duration = TimeSpan.FromHours(5.0),
                IsPublic = true,
            });

            context.Events.Add(new Event()
            {
                Title = "Snooker European Open5 2016",
                StartDate = DateTime.Now.Date.AddDays(-1),
                Duration = TimeSpan.FromHours(5.0),
                IsPublic = true,
            });

            context.SaveChanges();
        }

        //In this method we will create default User roles and Admin user for login
        private void CreateRolesandUsers(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            userManager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website 
                string userPWD = "123123";
                var user = new ApplicationUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";
                user.FullName = "Admin admin";

                var chkUser = userManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("User"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("Editor"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Editor";
                roleManager.Create(role);
            }
        }
    }
}
