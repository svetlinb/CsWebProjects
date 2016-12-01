using Events.Models;
using Events.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Services
{
    public class EventsService : BaseService, IEventsService
    {
        public IQueryable<Event> GetAllEvents()
        {
            return Context.Events
              .OrderBy(e => e.StartDate);
        }

        public IQueryable<Event> GetPublicEvents()
        {
            return Context.Events
              .OrderBy(e => e.StartDate)
              .Where(e => e.IsPublic);
        }

        public IQueryable<Event> GetPrivateEvents()
        {
            return Context.Events
              .OrderBy(e => e.StartDate)
              .Where(e => e.IsPublic == false);
        }

    }
}
