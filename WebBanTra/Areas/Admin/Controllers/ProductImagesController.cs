using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanTra.Areas.Admin.Controllers
{
    public class ProductImagesController : BaseController
    {
        // GET: Admin/ProductImages
        public ActionResult Index(int id)
        {
            ViewBag.Id = id;
            return View();
        }

    }
}