using GApp.BLL.Enums;
using GApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.WebApp.Manager.Views.msgs;

namespace TrainingIS.WebApp.Controllers
{
    public partial class TraineesController
    {

        //public override ActionResult Edit(Default_Form_Trainee_Model Default_Form_Trainee_Model)
        //{
        //    Trainee Trainee = new Default_Form_Trainee_ModelBLM(this._UnitOfWork, this.GAppContext)
        //         .ConverTo_Trainee(Default_Form_Trainee_Model);

        //    bool dataBaseException = false;
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            TraineeBLO.Save(Trainee);


        //            Alert(string.Format(msgManager.The_entity_has_been_changed, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Trainee.SingularName.ToLower(), Trainee), NotificationType.success);
        //            return RedirectToAction("Index");
        //        }
        //        catch (GAppException ex)
        //        {
        //            dataBaseException = true;
        //            Alert(ex.Message, NotificationType.error);
        //        }
        //    }
        //    if (!dataBaseException)
        //    {
        //        Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
        //    }
        //    msgHelper.Edit(msg);
        //    this.Fill_Edit_ViewBag(Default_Form_Trainee_Model);
        //    Default_Form_Trainee_Model = new Default_Form_Trainee_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_Form_Trainee_Model(Trainee);
        //    return View(Default_Form_Trainee_Model);
        //}

        public virtual ActionResult Absences(long? id)
        {
            msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee Trainee = TraineeBLO.FindBaseEntityByID((long)id);
            if (Trainee == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Trainee.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }


            return View(Trainee);
        }

    }
}