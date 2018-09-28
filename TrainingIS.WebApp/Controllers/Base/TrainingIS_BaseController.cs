using GApp.Models.Pages;
using GApp.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.DAL;

namespace TrainingIS.WebApp.Controllers
{
    public class TrainingIS_BaseController : BaseController<TrainingISModel>
    {
        protected virtual FilterRequestParams Save_OR_Load_filterRequestParams_State(FilterRequestParams filterRequestParams)
        {
            var applicationParamBLO = new ApplicationParamBLO(this._UnitOfWork, this.GAppContext);
            string current_Controller = this.GetType().Name;
            string current_User = applicationParamBLO.GAppContext.Current_User_Name;

            if (filterRequestParams.IsEmpty())
            {
                filterRequestParams = applicationParamBLO.Read_FilterRequestParams_State(current_User, current_Controller);
            }
            else
            {
                applicationParamBLO.Save_FilterRequestParams_State(filterRequestParams, current_User, current_Controller);
            }

            return filterRequestParams;
        }

        //protected override void OnException(ExceptionContext filterContext)
        //{
           

        //    if (!filterContext.ExceptionHandled)
        //    {
        //        this.Alert(filterContext.Exception.Message, GApp.BLL.Enums.NotificationType.error);
        //        string controllerName = (string)filterContext.RouteData.Values["controller"];
        //        string actionName = (string)filterContext.RouteData.Values["action"];

        //        //Log.Error(filterContext.Exception.Message + " in " + controllerName);

        //        var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

        //        filterContext.Result = new ViewResult
        //        {
        //            ViewName = "~/Views/Shared/Error.cshtml",
        //            ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
        //            TempData = filterContext.Controller.TempData
        //        };

        //        filterContext.ExceptionHandled = true;

        //    }

        //}
    }
}