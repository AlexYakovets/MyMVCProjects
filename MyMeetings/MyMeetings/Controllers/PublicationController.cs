using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Provider;
using MyMeetings.Models;

namespace MyMeetings.Controllers
{
    [Authorize]
    public class PublicationController : Controller
    {
        private ApplicationDbContext _DB = new ApplicationDbContext();
        // GET: Publication
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PublicationViewModels.CreateViewModel model)
        {
            if (ModelState.IsValid)
            { 
                var publication = new Publication()
                {   
                    Name = model.PublicationName,
                    DateOfMeeting = model.DateOfMeeting,
                    Text = model.Text,
                    AuthorId = User.Identity.GetUserId()
                };
                HttpPostedFileBase hpf = Request.Files["imagefile"] as HttpPostedFileBase;
                string filePath =
                              System.Web.HttpContext.Current.Server.MapPath(
                                  ConfigurationManager.AppSettings["PublicationAvatarsPath"] +"\\"+
                                  publication.Id + ".png");
                Image.SaveImage(hpf, filePath, 100, 100);
                publication.ImagePath = filePath;
                _DB.Publications.Add(publication);
                _DB.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            //AddErrors(result);

            return View(model);
        }
    }
}