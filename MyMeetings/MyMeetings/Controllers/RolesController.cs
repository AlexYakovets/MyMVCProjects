using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MyMeetings.Models;

namespace MyMeetings.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private ApplicationRoleManager RoleManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>(); }
        }
       
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RolesViewModels.CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result =
                    await
                        RoleManager.CreateAsync(new ApplicationRole {Name = model.Name, Description = model.Description});
                if (result.Succeeded)
                {
                   return RedirectToAction("Index","Roles");
                }
                else ModelState.AddModelError("", "Errors with role creating");
            }
            return View(model);

        }

        public async Task<ActionResult> Edit(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                return View(new RolesViewModels.EditRoleModel {Id = role.Id, Name = role.Name, Description = role.Description});
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(RolesViewModels.EditRoleModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = await RoleManager.FindByIdAsync(model.Id);
                if (role != null)
                {
                    role.Description = model.Description;
                    role.Name = model.Name;
                    IdentityResult result = await RoleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else ModelState.AddModelError("", "aErrors with role editing");

                }
            }
            return View(model);

        }

        public async Task<ActionResult> AddToUser(string userId,string roleId)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationRole role = await RoleManager.FindByIdAsync(roleId);
            ApplicationUser user = await userManager.FindByIdAsync(userId);
            var result=await userManager.AddToRoleAsync(user.Id, role.Name);
            if (result.Succeeded)
            {
                 
            }
            else ModelState.AddModelError("","Role not added for user :"+Convert.ToString(user.UserName));
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            if (role!=null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
    }
}