using GApp.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using TrainingIS.DAL;

namespace TrainingIS.WebApp.Filters
{
    public class ResaultTest : IResultFilter
    {
        private BaseController<TrainingISModel> _Controller;
        private string _ControllerName;

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //if (TrainingISModel.IsTest && !this._ControllerName.Contains("Account"))
            //{
            //    //if (!(filterContext.Result is RedirectToRouteResult))
            //    //{
            //        _Controller.transactionScope.Dispose();
            //    //}
                   
            //}


        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            this._Controller = filterContext.Controller as BaseController<TrainingISModel>;
            this._ControllerName = this._Controller.GetType().Name.RemoveFromEnd("Controller");
        }
    }
}