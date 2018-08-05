using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrainingIS.WebApp.Controllers;

namespace TrainingIS.WebApp
{
    /// <summary>
    /// Filter registred for all controllers in Application
    /// </summary>
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
           
            var controller = filterContext.Controller as BaseController;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var action = filterContext.ActionDescriptor.ActionName;
            var user = filterContext.HttpContext.User;

            // Create HasPermission insrance
            controller.hasPermission = new Security.HasPermission(user, controllerName);


            bool isAuthorised = false;
            var allowAnonymous_attributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute),false);
            if(allowAnonymous_attributes.Count() > 0)
            {
                isAuthorised = true;
            }
            else
            {
                isAuthorised = controller.hasPermission.ToAction(action);
            }
 
            if (!isAuthorised)
            {
                // [Bug] Localization
                string msg = string.Format("Vous n'avez pas l'autorisation pour accéder a cette fonctionnalité");
                string debug_msg = string.Format(" \n -- Controller : {0}, Action : {1}", controller, action);
                if(HttpContext.Current.IsDebuggingEnabled)
                {
                    msg += debug_msg;
                }

                controller.Alert(msg, Enums.Enums.NotificationType.error);
                filterContext.Result = new RedirectToRouteResult(
                 new System.Web.Routing.RouteValueDictionary(
                     new { controller = controller.Login_Controller, action = "Login" }));
            }
        }
    }
}