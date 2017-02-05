using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyMeetings.Models
{
    public class Publication
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Text { get; set; }          
        public DateTime DateTimeOfPublication { get; set; }
        public DateTime DateOfMeeting { get; set; }
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }
        [InverseProperty("Subscriptions")]
        public ICollection<ApplicationUser> Subscriptions { get; set; }

        public Publication()
        {
            Subscriptions = new List<ApplicationUser>();
            DateTimeOfPublication = DateTime.Now;
            Id = Guid.NewGuid().ToString();
        }
    }

}