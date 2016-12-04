using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events.Services;
using Events.Models;
using Events.Data;
using Microsoft.AspNet.Identity;

namespace Events.Controllers
{
    public class HomeController : BaseController
    {
        EventsService eventService;

        public HomeController(EventsService service)
        {
            this.eventService = service;
        }

        public ActionResult Index()
        {
            var publicEvents = this.eventService.GetPublicEvents();
            var mappedEvents = publicEvents.Select(e => new EventViewModels()
            {
                Id = e.Id,
                Title = e.Title,
                StartDate = e.StartDate,
                Duration = e.Duration,
                Author = e.Author.FullName,
                Description = e.Description,
                Location = e.Location,
            });

            var upcomingEvents = mappedEvents.Where(e => e.StartDate > DateTime.Now);

            return View(new CommingPassedEventsViewModels()
            {
                CommingEvents = upcomingEvents
            });
        }

        public ActionResult EventDetails(int id)
        {
            var eventService = new EventsService();
            var isAdmin = this.IsAdmin();
            var currentUserId = this.User.Identity.GetUserId();
            var events = this.eventService.GetAllEvents();

            var eventDetails = events
                .Where(e => e.Id == id)
                .Select(e => new EventDetailsViewModel()
                {
                    Id = e.Id,
                    Description = e.Description,
                    Comments = e.Comments.AsQueryable().Select(c => new CommentViewModels()
                    {
                        Text = c.Text,
                        Author = c.Author.FullName
                    }),
                    AuthorId = e.Author.Id
                }).FirstOrDefault();


            this.ViewBag.hasPermission = (isAdmin || (eventDetails != null && eventDetails.AuthorId != null && currentUserId == eventDetails.AuthorId));

            return this.PartialView("_AjaxEventDetails", eventDetails);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}