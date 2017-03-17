using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMeetings.Models
{
    public class PublicationCategory
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Publication> Publications{ get; set; } 
        public PublicationCategory()
        {
            Id = Guid.NewGuid().ToString();
            Publications = new List<Publication>();
        }
    }
}