using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace prjMvcDemo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           // routes.MapRoute(
           //    name: "Marco",  //名稱
           //    url: "{action}/{controller}/{id}",   //規則
           //    defaults: new { controller = "Customer", action = "List", id = UrlParameter.Optional } 
           //);//預設值

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Shopping", action = "List", id = UrlParameter.Optional }
            );   // 可以設定初始畫面的位置

        }
    }
}
