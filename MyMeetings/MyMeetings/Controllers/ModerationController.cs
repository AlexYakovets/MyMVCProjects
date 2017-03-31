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
using Microsoft.AspNet.Identity.Owin;
using MyMeetings.Models;
using Newtonsoft.Json;
using PagedList;

namespace MyMeetings.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ModerationController : Controller
    {
        ApplicationUserManager UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        //POST
       
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
            ApplicationRoleManager roleManager = HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            ModerationViewModels.UserDetailsViewModel currentDetails=new ModerationViewModels.UserDetailsViewModel();
            var result = UserManager.Users.FirstOrDefault(user => user.Id == id);
            currentDetails.Email = result.Email??"";
            currentDetails.FirstName = result.FirstName;
            currentDetails.SurName = result.SurName;
            currentDetails.DateOfBirth = result.DateOfBirth.ToShortDateString()??"";
            currentDetails.DateOfRegistration = result.DateOfRegistration.ToShortDateString();
            currentDetails.Gender = result.Gender;
            List<IdentityUserRole> userRoles=new List<IdentityUserRole>();
            List<string> userRoleList=new List<string>();
            foreach (var role in roleManager.Roles)
            {
                //var userRole = role.Users.FirstOrDefault(user => user.UserId == id);
                //var roleName = DBContext.Roles.Where(r => r.Id == userRole.RoleId);
                //if(roleName)currentDetails.Roles.Add(roleName.First().Name);
                foreach (var u in role.Users)
                {
                    if (u.UserId==id) userRoleList.Add(role.Name);
                    
                }
            }
            currentDetails.Roles=userRoleList;
            return PartialView(currentDetails);
        }

      
    }
    }