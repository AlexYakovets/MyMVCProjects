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
            IEnumerable<ApplicationUser> tempUsers = DBContext.Users;
            if (userName != "") tempUsers = tempUsers.Where(u => u.UserName.ToLower().Contains(userName.ToLower()));
            if (userFirstname != "")
                tempUsers =
                    tempUsers.Where(u => !String.IsNullOrEmpty(u.FirstName))
                        .Where(u => u.FirstName.ToLower().Contains(userFirstname.ToLower()));
            if (userSurname != "")
                tempUsers =
                    tempUsers.Where(u => !String.IsNullOrEmpty(u.SurName))
                        .Where(u => u.SurName.ToLower().Contains(userSurname.ToLower()));
            tempUsers = tempUsers.OrderBy(m => m.UserName).OrderBy(m => m.FirstName).OrderBy(m => m.SurName);
            List<ApplicationUser> users = tempUsers.ToList();
            return PartialView(users);
        }

        public string Get()
        {
            return JsonConvert.SerializeObject(DBContext.Users);
        }
    }
}