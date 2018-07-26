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
using TrainingIS.Entities.Resources.AbsenceResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions;
using TrainingIS.WebApp.ViewModels;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
 
namespace TrainingIS.WebApp.Controllers
{
    // Generated by Manager v 0.2.0
    public class BaseAbsencesController : BaseController
    {
        protected AbsenceBLO AbsenceBLO = null;

		public BaseAbsencesController()
        {
            this.msgHelper = new MsgViews(typeof(Absence));
			this.AbsenceBLO = new AbsenceBLO(this._UnitOfWork);
        }
		 
		public virtual ActionResult Index()
        {
		   msgHelper.Index(msg);
		   return View(AbsenceBLO.FindAll());
		}
		        public virtual ActionResult Create()
        {
			msgHelper.Create(msg);

            ViewBag.SeanceTrainingId = new SelectList(new SeanceTrainingBLO(this._UnitOfWork).FindAll(), "Id", "Code");
            ViewBag.TraineeId = new SelectList(new TraineeBLO(this._UnitOfWork).FindAll(), "Id", "Code");

            return View();
        } 
				[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "Trainee,TraineeId,isHaveAuthorization,SeanceTraining,SeanceTrainingId,FormerComment,TraineeComment,SupervisorComment,Id")] Absence Absence)
        {
			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				try
                {
                    AbsenceBLO.Save(Absence);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msg_Absence.SingularName, Absence), NotificationType.success);
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
            ViewBag.SeanceTrainingId = new SelectList(new SeanceTrainingBLO(this._UnitOfWork).FindAll(), "Id", "Code", Absence.SeanceTrainingId);
            ViewBag.TraineeId = new SelectList(new TraineeBLO(this._UnitOfWork).FindAll(), "Id", "Code", Absence.TraineeId);
            return View(Absence);
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
                return HttpNotFound();
            }
			 return View(Absence);

			
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
                return HttpNotFound();
            }			 
            ViewBag.SeanceTrainingId = new SelectList(new SeanceTrainingBLO(this._UnitOfWork).FindAll(), "Id", "Code", Absence.SeanceTrainingId);
            ViewBag.TraineeId = new SelectList(new TraineeBLO(this._UnitOfWork).FindAll(), "Id", "Code", Absence.TraineeId);
			return View(Absence);
        }
		        [HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "Trainee,TraineeId,isHaveAuthorization,SeanceTraining,SeanceTrainingId,FormerComment,TraineeComment,SupervisorComment,Id")] Absence Absence)	
        {
			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
                Absence old_Absence = AbsenceBLO.FindBaseEntityByID(Absence.Id);
                UpdateModel(old_Absence);

				try
                {
                    AbsenceBLO.Save(old_Absence);
					Alert(string.Format(msgManager.The_entity_has_been_changed, msg_Absence.SingularName, Absence), NotificationType.success);
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
			msgHelper.Edit(msg);

            ViewBag.SeanceTrainingId = new SelectList(new SeanceTrainingBLO(this._UnitOfWork).FindAll(), "Id", "Code", Absence.SeanceTrainingId);
            ViewBag.TraineeId = new SelectList(new TraineeBLO(this._UnitOfWork).FindAll(), "Id", "Code", Absence.TraineeId);
		return View(Absence);
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
                return HttpNotFound();
            }

			 return View(Absence);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			Absence Absence = AbsenceBLO.FindBaseEntityByID((long)id);
            AbsenceBLO.Delete(Absence);
			Alert(string.Format(msgManager.The_entity_has_been_removed, msg_Absence.SingularName, Absence), NotificationType.success);
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
                ImportReport importReport = AbsenceBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
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


        public virtual FileResult Export()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dataTable = AbsenceBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_Absence.PluralName + ".xlsx");
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
                AbsenceBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class AbsencesController : BaseAbsencesController{};
}
