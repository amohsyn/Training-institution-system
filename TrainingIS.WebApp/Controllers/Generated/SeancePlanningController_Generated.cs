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
using TrainingIS.Entities.Resources.SeancePlanningResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions; 
using GApp.Entities;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS.WebApp.Controllers
{  
    // Generated by Manager v 0.2.0 
    public class BaseSeancePlanningsController : BaseController
    {
        protected SeancePlanningBLO SeancePlanningBLO = null;

		public BaseSeancePlanningsController()
        {
            this.msgHelper = new MessagesService(typeof(SeancePlanning));
			this.SeancePlanningBLO = new SeancePlanningBLO(this._UnitOfWork);
        }

	    public virtual ActionResult Index()
        {
		    msgHelper.Index(msg);
            List<Default_SeancePlanningDetailsView> listDefault_SeancePlanningDetailsView = new List<Default_SeancePlanningDetailsView>();
			foreach (var item in SeancePlanningBLO.FindAll()){
                Default_SeancePlanningDetailsView Default_SeancePlanningDetailsView = new Default_SeancePlanningDetailsViewBLM(this._UnitOfWork)
                    .ConverTo_Default_SeancePlanningDetailsView(item);
                listDefault_SeancePlanningDetailsView.Add(Default_SeancePlanningDetailsView);
            }
			return View(listDefault_SeancePlanningDetailsView);
		}

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "TrainingId,SeanceDayId,SeanceNumberId,Description")] Default_SeancePlanningFormView Default_SeancePlanningFormView)
        {
			SeancePlanning SeancePlanning = null ;
			SeancePlanning = new Default_SeancePlanningFormViewBLM(this._UnitOfWork)
										.ConverTo_SeancePlanning(Default_SeancePlanningFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    SeancePlanningBLO.Save(SeancePlanning);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle(), msg_SeancePlanning.SingularName, SeancePlanning), NotificationType.success);
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
			ViewBag.SeanceDayId = new SelectList(new SeanceDayBLO(this._UnitOfWork).FindAll(), "Id", "Code", SeancePlanning.SeanceDayId);
			ViewBag.SeanceNumberId = new SelectList(new SeanceNumberBLO(this._UnitOfWork).FindAll(), "Id", "Code", SeancePlanning.SeanceNumberId);
			ViewBag.TrainingId = new SelectList(new TrainingBLO(this._UnitOfWork).FindAll(), "Id", "Code", SeancePlanning.TrainingId);
			return View(Default_SeancePlanningFormView);
        }


		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			ViewBag.SeanceDayId = new SelectList(new SeanceDayBLO(this._UnitOfWork).FindAll(), "Id", "Code");
			ViewBag.SeanceNumberId = new SelectList(new SeanceNumberBLO(this._UnitOfWork).FindAll(), "Id", "Code");
			ViewBag.TrainingId = new SelectList(new TrainingBLO(this._UnitOfWork).FindAll(), "Id", "Code");
            SeancePlanning seanceplanning = new SeancePlanning();
            Default_SeancePlanningFormView default_seanceplanningformview = new Default_SeancePlanningFormViewBLM(this._UnitOfWork)
                                        .ConverTo_Default_SeancePlanningFormView(seanceplanning);
            return View(default_seanceplanningformview);
        } 
		 
       


		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeancePlanning SeancePlanning = SeancePlanningBLO.FindBaseEntityByID((long)id);
            if (SeancePlanning == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeancePlanning.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			Default_SeancePlanningFormView Default_SeancePlanningFormView = new Default_SeancePlanningFormViewBLM(this._UnitOfWork)
                                                                .ConverTo_Default_SeancePlanningFormView(SeancePlanning) ;

			ViewBag.SeanceDayId = new SelectList(new SeanceDayBLO(this._UnitOfWork).FindAll(), "Id", "Code", Default_SeancePlanningFormView.SeanceDayId);
			ViewBag.SeanceNumberId = new SelectList(new SeanceNumberBLO(this._UnitOfWork).FindAll(), "Id", "Code", Default_SeancePlanningFormView.SeanceNumberId);
			ViewBag.TrainingId = new SelectList(new TrainingBLO(this._UnitOfWork).FindAll(), "Id", "Code", Default_SeancePlanningFormView.TrainingId);
 
			return View(Default_SeancePlanningFormView);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "TrainingId,SeanceDayId,SeanceNumberId,Description,Id")] Default_SeancePlanningFormView Default_SeancePlanningFormView)	
        {
			SeancePlanning SeancePlanning = new Default_SeancePlanningFormViewBLM(this._UnitOfWork)
                .ConverTo_SeancePlanning( Default_SeancePlanningFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    SeancePlanningBLO.Save(SeancePlanning);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle(), msg_SeancePlanning.SingularName, SeancePlanning), NotificationType.success);
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

			ViewBag.SeanceDayId = new SelectList(new SeanceDayBLO(this._UnitOfWork).FindAll(), "Id", "Code", Default_SeancePlanningFormView.SeanceDayId);
			ViewBag.SeanceNumberId = new SelectList(new SeanceNumberBLO(this._UnitOfWork).FindAll(), "Id", "Code", Default_SeancePlanningFormView.SeanceNumberId);
			ViewBag.TrainingId = new SelectList(new TrainingBLO(this._UnitOfWork).FindAll(), "Id", "Code", Default_SeancePlanningFormView.TrainingId);
			return View(Default_SeancePlanningFormView);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeancePlanning SeancePlanning = SeancePlanningBLO.FindBaseEntityByID((long) id);
            if (SeancePlanning == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeancePlanning.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			Default_SeancePlanningDetailsView Default_SeancePlanningDetailsView = new Default_SeancePlanningDetailsView();
		    Default_SeancePlanningDetailsView = new Default_SeancePlanningDetailsViewBLM(this._UnitOfWork)
                .ConverTo_Default_SeancePlanningDetailsView(SeancePlanning);


			return View(Default_SeancePlanningDetailsView);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeancePlanning SeancePlanning = SeancePlanningBLO.FindBaseEntityByID((long)id);
            if (SeancePlanning == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeancePlanning.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_SeancePlanningDetailsView Default_SeancePlanningDetailsView = new Default_SeancePlanningDetailsViewBLM(this._UnitOfWork)
							.ConverTo_Default_SeancePlanningDetailsView(SeancePlanning);


			 return View(Default_SeancePlanningDetailsView);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			SeancePlanning SeancePlanning = SeancePlanningBLO.FindBaseEntityByID((long)id);
			if (SeancePlanning == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeancePlanning.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                SeancePlanningBLO.Delete(SeancePlanning);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, SeancePlanning.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle(), msg_SeancePlanning.SingularName, SeancePlanning), NotificationType.success);
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
                ImportReport importReport = SeancePlanningBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/SeancePlannings/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
                DataTable dataTable = SeancePlanningBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_SeancePlanning.PluralName + ".xlsx");
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
                SeancePlanningBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class SeancePlanningsController : BaseSeancePlanningsController{};
}

