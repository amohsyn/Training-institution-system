﻿using System;
using System.Collections.Generic; 
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.BLL;
using TrainingIS.BLL.Exceptions;
using GApp.DAL.ReadExcel;
using ClosedXML.Excel;
using System.IO;     
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions; 
using GApp.Entities;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities.Base;
using GApp.WebApp.Controllers;
using GApp.BLL.Services;
using GApp.BLL.Enums;
using TrainingIS.Entities.Resources.AbsenceResources;
using TrainingIS.Models.Absences;
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS.WebApp.Controllers
{  
    // Generated by GApp v 0.3.0 
    public class BaseAbsencesController : BaseController<TrainingISModel>
    {
        protected AbsenceBLO AbsenceBLO = null;

		public BaseAbsencesController()
        {
            this.msgHelper = new MessagesService(typeof(Absence));
			this.AbsenceBLO = new AbsenceBLO(this._UnitOfWork, this.GAppContext) ;
        }

	    public virtual ActionResult Index()
        {
		    msgHelper.Index(msg);
            List<Index_Absence_Model> listIndex_Absence_Model = new List<Index_Absence_Model>();
			foreach (var item in AbsenceBLO.FindAll()){
                Index_Absence_Model Index_Absence_Model = new Index_Absence_ModelBLM(this._UnitOfWork, this.GAppContext) 
                    .ConverTo_Index_Absence_Model(item);
                listIndex_Absence_Model.Add(Index_Absence_Model);
            }
			return View(listIndex_Absence_Model);
		}

		private void Fill_ViewBag_Create(Create_Absence_Model Create_Absence_Model)
        {
		ViewBag.SeancePlanningId = new SelectList(new SeancePlanningBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Create_Absence_Model.SeancePlanningId);
		ViewBag.SeanceTrainingId = new SelectList(new SeanceTrainingBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Create_Absence_Model.SeanceTrainingId);
		ViewBag.TraineeId = new SelectList(new TraineeBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Create_Absence_Model.TraineeId);



        }

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			Create_Absence_Model create_absence_model = new Create_Absence_ModelBLM(this._UnitOfWork, this.GAppContext) .CreateNew();
			this.Fill_ViewBag_Create(create_absence_model);
			return View(create_absence_model);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "TraineeId,isHaveAuthorization,SeanceTrainingId,SeancePlanningId,FormerComment,TraineeComment,SupervisorComment")] Create_Absence_Model Create_Absence_Model)
        {
			Absence Absence = null ;
			Absence = new Create_Absence_ModelBLM(this._UnitOfWork, this.GAppContext) 
										.ConverTo_Absence(Create_Absence_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    AbsenceBLO.Save(Absence);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Absence.SingularName.ToLower(), Absence), NotificationType.success);
					return RedirectToAction("Index");
                }
                catch (GAppDbException ex)
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
			this.Fill_ViewBag_Create(Create_Absence_Model);
			return View(Create_Absence_Model);
        }

		private void Fill_Edit_ViewBag(Edit_Absence_Model Edit_Absence_Model)
        {
			ViewBag.TraineeId = new SelectList(new TraineeBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Edit_Absence_Model.TraineeId);
 



        }
		 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Absence Absence = AbsenceBLO.FindBaseEntityByID((long)id);
            if (Absence == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Absence.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			Edit_Absence_Model Edit_Absence_Model = new Edit_Absence_ModelBLM(this._UnitOfWork, this.GAppContext) 
                                                                .ConverTo_Edit_Absence_Model(Absence) ;

			this.Fill_Edit_ViewBag(Edit_Absence_Model);
			return View(Edit_Absence_Model);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "TraineeId,isHaveAuthorization,SeanceTraining,SeancePlanning,FormerComment,TraineeComment,SupervisorComment,Id")] Edit_Absence_Model Edit_Absence_Model)	
        {
			Absence Absence = new Edit_Absence_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Absence( Edit_Absence_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    AbsenceBLO.Save(Absence);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Absence.SingularName.ToLower(), Absence), NotificationType.success);
					return RedirectToAction("Index");
                }
                catch (GAppDbException ex)
                {
					dataBaseException = true;
                    Alert(ex.Message, NotificationType.error);
                }
            }
			if (!dataBaseException) 
            {
                Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            }
			msgHelper.Edit(msg);
			this.Fill_Edit_ViewBag(Edit_Absence_Model);
			return View(Edit_Absence_Model);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Absence Absence = AbsenceBLO.FindBaseEntityByID((long) id);
            if (Absence == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Absence.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			Details_Absence_Model Details_Absence_Model = new Details_Absence_Model();
		    Details_Absence_Model = new Details_Absence_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Details_Absence_Model(Absence);


			return View(Details_Absence_Model);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Absence Absence = AbsenceBLO.FindBaseEntityByID((long)id);
            if (Absence == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Absence.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Details_Absence_Model Details_Absence_Model = new Details_Absence_ModelBLM(this._UnitOfWork, this.GAppContext) 
							.ConverTo_Details_Absence_Model(Absence);


			 return View(Details_Absence_Model);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			Absence Absence = AbsenceBLO.FindBaseEntityByID((long)id);
			if (Absence == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Absence.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                AbsenceBLO.Delete(Absence);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, Absence.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Absence.SingularName.ToLower(), Absence), NotificationType.success);
            return RedirectToAction("Index");
        }

		public virtual ActionResult Import()
        { 
			this.Create_Files_Directory_If_Not_Exist();
            string FileName = "Import_" + Guid.NewGuid().ToString() + ".xlsx";

            //Save excel file to the server
            HttpPostedFileBase postedFile = Request.Files["import_objects"];
            string path = Server.MapPath("~/Content/Files/" + FileName);
            postedFile.SaveAs(path);

            //Save to database
            var excelData = new ExcelData(path);
            DataTable firstTable = excelData.getFirstTable();
            try
            {
                ImportReport importReport = AbsenceBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
					Session["repport_name"] = msg_Absence.PluralName;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/Absences/LastRepportFile\">Télécharger le rapport d'importation</a>";
                    importReport.AddMessage(a_download, MessagesService.MessageTypes.Meta_msg);

                }

                // Show HTML Report
                Message(importReport.get_HTML_Report(), NotificationType.info);
            }
            catch (ImportException e)
            {
                Message(e.Message, NotificationType.error);
            }
            return RedirectToAction("Index");
        }

		private void Create_Files_Directory_If_Not_Exist()
        {
            string Files_path = Server.MapPath("~/Content/Files");
            if(!Directory.Exists(Files_path))
            {
                Directory.CreateDirectory(Files_path);

            }
        }

        public virtual FileResult Export()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dataTable = AbsenceBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
					string FileName = string.Format("{0}-{1}", msg_Absence.PluralName, DateTime.Now.ToShortDateString());
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName + ".xlsx");
                }
            }
        }

		public FileResult LastRepportFile()
        {
            // [Bug] if the user try to Import multiple data in the same time
            if (Session["path_repport"] != null)
            {
                string path = Session["path_repport"] as string;
				string name = Session["repport_name"] as string;

                var fileStream = new FileStream(path, FileMode.Open);
                string FileName = string.Format("Rapport d'import-{0}-{1}",name,DateTime.Now.ToShortDateString());
                return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName + ".xlsx");

            }
            return null;

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                AbsenceBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class AbsencesController : BaseAbsencesController{};
}

