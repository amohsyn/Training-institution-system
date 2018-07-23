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



namespace TrainingIS.WebApp.Controllers
{
    // Generated by Manager v 0.1.5
    public class BaseSeancePlanningsController : BaseController
    {
        protected SeancePlanningBLO SeancePlanningBLO = null;

		public BaseSeancePlanningsController()
        {
            this.msgHelper = new MsgViews(typeof(SeancePlanning));
			this.SeancePlanningBLO = new SeancePlanningBLO(this._UnitOfWork);
        }

        public virtual ActionResult Index()
        {
		   msgHelper.Index(msg);
           return View(SeancePlanningBLO.FindAll());
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
                return HttpNotFound();
            }
            return View(SeancePlanning);
        }

        public virtual ActionResult Create()
        {
			msgHelper.Create(msg);
            ViewBag.SeanceDayId = new SelectList(new SeanceDayBLO(this._UnitOfWork).FindAll(), "Id", "Code");
            ViewBag.SeanceNumberId = new SelectList(new SeanceNumberBLO(this._UnitOfWork).FindAll(), "Id", "Code");
            ViewBag.TrainingId = new SelectList(new TrainingBLO(this._UnitOfWork).FindAll(), "Id", "Code");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "Training,TrainingId,SeanceDay,SeanceDayId,SeanceNumber,SeanceNumberId,Description,Id")] SeancePlanning SeancePlanning)
        {
			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				try
                {
                    SeancePlanningBLO.Save(SeancePlanning);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msg_SeancePlanning.SingularName, SeancePlanning), NotificationType.success);
					return RedirectToAction("Index");
                }
                catch (GAppDataBaseException ex)
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
            return View(SeancePlanning);
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
                return HttpNotFound();
            }
			ViewBag.SeanceDayId = new SelectList(new SeanceDayBLO(this._UnitOfWork).FindAll(), "Id", "Code", SeancePlanning.SeanceDayId);
			ViewBag.SeanceNumberId = new SelectList(new SeanceNumberBLO(this._UnitOfWork).FindAll(), "Id", "Code", SeancePlanning.SeanceNumberId);
			ViewBag.TrainingId = new SelectList(new TrainingBLO(this._UnitOfWork).FindAll(), "Id", "Code", SeancePlanning.TrainingId);
            return View(SeancePlanning);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "Training,TrainingId,SeanceDay,SeanceDayId,SeanceNumber,SeanceNumberId,Description,Id")] SeancePlanning SeancePlanning)
        {
			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
                SeancePlanning old_SeancePlanning = SeancePlanningBLO.FindBaseEntityByID(SeancePlanning.Id);
                UpdateModel(old_SeancePlanning);

				try
                {
                    SeancePlanningBLO.Save(old_SeancePlanning);
					Alert(string.Format(msgManager.The_entity_has_been_changed, msg_SeancePlanning.SingularName, SeancePlanning), NotificationType.success);
					return RedirectToAction("Index");
                }
                catch (GAppDataBaseException ex)
                {
					dataBaseException = true;
                    Alert(ex.Message, NotificationType.error);
                }
            }
			if (!dataBaseException)
            {
                Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            }

			ViewBag.SeanceDayId = new SelectList(new SeanceDayBLO(this._UnitOfWork).FindAll(), "Id", "Code", SeancePlanning.SeanceDayId);
			ViewBag.SeanceNumberId = new SelectList(new SeanceNumberBLO(this._UnitOfWork).FindAll(), "Id", "Code", SeancePlanning.SeanceNumberId);
			ViewBag.TrainingId = new SelectList(new TrainingBLO(this._UnitOfWork).FindAll(), "Id", "Code", SeancePlanning.TrainingId);
            msgHelper.Edit(msg);
            return View(SeancePlanning);
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
                return HttpNotFound();
            }
            return View(SeancePlanning);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			SeancePlanning SeancePlanning = SeancePlanningBLO.FindBaseEntityByID((long)id);
            SeancePlanningBLO.Delete(SeancePlanning);
			Alert(string.Format(msgManager.The_entity_has_been_removed, msg_SeancePlanning.SingularName, SeancePlanning), NotificationType.success);
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

