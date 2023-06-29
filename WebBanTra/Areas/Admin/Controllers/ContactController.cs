using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanTra.Models;

namespace WebBanTra.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        private WebBanTraDbContext db = new WebBanTraDbContext();

        // GET: Admin/Contact
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
        public ActionResult Reply(int id)
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
            tblContact Contact = db.tblContacts.Find(id);

            Contact.IsDelete = 1;

            Contact.RepliedBy = int.Parse(Session["Admin_ID"].ToString());
            //Contact.RepliedBy = 1;

            Contact.RepliedDate = DateTime.Now;
            db.Entry(Contact).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ReTrash(int? id)
        {
            tblContact Contact = db.tblContacts.Find(id);

            Contact.IsDelete = 0;

            Contact.RepliedBy = int.Parse(Session["Admin_ID"].ToString());
            //Contact.RepliedBy = 1;

            Contact.RepliedDate = DateTime.Now;

            db.Entry(Contact).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash", "Contact");
        }



    }
}