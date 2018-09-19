using GApp.BLL.Enums;
using GApp.DAL.Exceptions;
using GApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.BLL.Exceptions;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Models.SeanceTrainings;
using TrainingIS.WebApp.Manager.Views.msgs;

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

        public override ActionResult Create(Create_SeanceTraining_Model Create_SeanceTraining_Model)
        {
            SeanceTraining SeanceTraining = null;
            SeanceTraining = new Create_SeanceTraining_ModelBLM(this._UnitOfWork, this.GAppContext)
                                        .ConverTo_SeanceTraining(Create_SeanceTraining_Model);

            bool dataBaseException = false;
            if (ModelState.IsValid)
            {
                // If SeanceTraining Exist
                string reference = SeanceTraining.CalculateReference();
                SeanceTraining ExistantSeanceTraining = SeanceTrainingBLO.FindBaseEntityByReference(reference);
                if (ExistantSeanceTraining != null)
                {
                    return RedirectToAction("Edit", new { Id = ExistantSeanceTraining.Id });
                }

                try
                {
                    SeanceTraining.FormerValidation = true;
                    SeanceTrainingBLO.Save(SeanceTraining);
                    Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_SeanceTraining.SingularName.ToLower(), SeanceTraining), NotificationType.success);
                    return RedirectToAction("Edit", new { Id = SeanceTraining.Id });
                }
                catch (GAppDbException ex)
                {
                    dataBaseException = true;
                    Alert(ex.Message, NotificationType.error);
                }
                catch (GAppException ex)
                {
                    dataBaseException = true;
                    Alert(ex.Message, NotificationType.error);
                }
            }
            if (!dataBaseException)
            {
                Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            }
            msgHelper.Create(msg);
            this.Fill_ViewBag_Create(Create_SeanceTraining_Model);
            Create_SeanceTraining_Model = new Create_SeanceTraining_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Create_SeanceTraining_Model(SeanceTraining);
            return View(Create_SeanceTraining_Model);
        }

        public override ActionResult Edit(long? id)
        {
            return base.Edit(id);
        }

        public override ActionResult DeleteConfirmed(long id)
        {
            string returnUrl = this.HttpContext.Request["returnUrl"] as string;
            if (returnUrl == null)
            {
                return base.DeleteConfirmed(id);
            }
            else
            {
                base.DeleteConfirmed(id);
                return Redirect(returnUrl);
            }
          
        }

        //public ActionResult Create_Not_Created_SeanceTraining()
        //{
        //    // to not calculate the statisitque
        //    this.GAppContext.Session.Add(ImportService.IMPORT_PROCESS_KEY, "true");

        //    this.SeanceTrainingBLO.Create_Not_Created_SeanceTraining();

        //    string msg_e = string.Format("Tous les seances de formation sont crées");
        //    Alert(msg_e, NotificationType.info);
        //    return RedirectToAction("Index");
        //}
    }
}