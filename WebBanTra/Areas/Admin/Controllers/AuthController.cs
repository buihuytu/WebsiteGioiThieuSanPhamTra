using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanTra.Models;

namespace WebBanTra.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        private WebBanTraDbContext db = new WebBanTraDbContext();
        
        public ActionResult Login()
        {
            if (Session["Admin_Name"] != null)
            {
                Response.Redirect("~/Admin");
            }
            return View();
        }

        [HttpPost]
        public JsonResult Login(String User, String Pass)
        {
            int count_username = db.tblUsers.Where(m => m.IsActive == 1 && m.UserName == User && m.IsDelete == 0 && m.Role == 1).Count();
            if(count_username == 0)
            {
                return Json(new { s = 1 });
            }
            else
            {
                var user_acount = db.tblUsers
                .Where(m => m.IsActive == 1 && m.Role == 1 && m.Password == Pass && m.UserName == User);
                if (user_acount.Count() == 0)
                    return Json(new { s = 2 });
                else
                {
                    var user = user_acount.First();
                    Session["Admin_Name"] = user.FullName;
                    Session["Admin_ID"] = user.ID;
                    Session["Admin_Images"] = user.Image;
                    Session["Admin_Address"] = user.Address;
                    Session["Admin_Email"] = user.Email;
                    Session["Admin_Created_at"] = user.CreatedDate;
                    return Json(new { s = 0 });
                }
            }
        }

        public ActionResult Logout()
        {
            if (Session["Admin_Name"] != null)
            {
                Session["Admin_Name"] = null;
                Session["Admin_ID"] = null;
                Session["Admin_Images"] = null;
                Session["Admin_Address"] = null;
                Session["Admin_Email"] = null;
                Session["Admin_Created_at"] = null;
            }
            return Redirect("~/Admin/Login");
        }

        public ActionResult Profiles()
        {
            if (Session["Admin_Name"] == null)
            {
                return Redirect("~/Admin/Login");
            }
            if (Session["Admin_Name"] == null)
            {
                return HttpNotFound();
            }


            return View("Profiles");

        }

        public ActionResult Check()
        {
            if (Session["Admin_Name"] == null)
            {
                return Redirect("~/Admin/Login");
            }
            if (Session["Admin_Name"] == null)
            {
                return HttpNotFound();
            }
            var list = db.tblUsers.Where(m => m.IsActive == 1).ToList();
            return View("_Test", list);

        }
    }
}