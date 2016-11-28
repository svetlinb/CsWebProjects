using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Data
{
    public class Comment
    {
        public Comment()
        {
            this.Date = new DateTime();
        }

        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
