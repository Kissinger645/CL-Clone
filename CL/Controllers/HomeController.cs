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

            ViewBag.States = db.Locations.ToList();
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
            string city = City;
            ViewBag.Sale = db.Categories.Where(c => c.Main == "Sale");
            ViewBag.Housing = db.Categories.Where(c => c.Main == "Housing");
            ViewBag.Services = db.Categories.Where(c => c.Main == "Services");
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