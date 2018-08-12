using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.WebApp.Controllers;
namespace TrainingIS.WebApp.Filters
{
    public class BaseFilterAttribute : FilterAttribute 
    {
        protected BaseController _Controller { set; get; }
        protected String  _UserName { set; get; }

        public virtual void OnActionExecuted(ActionExecutedContext filterContext)
        {
           

        }

        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this._Controller = filterContext.Controller as BaseController;
            this._UserName = filterContext.HttpContext.User.Identity.Name;
        }
    }
}