using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TrainingIS.DAL;

namespace TrainingIS.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
         
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

          
            RazorViewEngine razorEngine = ViewEngines.Engines.OfType<RazorViewEngine>().FirstOrDefault();
            if (razorEngine != null)
            {
                var newViewFormats = new[]
                                          {
                                        "~/1_Generated_Code/Views/{1}_{0}.cshtml"
                                     };


                razorEngine.ViewLocationFormats = 
                razorEngine.ViewLocationFormats.Union(newViewFormats).ToArray();
            }

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TrainingISModel, TrainingIS.DAL.Migrations.Configuration>());
            TrainingISModel trainingISModel = new TrainingISModel();
            trainingISModel.Database.Initialize(true);

          

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {

        }


    }
}
