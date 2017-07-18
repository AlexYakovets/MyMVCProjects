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
        public string ChatId;
        public List<ChatUser> Users;
        public List<ChatMessage> Messages;
        public Publication Publication;
        public PublicationChat()
        {
            ChatId = Guid.NewGuid().ToString();
            Users = new List<ChatUser>();
            Messages = new List<ChatMessage>();
        }

    }
    public class ChatUser
    {
        public ApplicationUser User;
        public DateTime LoginTime;
        public DateTime LastPing;
    }
    public class ChatMessage
    {
        public ChatUser User;
        public DateTime Date = DateTime.Now;
        public string Text = "";
    }
}