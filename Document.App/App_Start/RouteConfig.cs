using Microsoft.AspNet.FriendlyUrls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Document.App.App_Start
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;

            GlobalConfiguration.Configuration.MapHttpAttributeRoutes();
            GlobalConfiguration.Configuration.EnsureInitialized();
            RouteTable.Routes.MapHttpRoute(name: "API Default", routeTemplate: "api/v1/{controller}/{id}", defaults: new { id = RouteParameter.Optional });


            routes.MapPageRoute("login", "login", "~/LoginPage.aspx");
            routes.MapPageRoute("product", "product", "~/ProductPage.aspx");
            routes.MapPageRoute("error", "error", "~/Error.aspx");
            routes.MapPageRoute("register", "register", "~/Register.aspx");
            routes.MapPageRoute("training", "training-session", "~/LearningWebform.aspx");

            routes.EnableFriendlyUrls(settings);


        }

    }
}