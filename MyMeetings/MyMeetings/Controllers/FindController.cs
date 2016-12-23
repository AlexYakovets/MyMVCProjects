using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using MyMeetings.Models;

namespace MyMeetings.Controllers
{
    public class FindController : Controller
    {
        ApplicationDbContext DBContext = new ApplicationDbContext();
        
        //// GET: Find
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //POST: /Account/FindUser
        [HttpPost]
        public ActionResult FindUser(string name)
        {
            IEnumerable < ApplicationUser > ListOfUsers= DBContext.Users;
            var users = ListOfUsers.Where(a => a.Email.Contains(name)).ToList();
            if (users.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView(users);
        }
    }
}