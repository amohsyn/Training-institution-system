﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainingIS.Entities;
using TrainingIS.BLL;
using GApp.DAL.ReadExcel;
using ClosedXML.Excel;
using System.IO;
using static TrainingIS.WebApp.Enums.Enums;
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.WebApp.Helpers.msgs;

namespace TrainingIS.WebApp.Controllers
{

    public class TraineesController : BaseController
    {
        private TraineeBLO traineeBLO = new TraineeBLO();

        public ActionResult Index()
        {
           
            return View(traineeBLO.FindAll());
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Trainee trainee = traineeBLO.FindBaseEntityByID((long) id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        public ActionResult Create()
        {
			 
            ViewBag.GroupId = new SelectList(new GroupBLO().FindAll(), "Id", "Code");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Sex,CIN,CNE,Cellphone,Email,Address,FaceBook,WebSite,GroupId")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                traineeBLO.Save(trainee);
                Alert(string.Format(msgManager.The_Entity_was_well_created, msg_Trainee.SingularName, trainee), NotificationType.success);
                return RedirectToAction("Index");
            }
            Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            ViewBag.GroupId = new SelectList(new GroupBLO().FindAll(), "Id", "Code", trainee.GroupId);
            return View(trainee);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Trainee trainee = traineeBLO.FindBaseEntityByID((long)id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(new GroupBLO().FindAll(), "Id", "Code", trainee.GroupId);
            return View(trainee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Sex,CIN,CNE,Cellphone,Email,Address,FaceBook,WebSite,GroupId")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                Trainee old_trainee = traineeBLO.FindBaseEntityByID(trainee.Id);
                UpdateModel(old_trainee);
                traineeBLO.Save(old_trainee);
                Alert(string.Format(msgManager.The_entity_has_been_changed, msg_Trainee.SingularName, trainee), NotificationType.success);
                return RedirectToAction("Index");
            }
            Alert(msgManager.The_information_you_have_entered_is_not_valid, NotificationType.warning);
            ViewBag.GroupId = new SelectList(new GroupBLO().FindAll(), "Id", "Code", trainee.GroupId);
            return View(trainee);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Trainee trainee = traineeBLO.FindBaseEntityByID((long)id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
			Trainee trainee = traineeBLO.FindBaseEntityByID((long)id);
            traineeBLO.Delete(trainee);
            Alert(string.Format(msgManager.The_entity_has_been_removed, msg_Trainee.SingularName, trainee), NotificationType.success);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                traineeBLO.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Import()
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
            string msg =   traineeBLO.Import(firstTable);
            Message(msg, NotificationType.info);

            return RedirectToAction("Index");
        }


        public FileResult Export()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dataTable = traineeBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_Trainee.PluralName + ".xlsx");
                }
            }
        }
    }
}
