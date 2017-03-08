using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        //Как мнициализировать через не статик поле
        //private ApplicationDbContext _DB = new ApplicationDbContext();
        //private ApplicationUserManager userManager =
        //      new ApplicationUserManager(new UserStore<ApplicationUser>(_DB));
        // GET: Publication
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PublicationViewModels.CreateViewModel model)
        {
            ApplicationDbContext _DB = new ApplicationDbContext();
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
                    Subscriptions= new List<ApplicationUser>() {currentAuthor}
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
        public ActionResult Index(int? page)
        {
            ApplicationDbContext _DB = new ApplicationDbContext();
            //ApplicationUserManager userManager =
            //      new ApplicationUserManager(new UserStore<ApplicationUser>(_DB));
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
                if (System.IO.File.Exists(publ.ImagePath))
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
                    PublicationId = publ.Id,
                    PublicationName = publ.Name,
                    DateOfPublication = publ.DateTimeOfPublication,
                    ImagePath = ImagePath.Remove(0,1),
                    PublicationText = publ.Text,
                    Subscribers = publ.Subscriptions.ToList(),
                    Creator = (publ.Author.FirstName+" "+publ.Author.SurName),
                    DateOfMeet = publ.DateOfMeeting
                    //Subscribers = publ.Subscriptions.ToList()
                });
            }
            int pagesize = 3;
            int pagenumber = (page ?? 1);
            return View(result.ToPagedList(pagenumber, pagesize));
        }
        [HttpGet]
        public async Task<ActionResult> AddSubscriber(string id)
        {
            ApplicationDbContext _DB = new ApplicationDbContext();
            ApplicationUserManager userManager =
                  new ApplicationUserManager(new UserStore<ApplicationUser>(_DB));
            Publication publication = await  _DB.Publications.FirstOrDefaultAsync(p => p.Id == id);
            var userId=User.Identity.GetUserId();
            ApplicationUser currentUser = await userManager.Users.FirstOrDefaultAsync(user => user.Id == userId);
            var  ifSubscribed= publication.Subscriptions.FirstOrDefault(user => user == currentUser);
            if (ifSubscribed == null)
            {
                publication.Subscriptions.Add(currentUser);
                _DB.SaveChanges();
            }
            
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> DeleteSubscriber(string id)
        {
            ApplicationDbContext _DB = new ApplicationDbContext();
            ApplicationUserManager userManager =
                  new ApplicationUserManager(new UserStore<ApplicationUser>(_DB));
            Publication publication = await _DB.Publications.FirstOrDefaultAsync(p => p.Id == id);
            var userId = User.Identity.GetUserId();
            ApplicationUser currentUser = await userManager.Users.FirstOrDefaultAsync(user => user.Id == userId);
            var ifSubscribed = publication.Subscriptions.FirstOrDefault(user => user == currentUser);
            if (ifSubscribed != null)
            {
                publication.Subscriptions.Remove(currentUser);
                _DB.SaveChanges();
            }

           
            return RedirectToAction("Index");
        }

        public bool IsSubscribedUser(ApplicationUser user)
        {
            return true;
        }

        //[HttpGet]
        //public ActionResult AddSubscriber(string id)
        //{
        //    ViewBag.PublicationID = id;
        //    return RedirectToAction("AddSubscribe");
        //}

        //[HttpPost]
        //public ActionResult AddSubscribe()
        //{
        //    var publication = _DB.Publications.FirstOrDefaultAsync(p => p.Id == ViewBag.p);
        //    return RedirectToAction("Index","Home");
        //}

    }
}