using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyMeetings.Models;

namespace MyMeetings.Controllers
{
    public class ModerationController : Controller
    {
        ApplicationUserManager UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: Moderation
        public ActionResult ShowUsers()
        {
            var result = UserManager.Users;
            return View(result);
        }
        public  ActionResult UserDetails(string id)
        {
            var result =   UserManager.Users.FirstOrDefault(user=>user.Id==id);
            return PartialView(result);
        }
    }
    }