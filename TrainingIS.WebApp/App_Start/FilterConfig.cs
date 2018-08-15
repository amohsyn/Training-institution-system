using GApp.WebApp.Context.Filters;
using System.Web;
using System.Web.Mvc;
using TrainingIS.WebApp.Core.Security.Filters;
using TrainingIS.WebApp.Filters;

namespace TrainingIS.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SecurityFilter());
            filters.Add(new GAppContextFilter());
            filters.Add(new TrainingIS_Context_Filter());
        }
    }
}
