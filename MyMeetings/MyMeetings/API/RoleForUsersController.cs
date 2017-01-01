using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MyMeetings.Controllers;
using MyMeetings.Models;
using Newtonsoft.Json;

namespace MyMeetings.API
{
    public class RoleForUsersController : ApiController
    {
        public string Get(string id)
        {
            IOwinContext context = new OwinContext();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            AccountController accountManager=new AccountController(manager, new ApplicationSignInManager(manager, context.Authentication));
            List<IdentityUserRole> listOfRoles = accountManager.GetRoleByUserId(id);
            return JsonConvert.SerializeObject(listOfRoles);
        }
    }
}
