using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
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
        public ApplicationUserManager userManager;
        public AccountController accountManager;
        public string Get(string id)
        {
            List<RolesViewModels.UserRoles> returnData = new List<RolesViewModels.UserRoles>();
            IOwinContext context = new OwinContext();
            ApplicationDbContext contextDb = ApplicationDbContext.Create();
            if(userManager==null)
             userManager =
                new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            if (accountManager==null)
                accountManager = new AccountController(userManager,
                new ApplicationSignInManager(userManager, context.Authentication));
            foreach (var r in contextDb.Roles)
            {
                RolesViewModels.UserRoles role = new RolesViewModels.UserRoles();
                role.Role = r;
                role.IsAvaible = false;
                returnData.Add(role);
            }
            List<IdentityUserRole> listOfRoles = accountManager.GetRoleByUserId(id);
            foreach (var l  in listOfRoles)
            {
                var avaibleRole = contextDb.Roles.FirstOrDefault(role => role.Id == l.RoleId);
                returnData.Find(model => model.Role == avaibleRole).IsAvaible = true;
            }
            return JsonConvert.SerializeObject(returnData);
        }

        // POST api/<controller>
        public HttpResponseMessage Put(string id, [FromBody] IEnumerable<RolesViewModels.ChangeRole> data)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Gone);
            ApplicationDbContext contextDb = new ApplicationDbContext();
             if(userManager==null)
                userManager =
                new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            foreach (var roleJson in data)
            {
                IdentityRole role = contextDb.Roles.FirstOrDefault(model => model.Id == roleJson.RoleId);
                if (role == null)
                {
                    response=new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                else
                {
                    if (roleJson.IsAvaible)
                    {
                        var result = userManager.AddToRole(id, role.Name);
                        if (!result.Succeeded)
                             response = Request.CreateResponse(HttpStatusCode.NotModified);
                        else
                        {
                            response = Request.CreateResponse(HttpStatusCode.OK, "upd");
                            response.Content = new StringContent("updated", Encoding.Unicode);
                            response.Headers.CacheControl = new CacheControlHeaderValue();
                        }
                    }
                    else
                    {
                        var result = userManager.RemoveFromRole(id, role.Name);
                        if (!result.Succeeded)
                             response = Request.CreateResponse(HttpStatusCode.NotModified);
                        else
                        {
                            response = Request.CreateResponse(HttpStatusCode.OK, "del");
                            response.Content = new StringContent("removed", Encoding.Unicode);
                            response.Headers.CacheControl = new CacheControlHeaderValue();
                            
                        }
                    }
                }
            }
            return response;
        }
    }
}


