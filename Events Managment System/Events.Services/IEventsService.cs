using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Services
{
    public interface IEventsService
    {
        IQueryable<Event> GetAllEvents();

        IQueryable<Event> GetPublicEvents();

        IQueryable<Event> GetPrivateEvents();
    }
}
