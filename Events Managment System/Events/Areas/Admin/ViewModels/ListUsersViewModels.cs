﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Events.Areas.Admin.ViewModels
{
    public class ListUsersViewModels
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}