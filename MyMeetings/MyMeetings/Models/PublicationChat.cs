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
        public string ChatId { get; set; }
        public List<ChatUser> Users { get; set; }
        public List<ChatMessage> Messages { get; set; }
        public virtual Publication Publication { get; set; }
        [InverseProperty("Chat")] 
        public virtual ICollection<Publication> PublicationEntity { get; set; }
        public PublicationChat()
        {
            ChatId = Guid.NewGuid().ToString();
            Users = new List<ChatUser>();
            Messages = new List<ChatMessage>();
        }

    }
    public class ChatUser
    {
        [Key]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LastPing { get; set; }
    }
    public class ChatMessage
    {
        [Key]
        public string id { get; set; }
        public ChatUser User { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public ChatMessage()
        {
            id = Guid.NewGuid().ToString();
            Date = DateTime.Now;
        }
    }
}