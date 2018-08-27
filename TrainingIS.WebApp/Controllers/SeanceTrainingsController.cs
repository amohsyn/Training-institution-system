using GApp.BLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Models.SeanceTrainings;

namespace TrainingIS.WebApp.Controllers
{
    public partial class SeanceTrainingsController
    {
        [NonAction]
        public override ActionResult Create()
        {
            return Create(DateTime.Now.ToShortDateString());
        }

       
        public ActionResult Create(string SeanceDate)
        {
            if (string.IsNullOrEmpty(SeanceDate))
            {
                SeanceDate = DateTime.Now.ToString();
            }

            DateTime SeanceDate_DateTime = Convert.ToDateTime(SeanceDate);
            Former former = new FormerBLO(this._UnitOfWork, this.GAppContext).Get_Current_Former();
            if(former == null)
            {
                // [Bug] Localization
                string msg_e = string.Format("Vous êtes pas un formateur, cette page est réservée pour les formateurs");
                Alert(msg_e, NotificationType.error);
                return RedirectToAction("Index");
            }
 
            msgHelper.Create(msg);

          


            Create_SeanceTraining_Model create_seancetraining_model = new Create_SeanceTraining_ModelBLM(this._UnitOfWork, this.GAppContext).CreateNew(SeanceDate_DateTime, former);

         

            return View(create_seancetraining_model);
        }

        public override ActionResult Edit(long? id)
        {
            return base.Edit(id);
        }
    }
}