using CL.Models;
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

        [Route("u/{UserName}")]
        public ActionResult UserPage()
        {
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
            ViewBag.SubSale = db.Categories.Where(c => c.Main == "Sale");
            ViewBag.SubHousing = db.Categories.Where(c => c.Main == "Housing");
            ViewBag.SubHousing = db.Categories.Where(c => c.Main == "Services");
            return View();
        }
    }
}