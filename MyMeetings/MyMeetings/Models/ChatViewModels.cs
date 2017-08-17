using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMeetings.Models
{
    public class ChatViewModels
    {
        public class ChatViewModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Author{ get; set; }
            public string DateOfCreate { get; set; }
            public IEnumerable<ChatMessageModelView> Messages { get; set; }
        }
        public class ChatMessageModelView
        {
            public string Text { get; set; }
            public string Author { get; set; }
            public string DateOfCreate { get; set; }
        }

    }
}