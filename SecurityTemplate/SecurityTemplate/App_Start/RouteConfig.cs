using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Security
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Entidades",
                url: "Entidades/{action}/{id}/{id2}",
                defaults: new { controller = "Entidades", action = "Index", id = UrlParameter.Optional, id2 = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Municipios",
                url: "Municipios/{action}/{id}/{id2}/{id3}",
                defaults: new { controller = "Municipios", action = "Index", id = UrlParameter.Optional, id2 = UrlParameter.Optional, id3 = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Entidades",
                url: "{controller}/{action}/{id}/{id2}",
                defaults: new { controller = "Entidades", action = "Index", id = UrlParameter.Optional, id2 = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Action 3 param",
                url: "{controller}/{action}/{id}/{id2}/{id3}"                
            );
        }
    }
}
