using GApp.Models.Pages;
using GApp.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.DAL;

namespace TrainingIS.WebApp.Controllers
{
    public class TrainingIS_BaseController : BaseController<TrainingISModel>
    {
        /// <summary>
        /// Save or Load the Filter, Order and Pagination user parameter from DataBase for the current Controller.
        /// </summary>
        /// <param name="filterRequestParams">The FilterRequestParams object empty or not</param>
        /// <returns>Finded FilterRequestParams if the params is empty</returns>
        protected virtual FilterRequestParams Save_OR_Load_filterRequestParams_State(FilterRequestParams filterRequestParams)
        {
            if (filterRequestParams == null)
                filterRequestParams = new FilterRequestParams();

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

        protected virtual void Delete_filterRequestParams_State()
        {
            var applicationParamBLO = new ApplicationParamBLO(this._UnitOfWork, this.GAppContext);
            string current_Controller = this.GetType().Name;
            string current_User = applicationParamBLO.GAppContext.Current_User_Name;
            applicationParamBLO.Delete_FilterRequestParams_State(current_User, current_Controller);
        }


        /// <summary>
        ///  Create the Dicrectory ~/Content/Files if not exist
        ///  it is used to save all applications files
        /// </summary>
        protected virtual void Create_Files_Directory_If_Not_Exist()
        {
            string Files_path = Server.MapPath("~/Content/Files");
            if (!Directory.Exists(Files_path))
            {
                Directory.CreateDirectory(Files_path);

            }
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