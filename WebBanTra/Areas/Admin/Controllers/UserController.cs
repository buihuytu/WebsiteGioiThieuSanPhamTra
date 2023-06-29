using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanTra.Models;

namespace WebBanTra.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private WebBanTraDbContext db = new WebBanTraDbContext();

        // GET: Admin/User
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
        public ActionResult Details(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        // create 
        public ActionResult Create()
        {
            return View();
        }

        // delete
        public ActionResult Delete(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        // edit
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult DelTrash(int id)
        {
            tblUser User = db.tblUsers.Find(id);

            User.IsDelete = 1;
            User.IsActive = 0;

            User.UpdatedBy = int.Parse(Session["Admin_ID"].ToString());
            //User.UpdatedBy = 1;

            User.UpdatedDate = DateTime.Now;
            db.Entry(User).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ReTrash(int? id)
        {
            tblUser User = db.tblUsers.Find(id);

            User.IsDelete = 0;
            User.IsActive = 1;

            User.UpdatedBy = int.Parse(Session["Admin_ID"].ToString());
            //User.UpdatedBy = 1;

            User.UpdatedDate = DateTime.Now;

            db.Entry(User).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash", "User");
        }
    }
}