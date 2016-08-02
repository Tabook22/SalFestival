using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SalFestival2015
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(name: "artUrl",
            //        url: "{controller}/{action}/{artid}",
            //        defaults: new
            //        {
            //            controller = "SiteSettings",
            //            action = "getArtLst",
            //            artid = UrlParameter.Optional
            //        }
            //        );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
