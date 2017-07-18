using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMeetings.Models;
namespace MyMeetings.Controllers
{
    public class DiscussController : Controller
    {
        static ApplicationDbContext DB = new ApplicationDbContext();
         PublicationChat chatModel;
        // GET: Discuss
        public ActionResult Index(string ChatId,string UserId)
        {
            chatModel = DB.Chats.FirstOrDefault(c => c.ChatId == ChatId);
            var currentUser = DB.Users.FirstOrDefault(u => u.Id == UserId);
            try
            {
                if (chatModel == null) chatModel = new PublicationChat();

                //оставляем только последние 10 сообщений
                if (chatModel.Messages.Count > 100)
                    chatModel.Messages.RemoveRange(0, 90);

                // если обычный запрос, просто возвращаем представление
                if (!Request.IsAjaxRequest())
                {
                    return View(chatModel);
                }
                // если передан параметр logOn
                else if (UserId != null && UserId!="")
                {
                    //проверяем, существует ли уже такой пользователь
                    if (chatModel.Users.FirstOrDefault(u => u.User.Id == UserId) != null)
                    {
                        throw new Exception("Пользователь с таким ником уже существует");
                    }
                    else
                    {
                        // добавляем в список нового пользователя
                        chatModel.Users.Add(new ChatUser()
                        {
                           
                            User = currentUser,
                            LoginTime = DateTime.Now,
                            LastPing = DateTime.Now
                        });

                        // добавляем в список сообщений сообщение о новом пользователе
                        chatModel.Messages.Add(new ChatMessage()
                        {
                            Text = currentUser.FirstName+currentUser.SurName + " вошел в чат",
                            Date = DateTime.Now
                        });
                    }

                    return View("ChatRoom", chatModel);
                }
                else
                {
                    ChatUser currentChatUser = chatModel.Users.FirstOrDefault(u => u.User == currentUser);

                    currentChatUser.LastPing = DateTime.Now;

                    List<ChatUser> toRemove = new List<ChatUser>();
                    foreach (Models.ChatUser usr in chatModel.Users)
                    {
                        TimeSpan span = DateTime.Now - usr.LastPing;
                        if (span.TotalSeconds > 120)
                            toRemove.Add(usr);
                    }
                    foreach (ChatUser u in toRemove)
                    {
                        LogOff(u);
                    }

                    // добавляем в список сообщений новое сообщение
                    //if (!string.IsNullOrEmpty(chatMessage))
                    //{
                    //    chatModel.Messages.Add(new ChatMessage()
                    //    {
                    //        User = currentChatUser,
                    //        Text = chatMessage,
                    //        Date = DateTime.Now
                    //    });
                    //}

                   return PartialView("History", chatModel);
                }
            }
            catch (Exception ex)
            {
                //в случае ошибки посылаем статусный код 500
                Response.StatusCode = 500;
                return Content(ex.Message);
            }
        }
        public void LogOff(ChatUser user)
        {
            chatModel.Users.Remove(user);
            chatModel.Messages.Add(new ChatMessage()
            {
                Text = user.User.FirstName+" "+user.User.SurName + " покинул чат.",
                Date = DateTime.Now
            });
        }
    }
    }
