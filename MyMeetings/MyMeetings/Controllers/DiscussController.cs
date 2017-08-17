using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using MyMeetings.Models;
using PagedList;
namespace MyMeetings.Controllers
{
    public class DiscussController : Controller
    {

        PublicationChat chatModel;
        // GET: Discuss
        public ActionResult Index(string chatId, int? page)
        {
            
            using (ApplicationDbContext DB = new ApplicationDbContext())
            {
                PublicationChat currentchat = DB.Chats.Include(m => m.Messages).Include(u => u.Users).FirstOrDefault(c => c.Id == chatId);
                var allMessages = currentchat.Messages.OrderBy(c => c.Date);
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
                int pagesize = 3;
                int pagenumber = (page ?? 1);
                ViewBag.ChatId = chatId;
                return PartialView("Index", chatMessages.ToPagedList(chatMessages.Count / pagesize, pagesize));

            }
                //using (ApplicationDbContext DB = new ApplicationDbContext())
                //{
                //    var currentUser = DB.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                //    chatModel = DB.Chats.Include(p => p.Publication).Include(m => m.Messages).FirstOrDefault(c => c.Id == ChatId);
                //    var allMessages = chatModel.Messages.OrderBy(c => c.Date);
                //    List<ChatViewModels.ChatMessageModelView> chatMessages = new List<ChatViewModels.ChatMessageModelView>();
                //    foreach (var m in allMessages)
                //    {
                //        ChatViewModels.ChatMessageModelView message = new ChatViewModels.ChatMessageModelView()
                //        {
                //            Author = m.User.FirstName + m.User.SurName,
                //            Text = m.Text,
                //            DateOfCreate = m.Date.ToString()
                //        };
                //        chatMessages.Add(message);
                //    }
                //    ChatViewModels.ChatViewModel model = new ChatViewModels.ChatViewModel()
                //    {
                //        Messages=chatMessages,
                //        DateOfCreate = chatModel.DateofCreate.ToString(),
                //        Id = chatModel.Id,
                //        Author = currentUser.FirstName + currentUser.SurName
                //    };
                //    return View(model);
                //}
            }
        //public ActionResult Messages(string chatId, int? page)
        //{
        //    bool flag;
            
        //    using (ApplicationDbContext DB = new ApplicationDbContext())
        //    {
        //        PublicationChat currentchat = DB.Chats.Include(m => m.Messages).Include(u => u.Users).FirstOrDefault(c => c.Id == chatId);
        //        var allMessages = currentchat.Messages.OrderBy(c => c.Date);
        //        List<ChatViewModels.ChatMessageModelView> chatMessages = new List<ChatViewModels.ChatMessageModelView>();
        //        foreach (var m in allMessages)
        //        {
        //            ChatViewModels.ChatMessageModelView message = new ChatViewModels.ChatMessageModelView()
        //            {
        //                Author = m.User.FirstName + m.User.SurName,
        //                Text = m.Text,
        //                DateOfCreate = m.Date.ToString()
        //            };
        //            chatMessages.Add(message);
        //        }
        //        if (chatMessages.Count <= 0)
        //        {
        //            return HttpNotFound();
        //        }
        //        int pagesize = 3;
        //        int pagenumber = (page ?? 1);
        //        ViewBag.ChatId = chatId;
        //        return PartialView("Messages", chatMessages.ToPagedList(pagenumber, pagesize));
        //    }

        //}
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
            return RedirectToAction("Index", new { chatid = chatId });
        }
    }
}
