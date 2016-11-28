﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Data
{
    public class Event
    {
        public Event()
        {
            this.StartDate = new DateTime();
            this.IsPublic = true;
            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public TimeSpan? Duration { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public bool IsPublic { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
