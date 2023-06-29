using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanTra.Models;

namespace WebBanTra.Areas.Library
{
    public class CheckSlug
    {
        WebBanTraDbContext db = new WebBanTraDbContext();
        public bool KiemTraSlug(String Table, String Slug, int? id)
        {
            switch (Table)
            {
                case "Category":
                    if (id != null)
                    {
                        if (db.tblCategories.Where(m => m.Slug == Slug && m.ID != id).Count() > 0)
                            return false;
                    }
                    else
                    {
                        if (db.tblCategories.Where(m => m.Slug == Slug).Count() > 0)
                            return false;
                    }
                    break;
                case "Topic":
                    break;
                case "Post":
                    break;
                case "Product":
                    break;
            }
            return true;


        }
    }
}