using GApp.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.DAL;

namespace TrainingIS.WebApp.Controllers
{
    public class InstallController : BaseController<TrainingISModel>
    {
        // GET: Install
        [AllowAnonymous]
        public ActionResult Index()
        {
            InstallBLO installBLO = new InstallBLO(this._UnitOfWork, this.GAppContext);
            if (installBLO.Install())
            {
                string msg_succus = string.Format("The application has been installed");
                Alert(msg_succus, GApp.BLL.Enums.NotificationType.success);
            }
            else
            {
                string msg_succus = string.Format("The application all ready installed");
                Alert(msg_succus, GApp.BLL.Enums.NotificationType.warning);
            }
            return RedirectToAction("Index", "Account");
        }
    }
}