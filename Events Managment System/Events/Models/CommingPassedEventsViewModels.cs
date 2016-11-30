namespace Events.Models
{
    using System.Collections.Generic;

    public class CommingPassedEventsViewModels
    {
        public IEnumerable<EventViewModels> CommingEvents { get; set; }

        public IEnumerable<EventViewModels> PassedEvents { get; set; }
    }
}