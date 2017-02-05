using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMeetings.Models
{
    public class PublicationViewModels
    {
        public class PartialPublication
        {
            public string ImagePath;
            public string Creator;
            public DateTime DateOfPublication;
            public string PublicationName;
            public DateTime DateOfMeet;
            public List<ApplicationUser> Subscribers;
            public string PublicationText;

        }
        public class CreateViewModel
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
        }
    }
    }
