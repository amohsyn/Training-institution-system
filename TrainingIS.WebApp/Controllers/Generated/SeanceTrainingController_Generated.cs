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
using static TrainingIS.WebApp.Enums.Enums;
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions; 
using GApp.Entities;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS.WebApp.Controllers
{  
    // Generated by Manager v 0.2.0 
    public class BaseSeanceTrainingsController : BaseController
    {
        protected SeanceTrainingBLO SeanceTrainingBLO = null;

		public BaseSeanceTrainingsController()
        {
            this.msgHelper = new MsgViews(typeof(SeanceTraining));
			this.SeanceTrainingBLO = new SeanceTrainingBLO(this._UnitOfWork);
        }

	    public virtual ActionResult Index()
        {
		    msgHelper.Index(msg);
            List<Default_SeanceTrainingDetailsView> listDefault_SeanceTrainingDetailsView = new List<Default_SeanceTrainingDetailsView>();
			foreach (var item in SeanceTrainingBLO.FindAll()){
                Default_SeanceTrainingDetailsView Default_SeanceTrainingDetailsView = new Default_SeanceTrainingDetailsViewBLM(this._UnitOfWork)
                    .ConverTo_Default_SeanceTrainingDetailsView(item);
                listDefault_SeanceTrainingDetailsView.Add(Default_SeanceTrainingDetailsView);
            }
			return View(listDefault_SeanceTrainingDetailsView);
		}

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "SeanceDate,SeancePlanningId")] Default_SeanceTrainingFormView Default_SeanceTrainingFormView)
        {
			SeanceTraining SeanceTraining = null ;
			SeanceTraining = new Default_SeanceTrainingFormViewBLM(this._UnitOfWork)
										.ConverTo_SeanceTraining(Default_SeanceTrainingFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    SeanceTrainingBLO.Save(SeanceTraining);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msg_SeanceTraining.SingularName, SeanceTraining), NotificationType.success);
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
			ViewBag.SeancePlanningId = new SelectList(new SeancePlanningBLO(this._UnitOfWork).FindAll(), "Id", "Code", SeanceTraining.SeancePlanningId);
			return View(Default_SeanceTrainingFormView);
        }


		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			ViewBag.SeancePlanningId = new SelectList(new SeancePlanningBLO(this._UnitOfWork).FindAll(), "Id", "Code");
            SeanceTraining seancetraining = new SeanceTraining();
            Default_SeanceTrainingFormView default_seancetrainingformview = new Default_SeanceTrainingFormViewBLM(this._UnitOfWork)
                                        .ConverTo_Default_SeanceTrainingFormView(seancetraining);
            return View(default_seancetrainingformview);
        } 
		 
       


		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeanceTraining SeanceTraining = SeanceTrainingBLO.FindBaseEntityByID((long)id);
            if (SeanceTraining == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeanceTraining.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			Default_SeanceTrainingFormView Default_SeanceTrainingFormView = new Default_SeanceTrainingFormViewBLM(this._UnitOfWork)
                                                                .ConverTo_Default_SeanceTrainingFormView(SeanceTraining) ;

			ViewBag.SeancePlanningId = new SelectList(new SeancePlanningBLO(this._UnitOfWork).FindAll(), "Id", "Code", Default_SeanceTrainingFormView.SeancePlanningId);
 
			return View(Default_SeanceTrainingFormView);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "SeanceDate,SeancePlanningId,Id")] Default_SeanceTrainingFormView Default_SeanceTrainingFormView)	
        {
			SeanceTraining SeanceTraining = new Default_SeanceTrainingFormViewBLM(this._UnitOfWork)
                .ConverTo_SeanceTraining( Default_SeanceTrainingFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    SeanceTrainingBLO.Save(SeanceTraining);
					Alert(string.Format(msgManager.The_entity_has_been_changed, msg_SeanceTraining.SingularName, SeanceTraining), NotificationType.success);
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

			ViewBag.SeancePlanningId = new SelectList(new SeancePlanningBLO(this._UnitOfWork).FindAll(), "Id", "Code", Default_SeanceTrainingFormView.SeancePlanningId);
			return View(Default_SeanceTrainingFormView);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeanceTraining SeanceTraining = SeanceTrainingBLO.FindBaseEntityByID((long) id);
            if (SeanceTraining == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeanceTraining.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			Default_SeanceTrainingDetailsView Default_SeanceTrainingDetailsView = new Default_SeanceTrainingDetailsView();
		    Default_SeanceTrainingDetailsView = new Default_SeanceTrainingDetailsViewBLM(this._UnitOfWork)
                .ConverTo_Default_SeanceTrainingDetailsView(SeanceTraining);


			return View(Default_SeanceTrainingDetailsView);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeanceTraining SeanceTraining = SeanceTrainingBLO.FindBaseEntityByID((long)id);
            if (SeanceTraining == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeanceTraining.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_SeanceTrainingDetailsView Default_SeanceTrainingDetailsView = new Default_SeanceTrainingDetailsViewBLM(this._UnitOfWork)
							.ConverTo_Default_SeanceTrainingDetailsView(SeanceTraining);


			 return View(Default_SeanceTrainingDetailsView);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			SeanceTraining SeanceTraining = SeanceTrainingBLO.FindBaseEntityByID((long)id);
			if (SeanceTraining == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeanceTraining.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                SeanceTrainingBLO.Delete(SeanceTraining);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, SeanceTraining.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed, msg_SeanceTraining.SingularName, SeanceTraining), NotificationType.success);
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
                ImportReport importReport = SeanceTrainingBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/SeanceTrainings/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
                DataTable dataTable = SeanceTrainingBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_SeanceTraining.PluralName + ".xlsx");
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
                SeanceTrainingBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class SeanceTrainingsController : BaseSeanceTrainingsController{};
}

