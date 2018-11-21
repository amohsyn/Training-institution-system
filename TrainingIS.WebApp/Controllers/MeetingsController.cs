using GApp.BLL.Enums;
using GApp.Entities;
using GApp.Exceptions;
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
using TrainingIS.Entities.Resources.MeetingResources;
using TrainingIS.Entities.Resources.WorkGroupResources;
using TrainingIS.Models.Meetings;
using TrainingIS.WebApp.Manager.Views.msgs;

namespace TrainingIS.WebApp.Controllers
{
    public partial class MeetingsController
    {

        #region Create
        protected override void Fill_ViewBag_Create(Create_Meeting_Model Form_Meeting_Model)
        {
            this.Fill_ViewBag_Form(Form_Meeting_Model);
        }
        protected  void Fill_ViewBag_Form(Form_Meeting_Model Form_Meeting_Model)
        {
            // workGroup
            WorkGroup workGroup = new WorkGroupBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(Form_Meeting_Model.WorkGroupId);
            ViewBag.WorkGroup = workGroup;


            ViewBag.Mission_Working_GroupId = new SelectList(
                 workGroup.Mission_Working_Groups,
                "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Form_Meeting_Model.Mission_Working_GroupId);

            ViewBag.WorkGroupId = new SelectList(
                new WorkGroupBLO(this._UnitOfWork, this.GAppContext).FindAll(),
                "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Form_Meeting_Model.WorkGroupId);


            if (workGroup.MemebersFormers != null)
            {
                ViewBag.Data_Selected_Presences_Of_Formers = workGroup.MemebersFormers.ToList<BaseEntity>();
                Form_Meeting_Model.Selected_Presences_Of_Formers = workGroup.MemebersFormers
                    .Select(m => m.Id.ToString())
                    .ToList();
            }


            if (workGroup.MemebersAdministrators != null)
            {
                ViewBag.Data_Selected_Presences_Of_Administrators = workGroup.MemebersAdministrators.ToList<BaseEntity>();
                Form_Meeting_Model.Selected_Presences_Of_Administrators = workGroup.MemebersAdministrators
                   .Select(m => m.Id.ToString())
                   .ToList();
            }


            if (workGroup.MemebersTrainees != null)
            {
                ViewBag.Data_Selected_Presences_Of_Trainees = workGroup.MemebersTrainees.ToList<BaseEntity>();
                Form_Meeting_Model.Selected_Presences_Of_Trainees = workGroup.MemebersTrainees
                   .Select(m => m.Id.ToString())
                   .ToList();
            }



            ViewBag.Data_Selected_Presences_Of_Guests_Formers = new FormerBLO(this._UnitOfWork, this.GAppContext).FindAll().ToList<BaseEntity>();

            ViewBag.Data_Selected_Presences_Of_Guests_Administrators = new AdministratorBLO(this._UnitOfWork, this.GAppContext).FindAll().ToList<BaseEntity>();

            ViewBag.Data_Selected_Presences_Of_Guests_Trainees = new TraineeBLO(this._UnitOfWork, this.GAppContext).FindAll().ToList<BaseEntity>();

        }

        /// <summary>
        /// To Create the meeting you must chose the workGroup
        /// </summary>
        /// <returns></returns>
        public override ActionResult Create()
        {
            msgHelper.Create(msg);
            List<WorkGroup> WorkGroups = new WorkGroupBLO(this._UnitOfWork, this.GAppContext).FindAll();
            ViewBag.WorkGroups = WorkGroups;
            return View("WorkGroupeChoice");
        }

        public ActionResult Create_By_WorkGroup(string WorkGroup_Code)
        {
            WorkGroup workGroup = new WorkGroupBLO(this._UnitOfWork, this.GAppContext).FindByCode(WorkGroup_Code);
            if (workGroup == null)
            {
                string msg = string.Format("Vous essayer de créer une réunion avec un conseil ou une comité qui n'exist pas");
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

            Meeting Meeting = this.MeetingBLO.CreateInstance();
            Meeting.WorkGroup = workGroup;

            Create_Meeting_Model Form_Meeting_Model = new Create_Meeting_ModelBLM(this._UnitOfWork, this.GAppContext)
                .ConverTo_Create_Meeting_Model(Meeting);


            this.Fill_ViewBag_Create(Form_Meeting_Model);
            msgHelper.Create(msg);

            return View("Create", Form_Meeting_Model);
        }

        //public ActionResult Create_By_InValidSanction(long Id)
        //{
        //    // BLO
        //    SanctionBLO sanctionBLO = new SanctionBLO(this._UnitOfWork, this.GAppContext);
        //    WorkGroupBLO workGroupBLO = new WorkGroupBLO(this._UnitOfWork, this.GAppContext);
        //    Mission_Working_GroupBLO mission_Working_GroupBLO = new Mission_Working_GroupBLO(this._UnitOfWork, this.GAppContext);
        //    // Find the sanction
        //    Sanction Sanction = sanctionBLO.FindBaseEntityByID((long)Id);

        //    // Find the WorkGroup
        //    WorkGroup workGroup = workGroupBLO.Find_By_Mission_Workgin_Group(Sanction.Id);
        //    if (workGroup == null)
        //    {
        //        // [Localization]
        //        string msg = string.Format("il n'y a pas des conseils ou des comités qui traitent cette catégorie du sanction : {0}, dans la base de données.", Sanction.SanctionCategory.ToString(););
        //        Alert(msg, NotificationType.error);
        //        return RedirectToAction("Index");
        //    }

        //    // mission_Working_GroupBLO
        //    Mission_Working_Group mission_Working_Group = mission_Working_GroupBLO.Find_By_Sanction(Sanction.Id);
        //    if (mission_Working_Group == null)
        //    {
        //        // [Localization]
        //        string msg = string.Format("il n'y a pas des conseils ou des comités qui traitent cette catégorie du sanction : {0}, dans la base de données.", Sanction.SanctionCategory.ToString(););
        //        Alert(msg, NotificationType.error);
        //        return RedirectToAction("Index");
        //    }

        //    Meeting Meeting = this.MeetingBLO.CreateInstance();
        //    Meeting.WorkGroup = workGroup;
        //    Meeting.Mission_Working_Group = mission_Working_Group;
           

        //    Create_Meeting_Model Form_Meeting_Model = new Create_Meeting_ModelBLM(this._UnitOfWork, this.GAppContext)
        //        .ConverTo_Create_Meeting_Model(Meeting);


        //    this.Fill_ViewBag_Create(Form_Meeting_Model);
        //    msgHelper.Create(msg);

        //    return View("Create", Form_Meeting_Model);
        //}


        /// <summary>
        /// Create Step 1 : Save the metting and redirect to Step 2 : Décision of meeting
        /// </summary>
        /// <param name="Form_Meeting_Model"></param>
        /// <returns></returns>
        public override ActionResult Create(Create_Meeting_Model Form_Meeting_Model)
        {
            Meeting Meeting = null;
            Meeting = new Form_Meeting_ModelBLM(this._UnitOfWork, this.GAppContext)
                                        .ConverTo_Meeting(Form_Meeting_Model);
            if (ModelState.IsValid)
            {
                try
                {
                    MeetingBLO.Save(Meeting);
                    Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Meeting.SingularName.ToLower(), Meeting), NotificationType.success);
                    return RedirectToAction("Edit", new { id = Meeting.Id });

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
            this.Fill_ViewBag_Create(Form_Meeting_Model);
            Form_Meeting_Model = new Create_Meeting_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Create_Meeting_Model(Meeting);
            return View(Form_Meeting_Model);
        }
        #endregion

        #region Edit
        protected override void Fill_Edit_ViewBag(Edit_Meeting_Model Form_Meeting_Model)
        {
            this.Fill_Edit_Form_ViewBag(Form_Meeting_Model);


        }
        protected  void Fill_Edit_Form_ViewBag(Form_Meeting_Model Form_Meeting_Model)
        {
            this.Fill_ViewBag_Form(Form_Meeting_Model);

            var isEdit = this.MeetingBLO.isHaveDecision(Form_Meeting_Model.Id);
            var DecisionInfo = this.MeetingBLO.GetDecisionInfo(Form_Meeting_Model.Id);
            ViewBag.DecisionInfo = DecisionInfo;
            ViewBag.isEdit = isEdit;


        }


        public override ActionResult Edit(Edit_Meeting_Model Form_Meeting_Model)
        {
            Meeting Meeting = new Form_Meeting_ModelBLM(this._UnitOfWork, this.GAppContext)
                 .ConverTo_Meeting(Form_Meeting_Model);

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
            this.Fill_Edit_Form_ViewBag(Form_Meeting_Model);
            Form_Meeting_Model = new Edit_Meeting_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Edit_Meeting_Model(Meeting);
            return View(Form_Meeting_Model);
        }

        public ActionResult Edit_Meeting_Form(long? id)
        {
            var ViewResult = base.Edit(id) as ViewResult;
            return View(ViewResult.Model);
        }
        #endregion

    }
}