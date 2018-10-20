using GApp.BLL.Enums;
using GApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.Resources.MeetingResources;
using TrainingIS.WebApp.Manager.Views.msgs;

namespace TrainingIS.WebApp.Controllers
{
    public partial class MeetingsController
    {
        #region Create
        /// <summary>
        /// Create Step 1 : Save the metting and redirect to Step 2 : Décision of meeting
        /// </summary>
        /// <param name="Default_Form_Meeting_Model"></param>
        /// <returns></returns>
        public override ActionResult Create(Default_Form_Meeting_Model Default_Form_Meeting_Model)
        {
            Meeting Meeting = null;
            Meeting = new Default_Form_Meeting_ModelBLM(this._UnitOfWork, this.GAppContext)
                                        .ConverTo_Meeting(Default_Form_Meeting_Model);
            if (ModelState.IsValid)
            {
                try
                {
                    MeetingBLO.Save(Meeting);
                    Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Meeting.SingularName.ToLower(), Meeting), NotificationType.success);
                    return RedirectToAction("Edit",new { id = Meeting.Id });

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
            this.Fill_ViewBag_Create(Default_Form_Meeting_Model);
            Default_Form_Meeting_Model = new Default_Form_Meeting_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_Form_Meeting_Model(Meeting);
            return View(Default_Form_Meeting_Model);
        }
        #endregion

        #region Edit
        protected override void Fill_Edit_ViewBag(Default_Form_Meeting_Model Default_Form_Meeting_Model)
        {
            base.Fill_Edit_ViewBag(Default_Form_Meeting_Model);

            var isEdit = this.MeetingBLO.isHaveDecision(Default_Form_Meeting_Model.Id);
            var DecisionInfo = this.MeetingBLO.GetDecisionInfo(Default_Form_Meeting_Model.Id);
            ViewBag.DecisionInfo = DecisionInfo;
            ViewBag.isEdit = isEdit;

            
        }


        public override ActionResult Edit(Default_Form_Meeting_Model Default_Form_Meeting_Model)
        {
            Meeting Meeting = new Default_Form_Meeting_ModelBLM(this._UnitOfWork, this.GAppContext)
                 .ConverTo_Meeting(Default_Form_Meeting_Model);

            if (ModelState.IsValid)
            {
                try
                {
                    MeetingBLO.Save(Meeting);
                    Alert(string.Format(msgManager.The_entity_has_been_changed, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Meeting.SingularName.ToLower(), Meeting), NotificationType.success);
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
            this.Fill_Edit_ViewBag(Default_Form_Meeting_Model);
            Default_Form_Meeting_Model = new Default_Form_Meeting_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_Form_Meeting_Model(Meeting);
            return View(Default_Form_Meeting_Model);
        }

        public ActionResult Edit_Meeting_Form(long? id)
        {
            var ViewResult = base.Edit(id) as ViewResult;
            return View(ViewResult.Model);
        }
        #endregion

    }
}