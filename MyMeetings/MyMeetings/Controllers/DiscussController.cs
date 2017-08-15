using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using MyMeetings.Models;
namespace MyMeetings.Controllers
{
    public class DiscussController : Controller
    {

        PublicationChat chatModel;
        // GET: Discuss
        public ActionResult Index(string ChatId, int? page)
        {
            using (ApplicationDbContext DB = new ApplicationDbContext())
            {
                var currentUser = DB.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                chatModel = DB.Chats.Include(p => p.Publication).FirstOrDefault(c => c.Id == ChatId);
                ChatViewModels.ChatViewModel model = new ChatViewModels.ChatViewModel()
                {
                    DateOfCreate = chatModel.DateofCreate.ToString(),
                    Id = chatModel.Id,
                    Author = currentUser.FirstName + currentUser.SurName
                };
                return View(model);
            }
        }
        public ActionResult Messages(string chatId)
        {
            using (ApplicationDbContext DB = new ApplicationDbContext())
            {
                PublicationChat currentchat = DB.Chats.Include(m=>m.Messages).Include(u=>u.Users).FirstOrDefault(c=>c.Id==chatId);
                var allMessages = currentchat.Messages.OrderBy(c=>c.Date);
                List<ChatViewModels.ChatMessageModelView> chatMessages = new List<ChatViewModels.ChatMessageModelView>();
                foreach (var m in allMessages)
                {
                    ChatViewModels.ChatMessageModelView message = new ChatViewModels.ChatMessageModelView()
                    {
                        Author = m.User.FirstName + m.User.SurName,
                        Text = m.Text,
                        DateOfCreate = m.Date.ToString()
                    };
                    chatMessages.Add(message);
                }
                if (chatMessages.Count <= 0)
                {
                    return HttpNotFound();
                }
                else return PartialView("Messages", chatMessages);
            }
        }
        [HttpPost]
        public ActionResult AddMessage(string message, string chatId)
        {
            if (message != null && chatId != null)
            {
                using (ApplicationDbContext DB = new ApplicationDbContext())
                {
                    PublicationChat currentchat = DB.Chats.FirstOrDefault(c => c.Id == chatId);
                    ApplicationUser currentAppUser = DB.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                    currentAppUser.LastActivity = DateTime.Now;
                    DB.SaveChanges();
                    currentchat.Users.Add(currentAppUser);
                    ChatMessage newMesage = new ChatMessage() { User = currentAppUser, Text = message };

                    currentchat.Messages.Add(newMesage);
                    DB.SaveChanges();
                }
            }
            //var chatMessages=Messages(chatId);
            //if (chatMessages.Count <= 0)
            //{
            //    return HttpNotFound();
            //}
            //else return PartialView("Messages",chatMessages);
            return RedirectToAction("Messages", new { chatid = chatId });
        }
    }
}
