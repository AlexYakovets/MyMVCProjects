using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Provider;
using MyMeetings.Models;
using PagedList;

namespace MyMeetings.Controllers
{
    [Authorize]
    public class PublicationsController : Controller
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
           ApplicationUserManager userManager =
               new ApplicationUserManager(new UserStore<ApplicationUser>(_DB));
            if (ModelState.IsValid)
            {
                string currentId = User.Identity.GetUserId();
                ApplicationUser currentAuthor = userManager.Users.FirstOrDefault(user => user.Id == currentId);
                var publication = new Publication()
                {   
                    Name = model.PublicationName,
                    DateOfMeeting = model.DateOfMeeting,
                    Text = model.Text,
                    Author = currentAuthor,
                    //AuthorId = currentId,
                    Subscriptions=new List<ApplicationUser>() {currentAuthor}
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
        public ActionResult Show(int? page)
        {
            List<PublicationViewModels.PartialPublication> result = new List<PublicationViewModels.PartialPublication>();
            //if (userName.IsNullOrWhiteSpace())
            //{
            //    result = UserManager.Users.ToList();
            //}
            //else
            //{
            //    var users = UserManager.Users.Where(a => a.UserName.Contains(userName)).ToList();
            //}
            foreach (var publ in _DB.Publications)
            {
                string ImagePath;
                if (System.IO.File.Exists(publ.ImagePath) == true)
                {
                    ImagePath = ConfigurationManager.AppSettings["PublicationAvatarsPath"] +
                                publ.Id + ".png";
                }
                else
                {
                    ImagePath = ConfigurationManager.AppSettings["PublicationAvatarsPath"] +
                               "default.png";
                }
                List<ApplicationUser> subscribers = new List<ApplicationUser>();
                result.Add(new PublicationViewModels.PartialPublication()
                {
                    PublicationName = publ.Name,
                    DateOfPublication = publ.DateTimeOfPublication,
                    ImagePath = ImagePath.Remove(0,1),
                    PublicationText = publ.Text,
                    Subscribers = subscribers,
                    Creator = (publ.Author.FirstName+" "+publ.Author.SurName),
                    DateOfMeet = publ.DateOfMeeting
                    //Subscribers = publ.Subscriptions.ToList()
                });
            }
            int pagesize = 3;
            int pagenumber = (page ?? 1);
            return View(result.ToPagedList(pagenumber, pagesize));
        }

    }
}