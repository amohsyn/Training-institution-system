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
using TrainingIS.Entities.Resources.SanctionResources;
using TrainingIS.WebApp.Manager.Views.msgs;

namespace TrainingIS.WebApp.Controllers
{
    public partial class SanctionsController
    {

        public override ActionResult Create()
        {
            string msg_redirect = string.Format("Vous devez ajouter une réuion pour créer une sanction");
            Alert(msg_redirect, NotificationType.info);
            return RedirectToAction("Create","Meetings");
        }

        [HttpGet]
        public ActionResult Create_Sanction(Int64 MeetingId)
        {
            msgHelper.Create(msg);
            Default_Form_Sanction_Model default_form_sanction_model = new Default_Form_Sanction_ModelBLM(this._UnitOfWork, this.GAppContext).CreateNew();
            default_form_sanction_model.MeetingId = MeetingId;
            this.Fill_ViewBag_Create(default_form_sanction_model);
            return View("Create", default_form_sanction_model);
        }

        public ActionResult Create_Sanction_Form(Int64 MeetingId)
        {
            msgHelper.Create(msg);
            Default_Form_Sanction_Model default_form_sanction_model = new Default_Form_Sanction_ModelBLM(this._UnitOfWork, this.GAppContext).CreateNew();
            this.Fill_ViewBag_Create(default_form_sanction_model);
            return View(default_form_sanction_model);
        }

        public override ActionResult Create(Default_Form_Sanction_Model Default_Form_Sanction_Model)
        {
            Sanction Sanction = null;
            Sanction = new Default_Form_Sanction_ModelBLM(this._UnitOfWork, this.GAppContext)
                                        .ConverTo_Sanction(Default_Form_Sanction_Model);

            if (ModelState.IsValid)
            {
                try
                {
                    SanctionBLO.Save(Sanction);
                    Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Sanction.SingularName.ToLower(), Sanction), NotificationType.success);
                    return RedirectToAction("Edit",new { id = Sanction.Id });
                }
                catch (GAppException ex)
                {
                    Alert(ex.Message, NotificationType.error);
                }
            }
            else
            {
                Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            }
            msgHelper.Create(msg);
            this.Fill_ViewBag_Create(Default_Form_Sanction_Model);
            Default_Form_Sanction_Model = new Default_Form_Sanction_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_Form_Sanction_Model(Sanction);
            return View(Default_Form_Sanction_Model);
        }


        public ActionResult Edit_Sanction_Form(long? id)
        {
            ViewResult viewResult = base.Edit(id) as ViewResult;
            return View(viewResult.Model);
        }


        public override ActionResult Edit(long? id)
        {
            return base.Edit(id);
        }

        public override ActionResult Edit(Default_Form_Sanction_Model Default_Form_Sanction_Model)
        {
            Sanction Sanction = new Default_Form_Sanction_ModelBLM(this._UnitOfWork, this.GAppContext)
                .ConverTo_Sanction(Default_Form_Sanction_Model);

            if (ModelState.IsValid)
            {
                try
                {
                    SanctionBLO.Save(Sanction);
                    Alert(string.Format(msgManager.The_entity_has_been_changed, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Sanction.SingularName.ToLower(), Sanction), NotificationType.success);
                    // return RedirectToAction("Index");
                }
                catch (GAppException ex)
                {
                    Alert(ex.Message, NotificationType.error);
                }
            }
            else 
            {
                Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            }
            msgHelper.Edit(msg);
            this.Fill_Edit_ViewBag(Default_Form_Sanction_Model);
            Default_Form_Sanction_Model = new Default_Form_Sanction_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_Form_Sanction_Model(Sanction);
            return View(Default_Form_Sanction_Model);
        }
        //[HttpGet]
        //public ActionResult Edit_Decision(long? id)
        //{
        //    bool dataBaseException = false;
        //    msgHelper.Edit(msg);
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Sanction Sanction = SanctionBLO.FindBaseEntityByID((long)id);
        //    if (Sanction == null)
        //    {
        //        string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Sanction.SingularName.ToLower());
        //        Alert(msg, NotificationType.error);
        //        return this.RedirectToAction("Edit", "Meetings", new { id = Sanction.MeetingId });
        //    }
        //    Default_Form_Sanction_Model Default_Form_Sanction_Model = new Default_Form_Sanction_ModelBLM(this._UnitOfWork, this.GAppContext)
        //                                                        .ConverTo_Default_Form_Sanction_Model(Sanction);

        //    this.Fill_Edit_ViewBag(Default_Form_Sanction_Model);
        //    return View(Default_Form_Sanction_Model);


        //    Sanction sanction = this.SanctionBLO.FindBaseEntityByID( Convert.ToInt64( id));

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public  ActionResult Edit_Decision(Default_Form_Sanction_Model Default_Form_Sanction_Model)
        //{
        //    ActionResult actionResult = base.Edit(Default_Form_Sanction_Model);

        //}

        //public ActionResult Edit_With_Meeting(Int64 MeetingId)
        //{
        //    base.Edit()
        //}
    }
}