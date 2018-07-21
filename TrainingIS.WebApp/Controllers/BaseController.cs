using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.WebApp.Helpers;
using TrainingIS.WebApp.Helpers.AlertMessages;
using TrainingIS.WebApp.Views.Base;
using static TrainingIS.WebApp.Enums.Enums;

namespace TrainingIS.WebApp.Controllers
{
    public class BaseController : Controller
    {
        #region Variables
        protected string Home_Controller = "Cplus";

        // Skin and Theme
        protected string Theme = "gentelella";
        protected string Skin = "default";

        // Message
        protected Dictionary<string, string> msg = new Dictionary<string, string>();
        protected MsgHelper msgHelper;

        // DAL
        protected UnitOfWork _UnitOfWork = null;
        #endregion

        public BaseController()
        {
            this.InitDAL();
            this.InitMessages();
           
        }
        protected override void EndExecute(IAsyncResult asyncResult)
        {
            this.CheckCurrentTrainingYear();
            base.EndExecute(asyncResult);
        }
        
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            this.InitCulture();
            return base.BeginExecuteCore(callback, state);
        }

        #region Culture Manager
        private void InitCulture()
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

        }
        #endregion

        #region DAL
        private void InitDAL()
        {
            _UnitOfWork = new UnitOfWork();
        }
        #endregion

        #region Message Manager
        private void InitMessages()
        {
            ViewBag.msg = msg;
        }
        public void Alert(string message, NotificationType notificationType)
        {
            AlertMessage alertMessage = new AlertMessage();
            alertMessage.message = message;
            alertMessage.notificationType = notificationType;
            TempData["notification"] = alertMessage;
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
        #endregion

        #region TrainingYear Manager
        public ActionResult ChangeCurrentTrainingYear(string Code)
        {
            ApplicationParamBLO applicationParamBLO = new ApplicationParamBLO(this._UnitOfWork);
            Session[ApplicationParamBLO.CURRENT_TrainingYear_Reference] = Code;
            return Redirect(string.Format("/{0}", this.Home_Controller));
        }
        /// <summary>
        /// Check CurrentTrainingYear from Session or DataBase
        /// </summary>
        protected void CheckCurrentTrainingYear()
        {
            TrainingYearBLO trainingYearBLO = new TrainingYearBLO(_UnitOfWork);

            // Chek Session value
            if (Session[ApplicationParamBLO.CURRENT_TrainingYear_Reference] != null)
            {
                string CurrentTrainingYear_Reference = Session[ApplicationParamBLO.CURRENT_TrainingYear_Reference] as string;
                ViewBag.CurrentTrainingYear = trainingYearBLO.FindBaseEntityByReference(CurrentTrainingYear_Reference);
            }
            else
            {
                var currentTrainingYear = trainingYearBLO.getCurrentTrainingYear();
                if(currentTrainingYear == null)
                {
                    
                    Alert(msg_Base.You_have_to_add_a_year_of_training, NotificationType.warning);
                    Redirect(Url.Action("Index", "TrainingYears"));
                }
                ViewBag.CurrentTrainingYear = currentTrainingYear;
            }
            ViewBag.TrainingYears = trainingYearBLO.FindAll();
        }
        #endregion
    }
}