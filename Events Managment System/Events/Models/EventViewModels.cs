﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Models
{
    public class EventViewModels
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public DateTime? StartDate { get; set; }

        public TimeSpan? Duration { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }
    }
}