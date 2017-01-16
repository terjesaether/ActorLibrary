using ActorLibrary.Migrations;
using ActorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;


namespace ActorLibrary
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Database.SetInitializer<ActorContext>(new DropCreateDatabaseIfModelChanges<ActorContext>());
            //Database.SetInitializer<ActorContext>(new DropCreateDatabaseAlways<ActorContext>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ActorContext, Configuration>());
            //Database.SetInitializer<ActorContext>(null);
        }
    }
}
