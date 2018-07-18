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
using TrainingIS.Entities.Resources.TrainingTypeResources;
using TrainingIS.WebApp.Helpers.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions;

namespace TrainingIS.WebApp.Controllers
{
    // Generated by Manager v 0.1.3
    public class BaseTrainingTypesController : BaseController
    {
        protected TrainingTypeBLO trainingTypeBLO = null;

		public BaseTrainingTypesController()
        {
            this.msgHelper = new MsgHelper(typeof(TrainingType));
			this.trainingTypeBLO = new TrainingTypeBLO(this._UnitOfWork);
        }

        public virtual ActionResult Index()
        {
		   msgHelper.Index(msg);
           return View(trainingTypeBLO.FindAll());
        }

        public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TrainingType trainingType = trainingTypeBLO.FindBaseEntityByID((long) id);
            if (trainingType == null)
            {
                return HttpNotFound();
            }
            return View(trainingType);
        }

        public virtual ActionResult Create()
        {
		   msgHelper.Create(msg);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "Id,Code,Name,Description,CreateDate,UpdateDate")] TrainingType trainingType)
        {
			
            if (ModelState.IsValid)
            {

				try
                {
                    trainingTypeBLO.Save(trainingType);
                }
                catch (GAppDataBaseException ex)
                {
                    msgHelper.Create(msg);
                    Alert(ex.Message, NotificationType.error);
                    return View(trainingType);
                }
                
				Alert(string.Format(msgManager.The_Entity_was_well_created, msg_TrainingType.SingularName, trainingType), NotificationType.success);
                return RedirectToAction("Index");
            }
			msgHelper.Create(msg);
		    Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            return View(trainingType);
        }

        public virtual ActionResult Edit(long? id)
        {
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TrainingType trainingType = trainingTypeBLO.FindBaseEntityByID((long)id);
            if (trainingType == null)
            {
                return HttpNotFound();
            }
            return View(trainingType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "Id,Code,Name,Description,CreateDate,UpdateDate")] TrainingType trainingType)
        {
            if (ModelState.IsValid)
            {
                TrainingType old_trainingType = trainingTypeBLO.FindBaseEntityByID(trainingType.Id);
                UpdateModel(old_trainingType);

				try
                {
                    trainingTypeBLO.Save(old_trainingType);
                }
                catch (GAppDataBaseException ex)
                {
                    msgHelper.Edit(msg); ;
                    Alert(ex.Message, NotificationType.error);
                    return View(trainingType);
                }

				Alert(string.Format(msgManager.The_entity_has_been_changed, msg_TrainingType.SingularName, trainingType), NotificationType.success);
                return RedirectToAction("Index");
            }

            msgHelper.Edit(msg);
			Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            return View(trainingType);
        }

        public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TrainingType trainingType = trainingTypeBLO.FindBaseEntityByID((long)id);
            if (trainingType == null)
            {
                return HttpNotFound();
            }
            return View(trainingType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			TrainingType trainingType = trainingTypeBLO.FindBaseEntityByID((long)id);
            trainingTypeBLO.Delete(trainingType);

			 

			Alert(string.Format(msgManager.The_entity_has_been_removed, msg_TrainingType.SingularName, trainingType), NotificationType.success);
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
                string msg =   trainingTypeBLO.Import(firstTable);
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
                DataTable dataTable = trainingTypeBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_TrainingType.PluralName + ".xlsx");
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                trainingTypeBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class TrainingTypesController : BaseTrainingTypesController{};
}
