using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanTra.Models;

namespace WebBanTra.Areas.Admin.Controllers
{
    public class SliderController : BaseController
    {
        private WebBanTraDbContext db = new WebBanTraDbContext();

        // GET: Admin/Slider
        public ActionResult Index()
        {
            return View();
        }

        // trash
        public ActionResult Trash()
        {
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

        // details
        public ActionResult Details(int id)
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
            tblSlider slider = db.tblSliders.Find(id);

            slider.IsDelete = 1;
            slider.IsActive = 0;

            slider.UpdatedBy = int.Parse(Session["Admin_ID"].ToString());
            //slider.UpdatedBy = 1;

            slider.UpdatedDate = DateTime.Now;
            db.Entry(slider).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ReTrash(int? id)
        {
            tblSlider slider = db.tblSliders.Find(id);

            slider.IsDelete = 0;
            slider.IsActive = 1;

            slider.UpdatedBy = int.Parse(Session["Admin_ID"].ToString());
            //slider.UpdatedBy = 1;

            slider.UpdatedDate = DateTime.Now;

            db.Entry(slider).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash", "Slider");
        }

        //doi trang thai
        [HttpPost]
        public JsonResult changeStatus(int id)
        {
            tblSlider slider = db.tblSliders.Find(id);
            slider.IsActive = (slider.IsActive == 1) ? 0 : 1;

            slider.UpdatedDate = DateTime.Now;
            slider.UpdatedBy = int.Parse(Session["Admin_ID"].ToString());
            db.Entry(slider).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {
                Status = slider.IsActive
            });
        }
    }
}