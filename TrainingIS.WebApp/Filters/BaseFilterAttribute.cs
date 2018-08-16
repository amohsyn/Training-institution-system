using GApp.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.DAL;

namespace TrainingIS.WebApp.Filters
{
    public class BaseFilterAttribute : FilterAttribute 
    {
        protected BaseController<TrainingISModel> _Controller { set; get; }
        protected String  _UserName { set; get; }
        protected String _ControllerName { set; get; }

        public virtual void OnActionExecuted(ActionExecutedContext filterContext)
        {
           

        }

        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this._Controller = filterContext.Controller as BaseController<TrainingISModel>;
            this._ControllerName = this._Controller.GetType().Name.RemoveFromEnd("Controller");
            
            this._UserName = filterContext.HttpContext.User.Identity.Name;
        }
    }
}