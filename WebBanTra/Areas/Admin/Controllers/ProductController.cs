using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanTra.Models;

namespace WebBanTra.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private WebBanTraDbContext db = new WebBanTraDbContext();

        // GET: Admin/Product
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
            tblProduct product = db.tblProducts.Find(id);

            product.IsDelete = 1;
            product.IsActive = 0;

            product.UpdatedBy = int.Parse(Session["Admin_ID"].ToString());
            //product.UpdatedBy = 1;

            product.UpdatedDate = DateTime.Now;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ReTrash(int? id)
        {
            tblProduct product = db.tblProducts.Find(id);

            product.IsDelete = 0;
            product.IsActive = 1;

            product.UpdatedBy = int.Parse(Session["Admin_ID"].ToString());
            //product.UpdatedBy = 1;

            product.UpdatedDate = DateTime.Now;

            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash", "Product");
        }

        //doi trang thai
        [HttpPost]
        public JsonResult changeStatus(int id)
        {
            tblProduct product = db.tblProducts.Find(id);
            product.IsActive = (product.IsActive == 1) ? 0 : 1;

            product.UpdatedDate = DateTime.Now;
            product.UpdatedBy = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                Status = product.IsActive
            });
        }
    }
}