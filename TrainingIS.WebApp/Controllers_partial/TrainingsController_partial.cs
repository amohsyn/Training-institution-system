using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrainingIS.WebApp.Controllers
{
    // Extended
    public  partial class TrainingsController 
    {
        [Authorize(Roles = "Admin")]
        public override ActionResult Index()
        {
            msgHelper.Index(msg);
            return View(trainingBLO.FindAll());
        }
    }
}