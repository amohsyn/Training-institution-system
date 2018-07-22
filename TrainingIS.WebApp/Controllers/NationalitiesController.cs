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
using TrainingIS.Entities.Resources.NationalityResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions;

namespace TrainingIS.WebApp.Controllers
{
    // Generated by Manager v 0.1.4
    public class BaseNationalitiesController : BaseController
    {
        protected NationalityBLO nationalityBLO = null;

		public BaseNationalitiesController()
        {
            this.msgHelper = new MsgViews(typeof(Nationality));
			this.nationalityBLO = new NationalityBLO(this._UnitOfWork);
        }

        public virtual ActionResult Index()
        {
		   msgHelper.Index(msg);
           return View(nationalityBLO.FindAll());
        }

        public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Nationality nationality = nationalityBLO.FindBaseEntityByID((long) id);
            if (nationality == null)
            {
                return HttpNotFound();
            }
            return View(nationality);
        }

        public virtual ActionResult Create()
        {
		   msgHelper.Create(msg);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "Id,Code,Name,Description,CreateDate,UpdateDate")] Nationality nationality)
        {
			
            if (ModelState.IsValid)
            {

				try
                {
                    nationalityBLO.Save(nationality);
                }
                catch (GAppDataBaseException ex)
                {
                    msgHelper.Create(msg);
                    Alert(ex.Message, NotificationType.error);
                    return View(nationality);
                }
                
				Alert(string.Format(msgManager.The_Entity_was_well_created, msg_Nationality.SingularName, nationality), NotificationType.success);
                return RedirectToAction("Index");
            }
			msgHelper.Create(msg);
		    Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            return View(nationality);
        }

        public virtual ActionResult Edit(long? id)
        {
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Nationality nationality = nationalityBLO.FindBaseEntityByID((long)id);
            if (nationality == null)
            {
                return HttpNotFound();
            }
            return View(nationality);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "Id,Code,Name,Description,CreateDate,UpdateDate")] Nationality nationality)
        {
            if (ModelState.IsValid)
            {
                Nationality old_nationality = nationalityBLO.FindBaseEntityByID(nationality.Id);
                UpdateModel(old_nationality);

				try
                {
                    nationalityBLO.Save(old_nationality);
                }
                catch (GAppDataBaseException ex)
                {
                    msgHelper.Edit(msg); ;
                    Alert(ex.Message, NotificationType.error);
                    return View(nationality);
                }

				Alert(string.Format(msgManager.The_entity_has_been_changed, msg_Nationality.SingularName, nationality), NotificationType.success);
                return RedirectToAction("Index");
            }

            msgHelper.Edit(msg);
			Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            return View(nationality);
        }

        public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Nationality nationality = nationalityBLO.FindBaseEntityByID((long)id);
            if (nationality == null)
            {
                return HttpNotFound();
            }
            return View(nationality);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			Nationality nationality = nationalityBLO.FindBaseEntityByID((long)id);
            nationalityBLO.Delete(nationality);

			 

			Alert(string.Format(msgManager.The_entity_has_been_removed, msg_Nationality.SingularName, nationality), NotificationType.success);
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
                string msg =   nationalityBLO.Import(firstTable);
                Message(msg, NotificationType.info);
               
            }
            catch (ImportException e)
            {
                Message(e.Message, NotificationType.info);
            }
			 return RedirectToAction("Index");
        }


        public virtual FileResult Export()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dataTable = nationalityBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_Nationality.PluralName + ".xlsx");
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                nationalityBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class NationalitiesController : BaseNationalitiesController{};
}
