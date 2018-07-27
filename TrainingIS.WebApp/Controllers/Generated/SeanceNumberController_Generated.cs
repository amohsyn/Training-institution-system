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
using TrainingIS.Entities.Resources.SeanceNumberResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
 using TrainingIS.Entities.ModelsViews;
namespace TrainingIS.WebApp.Controllers
{
    // Generated by Manager v 0.2.0
    public class BaseSeanceNumbersController : BaseController
    {
        protected SeanceNumberBLO SeanceNumberBLO = null;

		public BaseSeanceNumbersController()
        {
            this.msgHelper = new MsgViews(typeof(SeanceNumber));
			this.SeanceNumberBLO = new SeanceNumberBLO(this._UnitOfWork);
        }
		 
		public virtual ActionResult Index()
        {
			msgHelper.Index(msg);
            Default_IndexSeanceNumberView Default_IndexSeanceNumberView = new Default_IndexSeanceNumberView();
            foreach (var item in SeanceNumberBLO.FindAll()) { Default_IndexSeanceNumberView.Data.Add(item); }
            return View(Default_IndexSeanceNumberView);
		}
		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);


            return View();
        } 
		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "Code,StartTime,EndTime,Description,Id")] SeanceNumber SeanceNumber)
        {
			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				try
                {
                    SeanceNumberBLO.Save(SeanceNumber);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msg_SeanceNumber.SingularName, SeanceNumber), NotificationType.success);
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
            return View(SeanceNumber);
        }
		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeanceNumber SeanceNumber = SeanceNumberBLO.FindBaseEntityByID((long) id);
            if (SeanceNumber == null)
            {
                return HttpNotFound();
            }
			 return View(SeanceNumber);
        } 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeanceNumber SeanceNumber = SeanceNumberBLO.FindBaseEntityByID((long)id);
            if (SeanceNumber == null)
            {
                return HttpNotFound();
            }			 
			return View(SeanceNumber);
        }
		        [HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "Code,StartTime,EndTime,Description,Id")] SeanceNumber SeanceNumber)	
        {
			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
                SeanceNumber old_SeanceNumber = SeanceNumberBLO.FindBaseEntityByID(SeanceNumber.Id);
                UpdateModel(old_SeanceNumber);

				try
                {
                    SeanceNumberBLO.Save(old_SeanceNumber);
					Alert(string.Format(msgManager.The_entity_has_been_changed, msg_SeanceNumber.SingularName, SeanceNumber), NotificationType.success);
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

		return View(SeanceNumber);
        }
		 
 
        public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeanceNumber SeanceNumber = SeanceNumberBLO.FindBaseEntityByID((long)id);
            if (SeanceNumber == null)
            {
                return HttpNotFound();
            }

			 return View(SeanceNumber);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			SeanceNumber SeanceNumber = SeanceNumberBLO.FindBaseEntityByID((long)id);
            SeanceNumberBLO.Delete(SeanceNumber);
			Alert(string.Format(msgManager.The_entity_has_been_removed, msg_SeanceNumber.SingularName, SeanceNumber), NotificationType.success);
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
                ImportReport importReport = SeanceNumberBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/SeanceNumbers/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
                DataTable dataTable = SeanceNumberBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_SeanceNumber.PluralName + ".xlsx");
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
                SeanceNumberBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class SeanceNumbersController : BaseSeanceNumbersController{};
}

