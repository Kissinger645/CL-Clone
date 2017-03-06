using CL.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CL.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

            ViewBag.Cities = db.Locations.OrderBy(l => l.StateName).ToList();
                return View();
        }

        [Authorize]
        public ActionResult UserPage()
        {
            var thisUser = User.Identity.GetUserId();
            var myLoc = db.CLUsers.FirstOrDefault(u => u.OwnerId == thisUser);
            if (myLoc == null)
            {
                ViewBag.MyCity = "Not Set";
            }
            else
            {
                ViewBag.MyCity = myLoc.Location.CityName;
            }

            ViewBag.MyPosts = db.Posts.Where(u => u.OwnerId == thisUser);
            
            return View();
        }

        [Route("s/{State}")]
        public ActionResult State(string State)
        {
            ViewBag.Cities = db.Locations.Where(l => l.StateName == State);
            return View();
        }

        [Route("c/{City}")]
        public ActionResult City(string City)
        {
            ViewBag.City = City;
            ViewBag.Sale = db.Categories.Where(c => c.Main == "Sale");
            ViewBag.Housing = db.Categories.Where(c => c.Main == "Housing");
            ViewBag.Services = db.Categories.Where(c => c.Main == "Services");
            return View();
        }
        
        [Route("c/{City}/{Sub}")]
        public ActionResult ViewPosts(string City, string Sub)
        {
            ViewBag.Route = $"City: {City} - Category: {Sub}";
            var cat = db.Categories.FirstOrDefault(c => c.Sub == Sub);
            var city = db.Locations.FirstOrDefault(c => c.CityName == City);
         
            if (db.Posts.Where(p => p.CatId == cat.Id && p.CityId == city.Id).ToList() == null)
            {
                ViewBag.Posts = "Sorry, there are currently no postings in this category";
            }
            else
            {
                ViewBag.Posts = db.Posts.Where(p => p.CatId == cat.Id && p.CityId == city.Id).ToList();
            }
            return View();
        }

        [Route("c/thumb/{City}/{Sub}")]
        public ActionResult ViewPostsThumb(string City, string Sub)
        {
            ViewBag.Route = $"City: {City} - Category: {Sub}";
            var cat = db.Categories.FirstOrDefault(c => c.Sub == Sub);
            var city = db.Locations.FirstOrDefault(c => c.CityName == City);
            var posts = db.Posts.Where(p => p.CatId == cat.Id && p.CityId == city.Id).ToList();
            if (posts == null)
            {
                ViewBag.Posts = "Sorry, there are currently no postings in this category";
            }
            else
            {
                ViewBag.Posts = posts;
            }
            return View();
        }

        [Route("c/grid/{City}/{Sub}")]
        public ActionResult ViewPostsGrid(string City, string Sub)
        {
            ViewBag.Route = $"City: {City} - Category: {Sub}";
            var cat = db.Categories.FirstOrDefault(c => c.Sub == Sub);
            var city = db.Locations.FirstOrDefault(c => c.CityName == City);
            var posts = db.Posts.Where(p => p.CatId == cat.Id && p.CityId == city.Id).ToList();
            if (posts == null)
            {
                ViewBag.Posts = "Sorry, there are currently no postings in this category";
            }
            else
            {
                ViewBag.Posts = posts;
            }
            return View();
        }

        public ActionResult ChangeCity()
        {
            ViewBag.Cities = new SelectList(db.Locations, "Id","CityName");
            return View();
        }

        [HttpPost]
        public ActionResult ChangeCity(int Cities)
        {
            var thisUser = User.Identity.GetUserId();
            var hasLocation = db.CLUsers.FirstOrDefault(u => u.OwnerId == thisUser);
            if (hasLocation != null)
            {
                hasLocation.CityId = Cities;
            }
            else
            {
                UserModel cluser = new UserModel();
                cluser.OwnerId = thisUser;
                cluser.CityId = Cities;
                db.CLUsers.Add(cluser);
            }
            
            db.SaveChanges();
            return RedirectToAction("UserPage");
        }

        
    }
}