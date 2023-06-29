using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanTra.Models;

namespace WebBanTra.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private WebBanTraDbContext db = new WebBanTraDbContext();

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }

        // trash
        public ActionResult Trash()
        {
            return View();
        }

        // detail 
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
            tblCategory MCategory = db.tblCategories.Find(id);
            
            MCategory.IsDelete = 1;

            MCategory.UpdatedBy = int.Parse(Session["Admin_ID"].ToString());
            //MCategory.UpdatedBy = 1;

            MCategory.UpdatedDate = DateTime.Now;
            db.Entry(MCategory).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ReTrash(int? id)
        {
            tblCategory MCategory = db.tblCategories.Find(id);
            
            MCategory.IsDelete = 0;
            MCategory.IsActive = 1;

            MCategory.UpdatedBy = int.Parse(Session["Admin_ID"].ToString());
            //MCategory.UpdatedBy = 1;

            MCategory.UpdatedDate = DateTime.Now;

            db.Entry(MCategory).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash", "Category");
        }

        //doi trang thai
        [HttpPost]
        public JsonResult changeStatus(int id)
        {
            tblCategory MCategory = db.tblCategories.Find(id);
            MCategory.IsActive = (MCategory.IsActive == 1) ? 0 : 1;

            MCategory.UpdatedDate = DateTime.Now;
            MCategory.UpdatedBy = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(MCategory).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                Status = MCategory.IsActive
            });
        }


    }
}