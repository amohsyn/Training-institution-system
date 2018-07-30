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
           
            var controller = filterContext.Controller as BaseController;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var action = filterContext.ActionDescriptor.ActionName;
            var user = filterContext.HttpContext.User;

            // InitAutorization
            if (controller.hasPermission == null)
                controller.hasPermission = new Security.HasPermission();
            controller.hasPermission.InitAutorizationFor(user, controllerName);

            bool hasPermission = false;
            var allowAnonymous = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute),false);
            if(allowAnonymous.Count() > 0)
            {
                hasPermission = true;
            }
            else
            {
                hasPermission = controller.hasPermission.ToAction(action);
            }
 
            if (!hasPermission)
            {
                // [Bug] Localization
                string msg = string.Format("Vous n'avez pas l'autorisation pour accéder a cette fonctionnalité");
                controller.Alert(msg, Enums.Enums.NotificationType.error);
                filterContext.Result = new RedirectToRouteResult(
                 new System.Web.Routing.RouteValueDictionary(
                     new { controller = controller.Login_Controller, action = "Login" }));
            }
        }
    }
}