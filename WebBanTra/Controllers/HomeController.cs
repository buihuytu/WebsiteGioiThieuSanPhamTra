using Microsoft.AspNetCore.Mvc;
using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanTra.Models;

namespace WebBanTra.Controllers
{
    public class HomeController : Controller
    {
        private WebBanTraDbContext db = new WebBanTraDbContext();
        public ActionResult Index()
        {
            ViewBag.Slider = db.tblSliders.Where(p => p.IsActive == 1).ToList();
            ViewBag.PopularProduct = db.tblProducts.Where(p => p.IsActive == 1).OrderByDescending(p => p.CreatedDate).Take(5).ToList();
            return View();
        }

        public ActionResult Introduction()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitContact(tblContact mContact)
        {
            mContact.FullName = Request.Form["fullName"];
            mContact.Email = Request.Form["email"];
            mContact.Phone = Convert.ToInt32(Request.Form["phoneNumber"]);
            mContact.Detail = Request.Form["detail"];
            mContact.IsReply = 0;
            mContact.CreatedDate = DateTime.Now;

            db.tblContacts.Add(mContact);
            db.SaveChanges();
            return RedirectToAction("Contact", "Home");
        }

        public ActionResult News(int? page)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var list = db.tblNews.Where(n => n.IsActive == 1).OrderByDescending(n => n.CreatedDate).ToPagedList(pageNumber, pageSize);
            return View(list);
        }

        
        public ActionResult Product(int? page, string sortOrder, string typeSort)
        {
            
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ViewBag.Sort = "Thứ tự mặc định";
            ViewBag.sortOrder = sortOrder;
            ViewBag.typeSort = typeSort;
            var list = db.tblProducts.Where(p => p.IsActive == 1).OrderByDescending(p => p.CreatedDate).ToPagedList(pageNumber, pageSize);
            if(typeSort == "price" && sortOrder == "desc")
            {
                ViewBag.Sort = "Giá cao đến thấp";
                list = db.tblProducts.Where(p => p.IsActive == 1).OrderByDescending(p => p.Price).ToPagedList(pageNumber, pageSize);
            }
            else if (typeSort == "price" && sortOrder == "asc")
            {
                ViewBag.Sort = "Giá thấp đến cao";
                list = db.tblProducts.Where(p => p.IsActive == 1).OrderBy(p => p.Price).ToPagedList(pageNumber, pageSize);
            }
            ViewBag.listCate = db.tblCategories.Where(p => p.IsActive == 1).OrderByDescending(p => p.CreatedDate).ToList();
            
            return View(list);
        }


        public ActionResult ProductNotFound()
        {
            ViewBag.listCate = db.tblCategories.Where(p => p.IsActive == 1).OrderByDescending(p => p.CreatedDate).ToList();
            return View();
        }

        public ActionResult ProductsByCategory(string slug, int? page, string sortOrder, string typeSort)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ViewBag.Sort = "Thứ tự mặc định";
            ViewBag.sortOrder = sortOrder;
            ViewBag.typeSort = typeSort;
            ViewBag.slug = slug;
            int idCate = db.tblCategories.Where(c => c.Slug == slug).First().ID;
            var lstProducts = db.tblProducts.Where(n => n.CateID == idCate && n.IsDelete == 0 && n.IsActive == 1).OrderByDescending(n => n.CreatedDate).ToPagedList(pageNumber, pageSize);
            if (lstProducts.Count == 0)
            {
                return RedirectToAction("ProductNotFound");
            }
            else
            {
                if (typeSort == "price" && sortOrder == "desc")
                {
                    ViewBag.Sort = "Giá cao đến thấp";
                    lstProducts = db.tblProducts.Where(n => n.CateID == idCate && n.IsDelete == 0 && n.IsActive == 1).OrderByDescending(p => p.Price).ToPagedList(pageNumber, pageSize);
                }
                else if (typeSort == "price" && sortOrder == "asc")
                {
                    ViewBag.Sort = "Giá thấp đến cao";
                    lstProducts = db.tblProducts.Where(n => n.CateID == idCate && n.IsDelete == 0 && n.IsActive == 1).OrderBy(p => p.Price).ToPagedList(pageNumber, pageSize);
                }
            }
            
            ViewBag.listCate = db.tblCategories.Where(p => p.IsActive == 1).OrderByDescending(p => p.CreatedDate).ToList();
            return View(lstProducts);
        }

        public ActionResult ProductDetail(String slug)
        {
            ViewBag.Slug = slug;
            var getP = db.tblProducts.Where(p => p.Slug == slug && p.IsDelete == 0 && p.IsActive == 1).First();
            ViewBag.listOther = db.tblProducts.Where(p => p.IsActive== 1 && p.CateID == getP.CateID && p.ID != getP.ID).OrderByDescending(p => p.CreatedDate).Take(5).ToList();
            return View("ProductDetail", getP);
        }

        public ActionResult Posts(String slug)
        {
            
            var post = db.tblNews.Where(p => p.Slug == slug && p.IsDelete == 0 && p.IsActive == 1).First();
            ViewBag.PostName = post.Name;
            ViewBag.otherPost = db.tblNews.Where(p => p.IsActive == 1 && p.Id != post.Id).OrderByDescending(p => p.CreatedDate).Take(5).ToList();
            return View("Posts", post);
        }
    }
}