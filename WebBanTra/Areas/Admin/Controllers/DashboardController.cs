using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanTra.Models;

namespace WebBanTra.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        private WebBanTraDbContext db = new WebBanTraDbContext();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}