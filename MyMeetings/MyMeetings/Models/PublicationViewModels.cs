using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace MyMeetings.Models
{
    public class PublicationViewModels
    {
        public class PartialPublication
        {
            public string PublicationId;
            public string ImagePath;
            public string Creator;
            public DateTime DateOfPublication;
            public string PublicationName;
            public DateTime DateOfMeet;
            public List<ApplicationUser> Subscribers;
            public string PublicationText;

            public bool IsSubscribedUser
            {
                get
                {
                    string userId = ClaimsPrincipal.Current.Identity.GetUserId();
                    return Subscribers.Any(user=>user.Id==userId);
                    //var Subscribings =
                    //    this.Subscribers.FirstOrDefault(user => user.Id == ClaimsPrincipal.Current.Identity.GetUserId());
                    //return Subscribings != null;

                }
            }
        }
        public class CreatePublicationModelView
        {
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
            [Display(Name = "Publication name")]
            public string PublicationName { get; set; }

            [Required]
            [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
            [Display(Name = "Description of publication")]
            public string Text { get; set; }

            [Display(Name = "Date of meeting")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
            public DateTime DateOfMeeting { get; set; }

            [Display(Name = "Category")]
            public string Category { get; set; }

            public SelectList Categories { get; set; }



        }
    }
    }
