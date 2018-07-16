using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TrainingIS.DAL;
using TrainingIS.WebApp.Helpers;
using static TrainingIS.WebApp.Enums.Enums;

namespace TrainingIS.WebApp.Controllers
{
    public class BaseController : Controller
    {


        protected string Theme = "gentelella";
        protected string Skin = "default";
        protected Dictionary<string, string> msg = new Dictionary<string, string>();
        protected MsgHelper msgHelper;

        protected TrainingISModel dbContext = new TrainingISModel();

        public BaseController()
        {
            ViewBag.msg = msg;
        }

        // GET: Base
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
            {
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                    Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                    null;
            }

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        public void Alert(string message, NotificationType notificationType)
        {
            var msg = "<script language='javascript'>$(function(){" +
                " swal('" + notificationType.ToString().ToUpper() + "', '" + message + "','" + notificationType + "');" +
                " });</script>";
            TempData["notification"] = msg;
        }

        /// <summary>
        /// Sets the information for the system notification.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="notifyType">The type of notification to display to the user: Success, Error or Warning.</param>
        public void Message(string message, NotificationType notifyType)
        {
            TempData["Notification2"] = message;

            switch (notifyType)
            {
                case NotificationType.success:
                    TempData["NotificationCSS"] = "alert alert-success";
                    break;
                case NotificationType.error:
                    TempData["NotificationCSS"] = "alert alert-errors";
                    break;
                case NotificationType.warning:
                    TempData["NotificationCSS"] = "alert alert-warning";
                    break;

                case NotificationType.info:
                    TempData["NotificationCSS"] = "alert-box notice";
                    break;
            }
        }
    }
}