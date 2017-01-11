using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MyMeetings.Models
{
    public class ModerationViewModels
    {
        public class UserDetailsViewModel
        {
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string SurName { get; set; }
            public List<string> Roles { get; set; }
            public string DateOfRegistration { get; set; }
            public string Gender { get; set; }
            public string DateOfBirth { get; set; }
        }
        // GET: ModerationViewModels

    }
}