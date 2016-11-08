using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace tinygg.cn_mono
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "PostNormal", // 路由名称
                "{datetime}/{title_en}/{id}", // 带有参数的 URL
                //defaults
                new { controller = "Home", action = "Post", id = UrlParameter.Optional },
                //constraints
                new { datetime = @"^[\d]{4}\-[\d]{2}\-[\d]{2}$", title_en = @"^[a-zA-Z0-9_\-]{0,}$" }
            );

            routes.MapRoute(
            "PostStamp", // 路由名称
            "{stamp}", // 带有参数的 URL
                //defaults
            new { controller = "Home", action = "Post" },
            new { stamp = @"^\d{13}$" }
            );

            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            "Page", // 路由名称
            "{page}", // 带有参数的 URL
            new { controller = "Home", action = "Index", page = UrlParameter.Optional }
            );
        }
    }
}