using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

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
                                         "~/Views_Extended/{1}/{0}.cshtml",
                                        "~/Views_Extended/Shared/{0}.cshtml"
                                     };

                
                razorEngine.ViewLocationFormats = 
                razorEngine.ViewLocationFormats.Union(newViewFormats).Reverse().ToArray();
            }
        }
    }
}
