using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using MyMeetings.Models;
using Newtonsoft.Json;

namespace MyMeetings.Controllers
{
    public class FindController : Controller
    {
        private ApplicationDbContext DBContext = new ApplicationDbContext();

        //// GET: Find
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //POST: /Account/FindUser
        [HttpPost]

        public ActionResult FindUsers(string userName, string userFirstname, string userSurname)
        {
            // Подготовка запросы
            IEnumerable<ApplicationUser> tempUsers = DBContext.Users;
            if (userName != "") tempUsers = tempUsers.Where(u => u.UserName.Contains(userName));
            if (userFirstname != "") tempUsers = tempUsers.Where(u => u.FirstName.Contains(userFirstname));
            if (userSurname != "") tempUsers = tempUsers.Where(u => u.SurName.Contains(userSurname));

            List<ApplicationUser> users = tempUsers.ToList();
            if (users.Count == 0)
            {
                return HttpNotFound();
            }
            return PartialView(users);
        }

        public string Get()
        {
            return JsonConvert.SerializeObject(DBContext.Users);
        }
    }
}