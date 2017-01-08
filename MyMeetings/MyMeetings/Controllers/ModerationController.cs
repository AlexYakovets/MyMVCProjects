using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyMeetings.Models;
using Newtonsoft.Json;
using PagedList;

namespace MyMeetings.Controllers
{
    public class ModerationController : Controller
    {
        ApplicationDbContext DBContext = new ApplicationDbContext();
        ApplicationUserManager UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        // GET: Moderation
        public ActionResult ShowUsers(string userName,int? page)
        {
            List<ApplicationUser> result = new List<ApplicationUser>();
            if (userName.IsNullOrWhiteSpace())
            {
                result = UserManager.Users.ToList();
            }
            else
            {
                var users = UserManager.Users.Where(a => a.UserName.Contains(userName)).ToList();
            }
            int pagesize = 3;
            int pagenumber = (page ?? 1);
            return View(result.ToPagedList(pagenumber,pagesize));
        }

        public ActionResult UserDetails(string id)
        {
            var result = UserManager.Users.FirstOrDefault(user => user.Id == id);
            return PartialView(result);
        }

      
    }
    }