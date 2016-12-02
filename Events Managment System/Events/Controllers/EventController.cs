using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Events.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace Events.Controllers
{
    [Authorize]
    public class EventController : BaseController
    {
        public ActionResult Show()
        {
            string currentUserId = this.User.Identity.GetUserId();
            var isAdmin = this.IsAdmin();
            var mappedEvents = this.Context.Events
                .Where(e => e.AuthorId == currentUserId || isAdmin)
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


            var upcomingEvents = mappedEvents.Where(e => e.StartDate >= DateTime.Now);

            return View(new CommingPassedEventsViewModels()
            {
                CommingEvents = upcomingEvents
            });
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
                
                this.Context.Entry(e).State = EntityState.Added;
                this.Context.Events.Add(e);
                this.Context.SaveChanges();
                
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

                this.Context.SaveChanges();
                
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

            this.Context.Events.Remove(eventToDelete);
            this.Context.SaveChanges();
           
            return this.RedirectToAction("Show");
        }

        private Event LoadEvent(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = this.IsAdmin();
            var eventToEdit = this.Context.Events
                .Where(e => e.Id == id)
                .FirstOrDefault(e => e.AuthorId == currentUserId || isAdmin);
            return eventToEdit;
        }
    }
}