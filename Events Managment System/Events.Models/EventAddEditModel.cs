using System;
using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class EventAddEditModel
    {
        [Required]
        [MaxLength(200)]
        [Display(Name = "Title *")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date and Time *")]
        public DateTime? StartDate { get; set; }

        public TimeSpan? Duration { get; set; }

        public string AuthorId { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public bool IsPublic { get; set; }

        public static EventAddEditModel CreateFromEvent(Event e)
        {
            return new EventAddEditModel()
            {
                Title = e.Title,
                StartDate = e.StartDate,
                Duration = e.Duration,
                Location = e.Location,
                Description = e.Description,
                IsPublic = e.IsPublic
            };
        }
    }
}
