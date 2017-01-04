using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
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
        public string Get(string id) { 
         
            List<RolesViewModels.UserRoles>returnData=new List<RolesViewModels.UserRoles>();
            IOwinContext context = new OwinContext();
            ApplicationDbContext contextDB = ApplicationDbContext.Create();

            ApplicationUserManager manager =
                new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            AccountController accountManager = new AccountController(manager,
                new ApplicationSignInManager(manager, context.Authentication));
            foreach (var r in contextDB.Roles)
            {
                RolesViewModels.UserRoles role=new RolesViewModels.UserRoles();
                role.Role = r;
                role.IsAvaible = false;
                returnData.Add(role);
            }
            List<IdentityUserRole> listOfRoles = accountManager.GetRoleByUserId(id);
            foreach (var l  in listOfRoles)
            {
                var avaibleRole=contextDB.Roles.FirstOrDefault(role => role.Id == l.RoleId);
                returnData.Find(model => model.Role==avaibleRole).IsAvaible = true;
            }
            return JsonConvert.SerializeObject(returnData);
            
        }
    }
}
