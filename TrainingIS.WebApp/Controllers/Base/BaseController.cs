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
using TrainingIS.WebApp.Security;
using TrainingIS.WebApp.Views.Base;
using static TrainingIS.WebApp.Enums.Enums;

namespace TrainingIS.WebApp.Controllers
{

    public class BaseController : Controller
    {
        #region Variables
        public HasPermission hasPermission { set; get; }

        public string Home_Controller = "Cplus";
        public string Login_Controller = "Account";

        // Skin and Theme
        protected string Theme = "gentelella";
        protected string Skin = "default";

        // Message
        protected Dictionary<string, string> msg = new Dictionary<string, string>();
        protected MsgViews msgHelper;

        // DAL
        protected BLO_Manager _BLO_Manager = null;
        public UnitOfWork _UnitOfWork = null;
        #endregion

        public BaseController()
        {
            this.InitDAL();
            this.InitMessages();

        }
        protected override void EndExecute(IAsyncResult asyncResult)
        {
            this.InitSecurity();

            this.CheckCurrentTrainingYear();
            this.Init_UnitOfWork();
            base.EndExecute(asyncResult);
        }

        private void InitSecurity()
        {
            if (hasPermission != null)
            {
                // if hasPermission not defined Allow ALL Action in Views
                hasPermission = new HasPermission();
            }
              
            ViewBag.HasPermission = hasPermission;
        }

        private void Init_UnitOfWork()
        {
            this._UnitOfWork.User_Identity_Name = User.Identity.Name;
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
            _BLO_Manager = new BLO_Manager(_UnitOfWork);
        }
        #endregion

        #region Message Manager
        private void InitMessages()
        {
            ViewBag.msg = msg;
        }
        [NonAction]
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
        [NonAction]
        public void Message(string message, NotificationType notifyType)
        {
            TempData["Notification2"] = message;

            switch (notifyType)
            {
                case NotificationType.success:
                    TempData["NotificationCSS"] = "alert alert-success";
                    break;
                case NotificationType.error:
                    TempData["NotificationCSS"] = "alert alert-error";
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
     
       
        /// <summary>
        /// Check CurrentTrainingYear from Session or DataBase
        /// </summary>
        protected void CheckCurrentTrainingYear()
        {
            TrainingYearBLO trainingYearBLO = new TrainingYearBLO(_UnitOfWork);
            TrainingYear currentTrainingYear = null;

            // Chek Session value
            if (Session[ApplicationParamBLO.CURRENT_TrainingYear_Reference] != null)
            {
                string CurrentTrainingYear_Reference = Session[ApplicationParamBLO.CURRENT_TrainingYear_Reference] as string;
                currentTrainingYear = trainingYearBLO.FindBaseEntityByReference(CurrentTrainingYear_Reference);
            }
            else
            {
                currentTrainingYear = trainingYearBLO.getCurrentTrainingYear();
                if (currentTrainingYear == null)
                {

                    Alert(msg_Base.You_have_to_add_a_year_of_training, NotificationType.warning);
                    // can not redirect in this function!
                    Redirect(Url.Action("Index", "TrainingYears"));
                }

            }
            this._UnitOfWork.CurrentTrainingYear = currentTrainingYear;
            ViewBag.CurrentTrainingYear = currentTrainingYear;
            ViewBag.TrainingYears = trainingYearBLO.FindAll();
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._UnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}