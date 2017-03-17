
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MyMeetings.Models;

namespace MyMeetings.Controllers
{
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        private IEnumerable<PublicationCategory> Categories
        {
            get { return _db.PublicationCategories.ToList(); }
        }

        public ActionResult Index()
        {
            return View(Categories);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoriesViewModels.CreateCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    new PublicationCategory()
                    {
                        Name = model.Name,
                        Description = model.Description
                    };
                if (result != null)
                {
                    _db.PublicationCategories.Add(result);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Categories");
                }
                else ModelState.AddModelError("", "Errors with role creating");
            }
            return View(model);
        }



        public ActionResult Edit(string id)
        {
            PublicationCategory category = _db.PublicationCategories.FirstOrDefault(p => p.Id == id);
            if (category != null)
            {
                return View(new CategoriesViewModels.EditCategoryModel() { Id = category.Id, Name = category.Name, Description = category.Description });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RolesViewModels.EditRoleModel model)
        {
            if (ModelState.IsValid)
            {
                PublicationCategory category = await _db.PublicationCategories.FirstOrDefaultAsync(p=>p.Id==model.Id);
                if (category != null)
                {
                    category.Description = model.Description;
                    category.Name = model.Name;
                    var result=_db.SaveChangesAsync();
                    if (result!=null)
                    {
                        return RedirectToAction("Index");
                    }
                    else ModelState.AddModelError("", "aErrors with role editing");

                }
            }
            return View(model);

        }

        //    public async Task<ActionResult> AddToUser(string userId, string roleId)
        //    {
        //        ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //        ApplicationRole role = await RoleManager.FindByIdAsync(roleId);
        //        ApplicationUser user = await userManager.FindByIdAsync(userId);
        //        var result = await userManager.AddToRoleAsync(user.Id, role.Name);
        //        if (result.Succeeded)
        //        {

        //        }
        //        else ModelState.AddModelError("", "Role not added for user :" + Convert.ToString(user.UserName));
        //        return RedirectToAction("Index");
        //    }

        public async Task<ActionResult> Delete(string id)
        {
            PublicationCategory deletingCategory = await _db.PublicationCategories.FirstOrDefaultAsync(p=>p.Id==id);
            if (deletingCategory != null)
            {
                _db.PublicationCategories.Remove(deletingCategory);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //}
    }
}
