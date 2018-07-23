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
using TrainingIS.Entities.Resources.ClassroomResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions;

namespace TrainingIS.WebApp.Controllers
{
    // Generated by Manager v 0.1.4
    public class BaseClassroomsController : BaseController
    {
        protected ClassroomBLO classroomBLO = null;

		public BaseClassroomsController()
        {
            this.msgHelper = new MsgViews(typeof(Classroom));
			this.classroomBLO = new ClassroomBLO(this._UnitOfWork);
        }

        public virtual ActionResult Index()
        {
		   msgHelper.Index(msg);
           return View(classroomBLO.FindAll());
        }

        public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Classroom classroom = classroomBLO.FindBaseEntityByID((long) id);
            if (classroom == null)
            {
                return HttpNotFound();
            }
            return View(classroom);
        }

        public virtual ActionResult Create()
        {
		   msgHelper.Create(msg);
			
            ViewBag.ClassroomCategoryId = new SelectList(new ClassroomCategoryBLO(this._UnitOfWork).FindAll(), "Id", "Code");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "Id,Code,Name,ClassroomCategoryId,Description,CreateDate,UpdateDate")] Classroom classroom)
        {
			
            if (ModelState.IsValid)
            {

				try
                {
                    classroomBLO.Save(classroom);
                }
                catch (GAppDataBaseException ex)
                {
                    msgHelper.Create(msg);
                    Alert(ex.Message, NotificationType.error);
                    return View(classroom);
                }
                
				Alert(string.Format(msgManager.The_Entity_was_well_created, msg_Classroom.SingularName, classroom), NotificationType.success);
                return RedirectToAction("Index");
            }
			msgHelper.Create(msg);
            ViewBag.ClassroomCategoryId = new SelectList(new ClassroomCategoryBLO(this._UnitOfWork).FindAll(), "Id", "Code", classroom.ClassroomCategoryId);
		    Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            return View(classroom);
        }

        public virtual ActionResult Edit(long? id)
        {
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Classroom classroom = classroomBLO.FindBaseEntityByID((long)id);
            if (classroom == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassroomCategoryId = new SelectList(new ClassroomCategoryBLO(this._UnitOfWork).FindAll(), "Id", "Code", classroom.ClassroomCategoryId);
            return View(classroom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "Id,Code,Name,ClassroomCategoryId,Description,CreateDate,UpdateDate")] Classroom classroom)
        {
            if (ModelState.IsValid)
            {
                Classroom old_classroom = classroomBLO.FindBaseEntityByID(classroom.Id);
                UpdateModel(old_classroom);

				try
                {
                    classroomBLO.Save(old_classroom);
                }
                catch (GAppDataBaseException ex)
                {
                    msgHelper.Edit(msg); ;
                    Alert(ex.Message, NotificationType.error);
                    return View(classroom);
                }

				Alert(string.Format(msgManager.The_entity_has_been_changed, msg_Classroom.SingularName, classroom), NotificationType.success);
                return RedirectToAction("Index");
            }
            ViewBag.ClassroomCategoryId = new SelectList(new ClassroomCategoryBLO(this._UnitOfWork).FindAll(), "Id", "Code", classroom.ClassroomCategoryId);

            msgHelper.Edit(msg);
			Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            return View(classroom);
        }

        public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Classroom classroom = classroomBLO.FindBaseEntityByID((long)id);
            if (classroom == null)
            {
                return HttpNotFound();
            }
            return View(classroom);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			Classroom classroom = classroomBLO.FindBaseEntityByID((long)id);
            classroomBLO.Delete(classroom);

			 

			Alert(string.Format(msgManager.The_entity_has_been_removed, msg_Classroom.SingularName, classroom), NotificationType.success);
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
                string msg =   classroomBLO.Import(firstTable).ToString();
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
                DataTable dataTable = classroomBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_Classroom.PluralName + ".xlsx");
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                classroomBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class ClassroomsController : BaseClassroomsController{};
}
