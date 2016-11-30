using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events.Services;
using Events.Models;
using Events.Data;

namespace Events.Controllers
{
    public class HomeController : BaseController
    {
        
        public ActionResult Index()
        {
            var EventService = new EventsService();
            var publicEvents = EventService.GetPublicEvents();
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
            var passedEvents = mappedEvents.Where(e => e.StartDate <= DateTime.Now);
            return View(new CommingPassedEventsViewModels()
            {
                CommingEvents = upcomingEvents,
                PassedEvents = passedEvents
            });
        }

        public void EventDetails()
        {

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