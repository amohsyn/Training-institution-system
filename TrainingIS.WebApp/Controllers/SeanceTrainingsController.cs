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
using GApp.DAL.ReadExcel;
using ClosedXML.Excel;
using System.IO;
using static TrainingIS.WebApp.Enums.Enums;
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.WebApp.Helpers.msgs;
using TrainingIS.WebApp.Helpers;

namespace TrainingIS.WebApp.Controllers
{
    // Generated by Manager v 0.0.5
    public class BaseSeanceTrainingsController : BaseController
    {
        protected SeanceTrainingBLO seanceTrainingBLO = null;

		public BaseSeanceTrainingsController()
        {
            this.msgHelper = new MsgHelper(typeof(SeanceTraining));
            this.seanceTrainingBLO = new SeanceTrainingBLO(this._UnitOfWork);
        }

        public virtual ActionResult Index()
        {
		   msgHelper.Index(msg);
           return View(seanceTrainingBLO.FindAll());
        }

        public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeanceTraining seanceTraining = seanceTrainingBLO.FindBaseEntityByID((long) id);
            if (seanceTraining == null)
            {
                return HttpNotFound();
            }
            return View(seanceTraining);
        }

        public virtual ActionResult Create()
        {
		   msgHelper.Create(msg);
			
            ViewBag.SeancePlanningId = new SelectList(new SeancePlanningBLO(this._UnitOfWork).FindAll(), "Id", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "Id,SeanceDate,SeancePlanningId,CreateDate,UpdateDate")] SeanceTraining seanceTraining)
        {
			
            if (ModelState.IsValid)
            {
                seanceTrainingBLO.Save(seanceTraining);
				Alert(string.Format(msgManager.The_Entity_was_well_created, msg_SeanceTraining.SingularName, seanceTraining), NotificationType.success);
                return RedirectToAction("Index");
            }
			msgHelper.Create(msg);
            ViewBag.SeancePlanningId = new SelectList(new SeancePlanningBLO(this._UnitOfWork).FindAll(), "Id", "Description", seanceTraining.SeancePlanningId);
		    Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            return View(seanceTraining);
        }

        public virtual ActionResult Edit(long? id)
        {
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeanceTraining seanceTraining = seanceTrainingBLO.FindBaseEntityByID((long)id);
            if (seanceTraining == null)
            {
                return HttpNotFound();
            }
            ViewBag.SeancePlanningId = new SelectList(new SeancePlanningBLO(this._UnitOfWork).FindAll(), "Id", "Description", seanceTraining.SeancePlanningId);
            return View(seanceTraining);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "Id,SeanceDate,SeancePlanningId,CreateDate,UpdateDate")] SeanceTraining seanceTraining)
        {
            if (ModelState.IsValid)
            {
                SeanceTraining old_seanceTraining = seanceTrainingBLO.FindBaseEntityByID(seanceTraining.Id);
                UpdateModel(old_seanceTraining);
                seanceTrainingBLO.Save(old_seanceTraining);
				Alert(string.Format(msgManager.The_entity_has_been_changed, msg_SeanceTraining.SingularName, seanceTraining), NotificationType.success);
                return RedirectToAction("Index");
            }
            ViewBag.SeancePlanningId = new SelectList(new SeancePlanningBLO(this._UnitOfWork).FindAll(), "Id", "Description", seanceTraining.SeancePlanningId);

            msgHelper.Edit(msg);
			Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            return View(seanceTraining);
        }

        public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeanceTraining seanceTraining = seanceTrainingBLO.FindBaseEntityByID((long)id);
            if (seanceTraining == null)
            {
                return HttpNotFound();
            }
            return View(seanceTraining);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			SeanceTraining seanceTraining = seanceTrainingBLO.FindBaseEntityByID((long)id);
            seanceTrainingBLO.Delete(seanceTraining);

			 

			Alert(string.Format(msgManager.The_entity_has_been_removed, msg_SeanceTraining.SingularName, seanceTraining), NotificationType.success);
            return RedirectToAction("Index");
        }

		public virtual ActionResult Import()
        {
            //Save excel file to server
            HttpPostedFileBase parametersTemplate = Request.Files["import_objects"];

            // [Bug] if multiple user import the same file in the same moments
            string path = Server.MapPath("~/Content/Files/Upload" + parametersTemplate.FileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                parametersTemplate.SaveAs(path);
            }
            parametersTemplate.SaveAs(path);

            //Save new parameters to database
            var excelData = new ExcelData(path); // link to other project
            DataTable firstTable = excelData.getFirstTable();

			try
            {
                string msg =   seanceTrainingBLO.Import(firstTable);
                Message(msg, NotificationType.info);
               
            }
            catch (ImportLineException e)
            {
                Message(e.Message, NotificationType.info);
            }
			 return RedirectToAction("Index");
        }


        public virtual FileResult Export()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dataTable = seanceTrainingBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_SeanceTraining.PluralName + ".xlsx");
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                seanceTrainingBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

  
    public partial class SeanceTrainingsController : BaseSeanceTrainingsController{};
}
