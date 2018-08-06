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
using TrainingIS.Entities.Resources.TrainingResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions; 
using GApp.Entities;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS.WebApp.Controllers
{  
    // Generated by Manager v 0.2.0 
    public class BaseTrainingsController : BaseController
    {
        protected TrainingBLO TrainingBLO = null;

		public BaseTrainingsController()
        {
            this.msgHelper = new MsgViews(typeof(Training));
			this.TrainingBLO = new TrainingBLO(this._UnitOfWork);
        }

	    public virtual ActionResult Index()
        {
		    msgHelper.Index(msg);
            List<Default_TrainingDetailsView> listDefault_TrainingDetailsView = new List<Default_TrainingDetailsView>();
			foreach (var item in TrainingBLO.FindAll()){
                Default_TrainingDetailsView Default_TrainingDetailsView = new Default_TrainingDetailsViewBLM(this._UnitOfWork)
                    .ConverTo_Default_TrainingDetailsView(item);
                listDefault_TrainingDetailsView.Add(Default_TrainingDetailsView);
            }
			return View(listDefault_TrainingDetailsView);
		}

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "TrainingYearId,ModuleTrainingId,FormerId,GroupId,Code,Description")] Default_TrainingFormView Default_TrainingFormView)
        {
			Training Training = null ;
			Training = new Default_TrainingFormViewBLM(this._UnitOfWork)
										.ConverTo_Training(Default_TrainingFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    TrainingBLO.Save(Training);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msg_Training.SingularName, Training), NotificationType.success);
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
			ViewBag.FormerId = new SelectList(new FormerBLO(this._UnitOfWork).FindAll(), "Id", "Code", Training.FormerId);
			ViewBag.GroupId = new SelectList(new GroupBLO(this._UnitOfWork).FindAll(), "Id", "Code", Training.GroupId);
			ViewBag.ModuleTrainingId = new SelectList(new ModuleTrainingBLO(this._UnitOfWork).FindAll(), "Id", "Code", Training.ModuleTrainingId);
			ViewBag.TrainingYearId = new SelectList(new TrainingYearBLO(this._UnitOfWork).FindAll(), "Id", "Code", Training.TrainingYearId);
			return View(Default_TrainingFormView);
        }


		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			ViewBag.FormerId = new SelectList(new FormerBLO(this._UnitOfWork).FindAll(), "Id", "Code");
			ViewBag.GroupId = new SelectList(new GroupBLO(this._UnitOfWork).FindAll(), "Id", "Code");
			ViewBag.ModuleTrainingId = new SelectList(new ModuleTrainingBLO(this._UnitOfWork).FindAll(), "Id", "Code");
			ViewBag.TrainingYearId = new SelectList(new TrainingYearBLO(this._UnitOfWork).FindAll(), "Id", "Code");
            Training training = new Training();
            Default_TrainingFormView default_trainingformview = new Default_TrainingFormViewBLM(this._UnitOfWork)
                                        .ConverTo_Default_TrainingFormView(training);
            return View(default_trainingformview);
        } 
		 
       


		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Training Training = TrainingBLO.FindBaseEntityByID((long)id);
            if (Training == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Training.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			Default_TrainingFormView Default_TrainingFormView = new Default_TrainingFormViewBLM(this._UnitOfWork)
                                                                .ConverTo_Default_TrainingFormView(Training) ;

			ViewBag.FormerId = new SelectList(new FormerBLO(this._UnitOfWork).FindAll(), "Id", "Code", Default_TrainingFormView.FormerId);
			ViewBag.GroupId = new SelectList(new GroupBLO(this._UnitOfWork).FindAll(), "Id", "Code", Default_TrainingFormView.GroupId);
			ViewBag.ModuleTrainingId = new SelectList(new ModuleTrainingBLO(this._UnitOfWork).FindAll(), "Id", "Code", Default_TrainingFormView.ModuleTrainingId);
			ViewBag.TrainingYearId = new SelectList(new TrainingYearBLO(this._UnitOfWork).FindAll(), "Id", "Code", Default_TrainingFormView.TrainingYearId);
 
			return View(Default_TrainingFormView);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "TrainingYearId,ModuleTrainingId,FormerId,GroupId,Code,Description,Id")] Default_TrainingFormView Default_TrainingFormView)	
        {
			Training Training = new Default_TrainingFormViewBLM(this._UnitOfWork)
                .ConverTo_Training( Default_TrainingFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    TrainingBLO.Save(Training);
					Alert(string.Format(msgManager.The_entity_has_been_changed, msg_Training.SingularName, Training), NotificationType.success);
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

			ViewBag.FormerId = new SelectList(new FormerBLO(this._UnitOfWork).FindAll(), "Id", "Code", Default_TrainingFormView.FormerId);
			ViewBag.GroupId = new SelectList(new GroupBLO(this._UnitOfWork).FindAll(), "Id", "Code", Default_TrainingFormView.GroupId);
			ViewBag.ModuleTrainingId = new SelectList(new ModuleTrainingBLO(this._UnitOfWork).FindAll(), "Id", "Code", Default_TrainingFormView.ModuleTrainingId);
			ViewBag.TrainingYearId = new SelectList(new TrainingYearBLO(this._UnitOfWork).FindAll(), "Id", "Code", Default_TrainingFormView.TrainingYearId);
			return View(Default_TrainingFormView);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training Training = TrainingBLO.FindBaseEntityByID((long) id);
            if (Training == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Training.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			Default_TrainingDetailsView Default_TrainingDetailsView = new Default_TrainingDetailsView();
		    Default_TrainingDetailsView = new Default_TrainingDetailsViewBLM(this._UnitOfWork)
                .ConverTo_Default_TrainingDetailsView(Training);


			return View(Default_TrainingDetailsView);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Training Training = TrainingBLO.FindBaseEntityByID((long)id);
            if (Training == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Training.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_TrainingDetailsView Default_TrainingDetailsView = new Default_TrainingDetailsViewBLM(this._UnitOfWork)
							.ConverTo_Default_TrainingDetailsView(Training);


			 return View(Default_TrainingDetailsView);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			Training Training = TrainingBLO.FindBaseEntityByID((long)id);
			if (Training == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Training.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                TrainingBLO.Delete(Training);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, Training.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed, msg_Training.SingularName, Training), NotificationType.success);
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
                ImportReport importReport = TrainingBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/Trainings/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
                DataTable dataTable = TrainingBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_Training.PluralName + ".xlsx");
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
                TrainingBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class TrainingsController : BaseTrainingsController{};
}

