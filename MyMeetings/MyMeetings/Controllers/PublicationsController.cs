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
using System.Web.UI.WebControls;
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
            public ActionResult Create()
            {
                PublicationViewModels.CreatePublicationModelView model = new PublicationViewModels.CreatePublicationModelView();
            //model.Categories = new SelectList(_DB.PublicationCategories, "Id", "Name");
         
            //ViewBag.Categories = new SelectList(_DB.PublicationCategories, "Id", "Name");
            model.Categories = new SelectList(_DB.PublicationCategories, "Id", "Name");
            return View(model);
            }

            [HttpPost]

            public ActionResult Create(PublicationViewModels.CreatePublicationModelView model)
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
                        Category = _DB.PublicationCategories.FirstOrDefault(p=>p.Id==model.CategoryID.ToString()),
                        Subscriptions = new List<ApplicationUser>() {currentAuthor}
                    };
                    HttpPostedFileBase hpf = Request.Files["imagefile"] as HttpPostedFileBase;
                    string filePath =
                        System.Web.HttpContext.Current.Server.MapPath(
                            ConfigurationManager.AppSettings["PublicationAvatarsPath"] + "\\" +
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
                List<PublicationViewModels.PartialPublication> result =
                    new List<PublicationViewModels.PartialPublication>();
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
                        DateOfPublication = publ.DateTimeOfPublication.ToString(),
                        ImagePath = ImagePath.Remove(0, 1),
                        PublicationText = publ.Text,
                        Subscribers = publ.Subscriptions.ToList(),
                        Creator = (publ.Author.FirstName + " " + publ.Author.SurName),
                        DateOfMeet = publ.DateOfMeeting.ToString(@"MM\/dd\/yyyy HH:mm:ss")
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
                Publication publication = await _DB.Publications.FirstOrDefaultAsync(p => p.Id == id);
                var userId = User.Identity.GetUserId();
                ApplicationUser currentUser = await userManager.Users.FirstOrDefaultAsync(user => user.Id == userId);
                var ifSubscribed = publication.Subscriptions.FirstOrDefault(user => user == currentUser);
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
