using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events.Services;
using Events.Models;
using Events.Data;
using Microsoft.AspNet.Identity;
using PagedList;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Events.Controllers
{
    public class HomeController : BaseController
    {
        IEventsService eventService;

        public HomeController(IEventsService service)
        {
            this.eventService = service;
        }

        [HttpGet]
        public ActionResult Index(int page=1, int pageSize=6)
        {
            var publicEvents = this.eventService.GetPublicEvents();
            IPagedList<EventViewModels> model = null;

            if (publicEvents != null && publicEvents.Any())
            {
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

                IEnumerable<EventViewModels> events = mappedEvents.OrderBy(e => e.StartDate);
                model = events.ToPagedList(page, pageSize);
            }

            return View("Index", model);
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

            return View("Contact");
        }
    }
}