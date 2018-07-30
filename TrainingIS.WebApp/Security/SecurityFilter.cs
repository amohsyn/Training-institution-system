using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrainingIS.WebApp.Controllers;

namespace TrainingIS.WebApp
{
    public class SecurityFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

           
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {


            //var role = (string) filterContext.Controller.TempData["role"];

            //var isAdmin = role == Constants.ADMIN;

            //if (!isAdmin)
            //{
            //    filterContext.Result = new RedirectToRouteResult(
            //       new System.Web.Routing.RouteValueDictionary(
            //           new { controller = "Dashboard", action = "Index" }));
            //    return;
            //}

            var controller = filterContext.Controller as BaseController;
            var ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var Action = filterContext.ActionDescriptor.ActionName;
            var User = filterContext.HttpContext.User;

            //if(filterContext.ActionDescriptor.getc)

 
            var isAccessAllowed = false;

            if (!isAccessAllowed)
            {
                // [Bug] Localization
                string msg = string.Format("Vous n'avez pas l'autorisation pour accéder a cette fonctionnalité");
                controller.Alert(msg,Enums.Enums.NotificationType.error);
                filterContext.Result = new RedirectToRouteResult(
                 new System.Web.Routing.RouteValueDictionary(
                     new { controller =  controller.Home_Controller, action = "Index" }));
            }
        }
    }
}