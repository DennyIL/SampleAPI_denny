using SampleAPI.Areas.HelpPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SampleAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //penambahan reurn format JSON - 20181107
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //untuk dokumentasi API, lamgsung ter generate di WEB
            config.SetDocumentationProvider
                (new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/App_Data/Documentation.xml")));
        }
    }
}
