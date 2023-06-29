﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebBanTra
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_Start()
        {
            // Administrators
            Session["Admin_Name"] = null;
            Session["Admin_ID"] = null;
            Session["Admin_Images"] = null;
            Session["Admin_Address"] = null;
            Session["Admin_Email"] = null;
            Session["Admin_Created_at"] = null;


        }
    }
}
