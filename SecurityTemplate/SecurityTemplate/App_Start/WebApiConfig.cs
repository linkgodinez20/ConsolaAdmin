using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Security
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "api3params",
                routeTemplate: "api/{controller}/{id}/{id2}/{id3}",
                defaults: new { controller = "Municipios", action = "Index", id = RouteParameter.Optional, id2 = RouteParameter.Optional, id3 = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
