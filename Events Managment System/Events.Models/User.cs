using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Models
{
    public class User
    {
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }
        
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }

        [NotMapped]
        public string UserRoles { get; set; }
    }
}
