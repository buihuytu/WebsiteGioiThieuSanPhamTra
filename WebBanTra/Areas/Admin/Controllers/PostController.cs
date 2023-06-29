using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanTra.Models;

namespace WebBanTra.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        private WebBanTraDbContext db = new WebBanTraDbContext();

        // GET: Admin/Post
        public ActionResult Index()
        {
            return View();
        }

        // trash
        public ActionResult Trash()
        {
            return View();
        }

        // details
        public ActionResult Details(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        // create 
        public ActionResult Create()
        {
            return View();
        }

        // edit
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        // delete
        public ActionResult Delete(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult DelTrash(int id)
        {
            tblNew post = db.tblNews.Find(id);

            post.IsDelete = 1;
            post.IsActive = 0;

            post.UpdatedBy = int.Parse(Session["Admin_ID"].ToString());
            //post.UpdatedBy = 1;

            post.UpdatedDate = DateTime.Now;
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ReTrash(int? id)
        {
            tblNew post = db.tblNews.Find(id);

            post.IsDelete = 0;
            post.IsActive = 1;

            post.UpdatedBy = int.Parse(Session["Admin_ID"].ToString());
            //post.UpdatedBy = 1;

            post.UpdatedDate = DateTime.Now;

            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash", "Post");
        }

        //doi trang thai
        [HttpPost]
        public JsonResult changeStatus(int id)
        {
            tblNew post = db.tblNews.Find(id);
            post.IsActive = (post.IsActive == 1) ? 0 : 1;

            post.UpdatedDate = DateTime.Now;
            post.UpdatedBy = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                Status = post.IsActive
            });
        }
    }
}