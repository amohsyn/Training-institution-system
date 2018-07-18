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
using TrainingIS.Entities.Resources.ClassroomCategoryResources;
using TrainingIS.WebApp.Helpers.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions;

namespace TrainingIS.WebApp.Controllers
{
    // Generated by Manager v 0.1.4
    public class BaseClassroomCategoriesController : BaseController
    {
        protected ClassroomCategoryBLO classroomCategoryBLO = null;

		public BaseClassroomCategoriesController()
        {
            this.msgHelper = new MsgHelper(typeof(ClassroomCategory));
			this.classroomCategoryBLO = new ClassroomCategoryBLO(this._UnitOfWork);
        }

        public virtual ActionResult Index()
        {
		   msgHelper.Index(msg);
           return View(classroomCategoryBLO.FindAll());
        }

        public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClassroomCategory classroomCategory = classroomCategoryBLO.FindBaseEntityByID((long) id);
            if (classroomCategory == null)
            {
                return HttpNotFound();
            }
            return View(classroomCategory);
        }

        public virtual ActionResult Create()
        {
		   msgHelper.Create(msg);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "Id,Code,Name,Description,CreateDate,UpdateDate")] ClassroomCategory classroomCategory)
        {
			
            if (ModelState.IsValid)
            {

				try
                {
                    classroomCategoryBLO.Save(classroomCategory);
                }
                catch (GAppDataBaseException ex)
                {
                    msgHelper.Create(msg);
                    Alert(ex.Message, NotificationType.error);
                    return View(classroomCategory);
                }
                
				Alert(string.Format(msgManager.The_Entity_was_well_created, msg_ClassroomCategory.SingularName, classroomCategory), NotificationType.success);
                return RedirectToAction("Index");
            }
			msgHelper.Create(msg);
		    Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            return View(classroomCategory);
        }

        public virtual ActionResult Edit(long? id)
        {
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClassroomCategory classroomCategory = classroomCategoryBLO.FindBaseEntityByID((long)id);
            if (classroomCategory == null)
            {
                return HttpNotFound();
            }
            return View(classroomCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "Id,Code,Name,Description,CreateDate,UpdateDate")] ClassroomCategory classroomCategory)
        {
            if (ModelState.IsValid)
            {
                ClassroomCategory old_classroomCategory = classroomCategoryBLO.FindBaseEntityByID(classroomCategory.Id);
                UpdateModel(old_classroomCategory);

				try
                {
                    classroomCategoryBLO.Save(old_classroomCategory);
                }
                catch (GAppDataBaseException ex)
                {
                    msgHelper.Edit(msg); ;
                    Alert(ex.Message, NotificationType.error);
                    return View(classroomCategory);
                }

				Alert(string.Format(msgManager.The_entity_has_been_changed, msg_ClassroomCategory.SingularName, classroomCategory), NotificationType.success);
                return RedirectToAction("Index");
            }

            msgHelper.Edit(msg);
			Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            return View(classroomCategory);
        }

        public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClassroomCategory classroomCategory = classroomCategoryBLO.FindBaseEntityByID((long)id);
            if (classroomCategory == null)
            {
                return HttpNotFound();
            }
            return View(classroomCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			ClassroomCategory classroomCategory = classroomCategoryBLO.FindBaseEntityByID((long)id);
            classroomCategoryBLO.Delete(classroomCategory);

			 

			Alert(string.Format(msgManager.The_entity_has_been_removed, msg_ClassroomCategory.SingularName, classroomCategory), NotificationType.success);
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
                string msg =   classroomCategoryBLO.Import(firstTable);
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
                DataTable dataTable = classroomCategoryBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_ClassroomCategory.PluralName + ".xlsx");
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                classroomCategoryBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class ClassroomCategoriesController : BaseClassroomCategoriesController{};
}
