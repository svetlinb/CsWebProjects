using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Events.Services;

namespace Events.Controllers
{
    [Authorize]
    public class EventController : BaseController
    {
        EventsService eventService;

        public EventController(EventsService service)
        {
            this.eventService = service;
        }
        public ActionResult Show()
        {
            string currentUserId = this.User.Identity.GetUserId();
            var isAdmin = this.IsAdmin();
            var allEvents = this.eventService.GetAllEvents();
            var mappedEvents = allEvents
                .Where(e => e.IsPublic || isAdmin || (e.AuthorId == currentUserId))
                .OrderBy(e => e.StartDate)
                .Select(e => new EventViewModels()
                {
                    Id = e.Id,
                    Title = e.Title,
                    StartDate = e.StartDate,
                    Duration = e.Duration,
                    Author = e.Author.FullName,
                    Description = e.Description,
                    Location = e.Location,
                });


            IEnumerable<EventViewModels> events = mappedEvents;

            return View(events);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventAddEditModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var e = new Event()
                {
                    AuthorId = this.User.Identity.GetUserId(),
                    Title = model.Title,
                    StartDate = model.StartDate,
                    Duration = model.Duration,
                    Description = model.Description,
                    Location = model.Location,
                    IsPublic = model.IsPublic
                };
                
                this.eventService.Context.Entry(e).State = EntityState.Added;
                this.eventService.Context.Events.Add(e);
                this.eventService.Context.SaveChanges();
                
                return this.RedirectToAction("Show");
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var eventToEdit = this.LoadEvent(id);
            if (eventToEdit == null)
            {
                return this.RedirectToAction("Show");
            }

            var model = EventAddEditModel.CreateFromEvent(eventToEdit);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EventAddEditModel model)
        {
            var eventToEdit = this.LoadEvent(id);
            if (eventToEdit == null)
            {
                return this.RedirectToAction("Show");
            }

            if (model != null && this.ModelState.IsValid)
            {
                eventToEdit.Title = model.Title;
                eventToEdit.StartDate = model.StartDate;
                eventToEdit.Duration = model.Duration;
                eventToEdit.Description = model.Description;
                eventToEdit.Location = model.Location;
                eventToEdit.IsPublic = model.IsPublic;

                this.eventService.Context.SaveChanges();
                
                return this.RedirectToAction("Show");
            }

            return this.View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var eventToDelete = this.LoadEvent(id);
            if (eventToDelete == null)
            {
                return this.RedirectToAction("Show");
            }

            var model = EventAddEditModel.CreateFromEvent(eventToDelete);
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EventAddEditModel model)
        {
            var eventToDelete = this.LoadEvent(id);
            if (eventToDelete == null)
            {
                return this.RedirectToAction("Show");
            }

            this.eventService.Context.Events.Remove(eventToDelete);
            this.eventService.Context.SaveChanges();
           
            return this.RedirectToAction("Show");
        }

        private Event LoadEvent(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = this.IsAdmin();
            var eventToEdit = this.eventService.Context.Events
                .Where(e => e.Id == id)
                .FirstOrDefault(e => e.AuthorId == currentUserId || isAdmin);
            return eventToEdit;
        }
    }
}