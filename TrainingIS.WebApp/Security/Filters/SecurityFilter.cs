using GApp.BLL.Enums;
using GApp.WebApp.Controllers;
using GApp.WebApp.Core.Controllers;
using GApp.WebApp.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrainingIS.WebApp.Controllers;

namespace TrainingIS.WebApp.Core.Security.Filters
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

            var controller = filterContext.Controller as IBaseController;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var action = filterContext.ActionDescriptor.ActionName;
            var user = filterContext.HttpContext.User;

            // GAppContext
            var _Controller = filterContext.Controller as IBaseController;
            var _UserName = filterContext.HttpContext.User.Identity.Name;
            _Controller.GAppContext.Current_User_Name = _UserName;

            // Create HasPermission insrance
            controller.HasPermission = new  HasPermission(user, controllerName,controller.GAppContext);


            bool isAuthorised = false;
            var allowAnonymous_attributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), false);
            if (allowAnonymous_attributes.Count() > 0)
            {
                isAuthorised = true;
            }
            else
            {
                isAuthorised = controller.HasPermission.ToAction(action);
            }

            if (!isAuthorised)
            {
                // [Bug] Localization
                string msg = string.Format("Vous n'avez pas l'autorisation pour accéder a cette fonctionnalité");
                string debug_msg = string.Format(" \n [{0},{1}]", controller, action);
                if (HttpContext.Current.IsDebuggingEnabled)
                {
                    msg += debug_msg;
                }

                if(controller.GetType().Name != typeof(CplusController).Name)
                {
                    controller.Alert(msg, NotificationType.error);
                }

               if(filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                   
                    filterContext.Result = new JsonResult
                    {
                        Data = new { Success = false, Data = "Vous n'avez pas l'autorization à utiliser cette action" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        ContentType = "application/json",
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    filterContext.HttpContext.Response.StatusCode = 401;
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                 new System.Web.Routing.RouteValueDictionary(
                     new { controller = controller.Login_Controller, action = "Login" }));
                }
                
            }
        }
    }
}
