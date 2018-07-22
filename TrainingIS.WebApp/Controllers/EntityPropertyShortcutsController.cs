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
using TrainingIS.Entities.Resources.EntityPropertyShortcutResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions;

namespace TrainingIS.WebApp.Controllers
{
    // Generated by Manager v 0.1.5
    public class BaseEntityPropertyShortcutsController : BaseController
    {
        protected EntityPropertyShortcutBLO entityPropertyShortcutBLO = null;

		public BaseEntityPropertyShortcutsController()
        {
            this.msgHelper = new MsgViews(typeof(EntityPropertyShortcut));
			this.entityPropertyShortcutBLO = new EntityPropertyShortcutBLO(this._UnitOfWork);
        }

        public virtual ActionResult Index()
        {
		   msgHelper.Index(msg);
           return View(entityPropertyShortcutBLO.FindAll());
        }

        public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EntityPropertyShortcut entityPropertyShortcut = entityPropertyShortcutBLO.FindBaseEntityByID((long) id);
            if (entityPropertyShortcut == null)
            {
                return HttpNotFound();
            }
            return View(entityPropertyShortcut);
        }

        public virtual ActionResult Create()
        {
		   msgHelper.Create(msg);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "Id,EntityName,PropertyName,PropertyShortcutName,Description,CreateDate,UpdateDate")] EntityPropertyShortcut entityPropertyShortcut)
        {
			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				try
                {
                    entityPropertyShortcutBLO.Save(entityPropertyShortcut);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msg_EntityPropertyShortcut.SingularName, entityPropertyShortcut), NotificationType.success);
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
            return View(entityPropertyShortcut);
        }

        public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EntityPropertyShortcut entityPropertyShortcut = entityPropertyShortcutBLO.FindBaseEntityByID((long)id);
            if (entityPropertyShortcut == null)
            {
                return HttpNotFound();
            }
            return View(entityPropertyShortcut);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "Id,EntityName,PropertyName,PropertyShortcutName,Description,CreateDate,UpdateDate")] EntityPropertyShortcut entityPropertyShortcut)
        {
			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
                EntityPropertyShortcut old_entityPropertyShortcut = entityPropertyShortcutBLO.FindBaseEntityByID(entityPropertyShortcut.Id);
                UpdateModel(old_entityPropertyShortcut);

				try
                {
                    entityPropertyShortcutBLO.Save(old_entityPropertyShortcut);
					Alert(string.Format(msgManager.The_entity_has_been_changed, msg_EntityPropertyShortcut.SingularName, entityPropertyShortcut), NotificationType.success);
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
            return View(entityPropertyShortcut);
        }

        public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EntityPropertyShortcut entityPropertyShortcut = entityPropertyShortcutBLO.FindBaseEntityByID((long)id);
            if (entityPropertyShortcut == null)
            {
                return HttpNotFound();
            }
            return View(entityPropertyShortcut);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			EntityPropertyShortcut entityPropertyShortcut = entityPropertyShortcutBLO.FindBaseEntityByID((long)id);
            entityPropertyShortcutBLO.Delete(entityPropertyShortcut);

			 

			Alert(string.Format(msgManager.The_entity_has_been_removed, msg_EntityPropertyShortcut.SingularName, entityPropertyShortcut), NotificationType.success);
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
                string msg =   entityPropertyShortcutBLO.Import(firstTable);
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
                DataTable dataTable = entityPropertyShortcutBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_EntityPropertyShortcut.PluralName + ".xlsx");
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                entityPropertyShortcutBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class EntityPropertyShortcutsController : BaseEntityPropertyShortcutsController{};
}
