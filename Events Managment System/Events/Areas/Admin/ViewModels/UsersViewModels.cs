﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Events.Areas.Admin.ViewModels
{
    public class UsersViewModels
    {
        public string Id { get; set; }
        
        [Display(Name = "User Name *")]
        public string UserName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email *")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "User Roles *")]
        public string UserRoles { get; set; }
    }
}