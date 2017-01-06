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
using PagedList;

namespace MyMeetings.Controllers
{
    public class ModerationController : Controller
    {
        ApplicationUserManager UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: Moderation
        public ActionResult ShowUsers(int? page)
        {
            var result = UserManager.Users.ToList();
            int pagesize = 3;
            int pagenumber = (page ?? 1);
            return View(result.ToPagedList(pagenumber,pagesize));
        }
        public  ActionResult UserDetails(string id)
        {
            var result =   UserManager.Users.FirstOrDefault(user=>user.Id==id);
            return PartialView(result);
        }
    }
    }