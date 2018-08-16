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
using GApp.Entities.Resources.LogWorkResources;
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS.WebApp.Controllers
{  
    // Generated by GApp v 0.3.0 
    public class BaseLogWorksController : BaseController<TrainingISModel>
    {
        protected LogWorkBLO LogWorkBLO = null;

		public BaseLogWorksController()
        {
            this.msgHelper = new MessagesService(typeof(LogWork));
			this.LogWorkBLO = new LogWorkBLO(this._UnitOfWork, this.GAppContext) ;
        }

	    public virtual ActionResult Index()
        {
		    msgHelper.Index(msg);
            List<Default_Details_LogWork_Model> listDefault_Details_LogWork_Model = new List<Default_Details_LogWork_Model>();
			foreach (var item in LogWorkBLO.FindAll()){
                Default_Details_LogWork_Model Default_Details_LogWork_Model = new Default_Details_LogWork_ModelBLM(this._UnitOfWork, this.GAppContext) 
                    .ConverTo_Default_Details_LogWork_Model(item);
                listDefault_Details_LogWork_Model.Add(Default_Details_LogWork_Model);
            }
			return View(listDefault_Details_LogWork_Model);
		}

		private void Fill_ViewBag_Create(Default_Form_LogWork_Model Default_Form_LogWork_Model)
        {



        }

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			Default_Form_LogWork_Model default_form_logwork_model = new Default_Form_LogWork_ModelBLM(this._UnitOfWork, this.GAppContext) .CreateNew();
			this.Fill_ViewBag_Create(default_form_logwork_model);
			return View(default_form_logwork_model);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "UserId,OperationWorkType,OperationReference,EntityType,Description")] Default_Form_LogWork_Model Default_Form_LogWork_Model)
        {
			LogWork LogWork = null ;
			LogWork = new Default_Form_LogWork_ModelBLM(this._UnitOfWork, this.GAppContext) 
										.ConverTo_LogWork(Default_Form_LogWork_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    LogWorkBLO.Save(LogWork);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_LogWork.SingularName.ToLower(), LogWork), NotificationType.success);
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
			this.Fill_ViewBag_Create(Default_Form_LogWork_Model);
			return View(Default_Form_LogWork_Model);
        }

		private void Fill_Edit_ViewBag(Default_Form_LogWork_Model Default_Form_LogWork_Model)
        {
 



        }
		 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LogWork LogWork = LogWorkBLO.FindBaseEntityByID((long)id);
            if (LogWork == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_LogWork.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			Default_Form_LogWork_Model Default_Form_LogWork_Model = new Default_Form_LogWork_ModelBLM(this._UnitOfWork, this.GAppContext) 
                                                                .ConverTo_Default_Form_LogWork_Model(LogWork) ;

			this.Fill_Edit_ViewBag(Default_Form_LogWork_Model);
			return View(Default_Form_LogWork_Model);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "UserId,OperationWorkType,OperationReference,EntityType,Description,Id")] Default_Form_LogWork_Model Default_Form_LogWork_Model)	
        {
			LogWork LogWork = new Default_Form_LogWork_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_LogWork( Default_Form_LogWork_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    LogWorkBLO.Save(LogWork);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_LogWork.SingularName.ToLower(), LogWork), NotificationType.success);
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
			this.Fill_Edit_ViewBag(Default_Form_LogWork_Model);
			return View(Default_Form_LogWork_Model);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogWork LogWork = LogWorkBLO.FindBaseEntityByID((long) id);
            if (LogWork == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_LogWork.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			Default_Details_LogWork_Model Default_Details_LogWork_Model = new Default_Details_LogWork_Model();
		    Default_Details_LogWork_Model = new Default_Details_LogWork_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Default_Details_LogWork_Model(LogWork);


			return View(Default_Details_LogWork_Model);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LogWork LogWork = LogWorkBLO.FindBaseEntityByID((long)id);
            if (LogWork == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_LogWork.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_Details_LogWork_Model Default_Details_LogWork_Model = new Default_Details_LogWork_ModelBLM(this._UnitOfWork, this.GAppContext) 
							.ConverTo_Default_Details_LogWork_Model(LogWork);


			 return View(Default_Details_LogWork_Model);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			LogWork LogWork = LogWorkBLO.FindBaseEntityByID((long)id);
			if (LogWork == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_LogWork.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                LogWorkBLO.Delete(LogWork);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, LogWork.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_LogWork.SingularName.ToLower(), LogWork), NotificationType.success);
            return RedirectToAction("Index");
        }

		public virtual ActionResult Import()
        {
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
                ImportReport importReport = LogWorkBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/LogWorks/LastRepportFile\">Télécharger le rapport d'importation</a>";
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


        public virtual FileResult Export()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dataTable = LogWorkBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_LogWork.PluralName + ".xlsx");
                }
            }
        }

		public FileResult LastRepportFile()
        {
            // [Bug] if the user try to Import multiple data in the same time
            if (Session["path_repport"] != null)
            {
                string path = Session["path_repport"] as string;
                var fileStream = new FileStream(path, FileMode.Open);
                string FileName = "Rapport d'importation - " + DateTime.Now.ToString();
                return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Import_Repport" + ".xlsx");

            }
            return null;

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                LogWorkBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class LogWorksController : BaseLogWorksController{};
}

