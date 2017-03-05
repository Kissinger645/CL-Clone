using CL.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CL.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Categories, "Id", "Sub");
            return View();
        }

        [HttpPost]
        public ActionResult Create(int Categories, int Price, string Description, string Title)
        {
            var thisGuy = User.Identity.GetUserId();
            var clGuy = db.CLUsers.Where(u => u.OwnerId == thisGuy).FirstOrDefault();
            var hisLocation = clGuy.Location;

            PostModel post = new PostModel();
            post.Title = Title;
            post.Description = Description;
            post.Price = Price;
            post.Posted = DateTime.Now;
            post.OwnerId = thisGuy;
            post.CatId = Categories;
            post.City = hisLocation;
            
            db.Posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("Upload", "Post", new { id = post.Id });
        }

        public ActionResult Upload(int id)
        {
            var uploadViewModel = new ImageUploadViewModel();
            return View(uploadViewModel);
        }

        [HttpPost]
        public ActionResult Upload(ImageUploadViewModel formData, int id)
        {
            var pId = id.ToString();
            var uploadedFile = Request.Files[0];
            string filename = $"{DateTime.Now.Ticks}{uploadedFile.FileName}";
            var serverPath = Server.MapPath(@"~\Uploads");
            var fullPath = Path.Combine(serverPath, filename);
            uploadedFile.SaveAs(fullPath);

            var uploadModel = new ImageUpload
            {
                Caption = pId,
                File = filename
            };
            db.ImageUploads.Add(uploadModel);
            db.SaveChanges();
            return RedirectToAction("UserPage", "Home");
        }

        public ActionResult Details(int? id)
        {
            var cId = id.ToString();
            if (db.ImageUploads.Where(u => u.Caption == cId).FirstOrDefault() != null)
            {
                var pic = db.ImageUploads.Where(u => u.Caption == cId).FirstOrDefault();
                ViewBag.Picture = pic.FilePath;
            }

            PostModel post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.Post = post;
            return View();
        }
        public ActionResult Delete(int? Pid)
        {
            if (Pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostModel post = db.Posts.Find(Pid);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Pid)
        {
            PostModel post = db.Posts.Find(Pid);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("UserPage", "Home");
        }




    }
}