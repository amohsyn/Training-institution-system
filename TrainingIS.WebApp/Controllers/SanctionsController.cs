using ClosedXML.Excel;
using GApp.BLL.Enums;
using GApp.Entities;
using GApp.Exceptions;
using GApp.Models.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainingIS.BLL;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.BLL.Services.Import;
using TrainingIS.Entities;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.Resources.SanctionResources;
using TrainingIS.WebApp.Manager.Views.msgs;

namespace TrainingIS.WebApp.Controllers
{
    public partial class SanctionsController
    {
        #region Create Sanction
        /// <summary>
        /// to Create Sanction you must use Create_Sanction action
        /// </summary>
        /// <returns></returns>
        public override ActionResult Create()
        {
            string msg_redirect = string.Format("Vous devez ajouter une réuion pour créer une sanction");
            Alert(msg_redirect, NotificationType.info);
            return RedirectToAction("Create","Meetings");
        }

        /// <summary>
        /// Create_Sanction use the CreateView
        /// </summary>
        /// <param name="MeetingId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create_Sanction(Int64 MeetingId)
        {
            msgHelper.Create(msg);
            Default_Form_Sanction_Model default_form_sanction_model = new Default_Form_Sanction_ModelBLM(this._UnitOfWork, this.GAppContext).CreateNew();
            default_form_sanction_model.MeetingId = MeetingId;
            this.Fill_ViewBag_Create(default_form_sanction_model);
            return View("Create", default_form_sanction_model);
        }
        protected override void Fill_ViewBag_Create(Default_Form_Sanction_Model Default_Form_Sanction_Model)
        {
            MeetingBLO meetingBLO = new MeetingBLO(this._UnitOfWork, this.GAppContext);
            SanctionCategoryBLO sanctionCategoryBLO = new SanctionCategoryBLO(this._UnitOfWork, this.GAppContext);

            Meeting meeting = meetingBLO.FindBaseEntityByID(Default_Form_Sanction_Model.MeetingId);
            List<SanctionCategory> sanctionCategories = sanctionCategoryBLO.Find_By_DecisionAuthority(meeting.Mission_Working_Group.DecisionAuthority);
            ViewBag.MeetingId = new SelectList(new MeetingBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Sanction_Model.MeetingId);

            // Fin the sanctions category by Decipline category readed from Mission of Meeting
            ViewBag.SanctionCategoryId = new SelectList(sanctionCategories, "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Sanction_Model.SanctionCategoryId);

            ViewBag.TraineeId = new SelectList(new TraineeBLO(this._UnitOfWork, this.GAppContext).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Sanction_Model.TraineeId);

        }

        public ActionResult Create_Sanction_Form(Int64 MeetingId)
        {
            msgHelper.Create(msg);
            Default_Form_Sanction_Model default_form_sanction_model = new Default_Form_Sanction_ModelBLM(this._UnitOfWork, this.GAppContext).CreateNew();
            default_form_sanction_model.MeetingId = MeetingId;
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

        #endregion

        #region Edit Sanction
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
        #endregion

        #region Export and Import
        /// <summary>
        /// [Generalize] Export the filterted Sanctions
        /// </summary>
        /// <returns></returns>
        public override FileResult Export()
        {
            Int32 _TotalRecords = 0;
            List<string> SearchCreteria = this.GetSearchCreteria();
            List<Export_Sanction_Model> _ListDefault_Details_Sanction_Model = null;
            FilterRequestParams filterRequestParams = null;
            try
            {
                filterRequestParams = this.Save_OR_Load_filterRequestParams_State(filterRequestParams);
                filterRequestParams.pageSize = 0;
                _ListDefault_Details_Sanction_Model = new Export_Sanction_ModelBLM(this._UnitOfWork, this.GAppContext)
                    .Find(filterRequestParams, SearchCreteria, out _TotalRecords);

            }
            catch (Exception ex)
            {
                filterRequestParams = new FilterRequestParams();
                this.Delete_filterRequestParams_State();
                filterRequestParams.pageSize = 0;
                _ListDefault_Details_Sanction_Model = new Export_Sanction_ModelBLM(this._UnitOfWork, this.GAppContext)
                  .Find(filterRequestParams, SearchCreteria, out _TotalRecords);
            }

            ExportService exportService = new ExportService(typeof(Sanction), typeof(Export_Sanction_Model));
            DataTable dataTable = exportService.CreateDataTable(msg_Sanction.PluralName);
            exportService.Fill(dataTable, _ListDefault_Details_Sanction_Model.Cast<object>().ToList());
 
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string FileName = string.Format("{0}-{1}", msg_Sanction.PluralName, DateTime.Now.ToShortDateString());
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName + ".xlsx");
                }
            }
        }
        public FileResult Import_File_Example()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dataTable = SanctionBLO.Import_File_Example();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string FileName = string.Format("{0}-{1}", msg_Sanction.PluralName, DateTime.Now.ToShortDateString());
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName + ".xlsx");
                }
            }
        }
        #endregion

    }
}