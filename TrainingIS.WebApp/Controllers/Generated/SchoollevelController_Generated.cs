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
using TrainingIS.Entities.Resources.SchoollevelResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
 using TrainingIS.Entities.ModelsViews;
namespace TrainingIS.WebApp.Controllers
{
    // Generated by Manager v 0.2.0
    public class BaseSchoollevelsController : BaseController
    {
        protected SchoollevelBLO SchoollevelBLO = null;

		public BaseSchoollevelsController()
        {
            this.msgHelper = new MsgViews(typeof(Schoollevel));
			this.SchoollevelBLO = new SchoollevelBLO(this._UnitOfWork);
        }
		 
		public virtual ActionResult Index()
        {
			msgHelper.Index(msg);
            Default_IndexSchoollevelView Default_IndexSchoollevelView = new Default_IndexSchoollevelView();
            foreach (var item in SchoollevelBLO.FindAll()) { Default_IndexSchoollevelView.Data.Add(item); }
            return View(Default_IndexSchoollevelView);
		}
		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);


            return View();
        } 
		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "Code,Name,Description,Id")] Schoollevel Schoollevel)
        {
			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				try
                {
                    SchoollevelBLO.Save(Schoollevel);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msg_Schoollevel.SingularName, Schoollevel), NotificationType.success);
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
            return View(Schoollevel);
        }
		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Schoollevel Schoollevel = SchoollevelBLO.FindBaseEntityByID((long) id);
            if (Schoollevel == null)
            {
                return HttpNotFound();
            }
			 return View(Schoollevel);
        } 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Schoollevel Schoollevel = SchoollevelBLO.FindBaseEntityByID((long)id);
            if (Schoollevel == null)
            {
                return HttpNotFound();
            }			 
			return View(Schoollevel);
        }
		        [HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "Code,Name,Description,Id")] Schoollevel Schoollevel)	
        {
			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
                Schoollevel old_Schoollevel = SchoollevelBLO.FindBaseEntityByID(Schoollevel.Id);
                UpdateModel(old_Schoollevel);

				try
                {
                    SchoollevelBLO.Save(old_Schoollevel);
					Alert(string.Format(msgManager.The_entity_has_been_changed, msg_Schoollevel.SingularName, Schoollevel), NotificationType.success);
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

		return View(Schoollevel);
        }
		 
 
        public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Schoollevel Schoollevel = SchoollevelBLO.FindBaseEntityByID((long)id);
            if (Schoollevel == null)
            {
                return HttpNotFound();
            }

			 return View(Schoollevel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			Schoollevel Schoollevel = SchoollevelBLO.FindBaseEntityByID((long)id);
            SchoollevelBLO.Delete(Schoollevel);
			Alert(string.Format(msgManager.The_entity_has_been_removed, msg_Schoollevel.SingularName, Schoollevel), NotificationType.success);
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
                ImportReport importReport = SchoollevelBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/Schoollevels/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
                DataTable dataTable = SchoollevelBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_Schoollevel.PluralName + ".xlsx");
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
                SchoollevelBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class SchoollevelsController : BaseSchoollevelsController{};
}

