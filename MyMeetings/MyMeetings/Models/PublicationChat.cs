using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyMeetings.Models
{
    public class PublicationChat
    {
        [Key]
        [ForeignKey("Publication")]
        public string Id { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<ChatMessage> Messages { get; set; }
        public DateTime DateofCreate { get; set; }
        public Publication Publication { get; set; }
    
        public PublicationChat()
        {
            DateofCreate = DateTime.Now;
            Users = new List<ApplicationUser>();
            Messages = new List<ChatMessage>();
        }

    }
    public class ChatMessage
    {
        [Key]
        public string Id { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public ChatMessage()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now;
        }
    }
}