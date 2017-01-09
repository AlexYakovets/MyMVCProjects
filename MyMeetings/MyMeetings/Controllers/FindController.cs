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
            IEnumerable<ApplicationUser> ListOfUsers = DBContext.Users.ToList();
            //    var users =
            //        ListOfUsers.Where(
            //            a =>
            //                (!userName.IsNullOrWhiteSpace() ? a.UserName.Contains(userName) : true)).Where(a=>(!userFirstname.IsNullOrWhiteSpace() ? a.FirstName.Contains(userFirstname) : true)).Where(a=>(!userSurname.IsNullOrWhiteSpace() ? a.SurName.Contains(userSurname) : true)).ToList();

            if (userName.IsNullOrWhiteSpace() == false)
            {

                ListOfUsers = ListOfUsers.Where(user => user.UserName.Contains(userName)).ToList();
    }
            if (userFirstname.IsNullOrWhiteSpace() == false)
            {

                ListOfUsers = ListOfUsers.Where(user => user.FirstName.Contains(userFirstname)).ToList();
}
            if (userSurname.IsNullOrWhiteSpace() == false)
            {

                ListOfUsers = ListOfUsers.Where(user => user.SurName.Contains(userSurname)).ToList();
            }

            if (ListOfUsers.Count<=0)
            {
                return HttpNotFound();
            }
            return PartialView(ListOfUsers);
        }

        public string Get()
        {
            return JsonConvert.SerializeObject(DBContext.Users);
        }
    }
}