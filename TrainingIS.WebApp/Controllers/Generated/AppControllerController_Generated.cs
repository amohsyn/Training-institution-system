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
using TrainingIS.Entities.Resources.AppControllerResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions;
using GApp.Entities;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS.WebApp.Controllers
{  
    // Generated by Manager v 0.2.0 
    public class BaseAppControllersController : BaseController
    {
        protected AppControllerBLO AppControllerBLO = null;

		public BaseAppControllersController()
        {
            this.msgHelper = new MsgViews(typeof(AppController));
			this.AppControllerBLO = new AppControllerBLO(this._UnitOfWork);
        }
		 
		public virtual ActionResult Index()
        {
		    msgHelper.Index(msg);
            List<AppControllerDetailsView> listAppControllerDetailsView = new List<AppControllerDetailsView>();
			foreach (var item in AppControllerBLO.FindAll()){
                AppControllerDetailsView AppControllerDetailsView = item;
                listAppControllerDetailsView.Add(AppControllerDetailsView);

            }
			return View(listAppControllerDetailsView);

		}
				public virtual ActionResult Create()
        {
			msgHelper.Create(msg);
            return View();
        } 
		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "Code,Description,SelectListRoles,SelectedRoles,AppRoles")] AppControllerFormView AppControllerFormView)
        {
			AppController AppController = new AppController() ;
			AppController = new AppControllerFormViewBLM(this._UnitOfWork)
										.ConverTo_AppController(AppControllerFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				try
                {
                    AppControllerBLO.Save(AppController);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msg_AppController.SingularName, AppController), NotificationType.success);
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
			return View(AppControllerFormView);
        }
		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppController AppController = AppControllerBLO.FindBaseEntityByID((long) id);
            if (AppController == null)
            {
                return HttpNotFound();
            }
			AppControllerDetailsView AppControllerDetailsView = new AppControllerDetailsView();
		    AppControllerDetailsView = AppController;
			return View(AppControllerDetailsView);
        } 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AppController AppController = AppControllerBLO.FindBaseEntityByID((long)id);
            if (AppController == null)
            {
                return HttpNotFound();
            }			 
			AppControllerFormView AppControllerFormView = new AppControllerFormViewBLM(this._UnitOfWork)
                                                                .ConverTo_AppControllerFormView(AppController) ;

			return View(AppControllerFormView);
        }
			
		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "Code,Description,SelectListRoles,SelectedRoles,AppRoles,Id")] AppControllerFormView AppControllerFormView)	
        {
			AppController AppController = new AppControllerFormViewBLM(this._UnitOfWork)
                .ConverTo_AppController( AppControllerFormView);


			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
                AppController old_AppController = AppControllerBLO.FindBaseEntityByID(AppController.Id);
                UpdateModel(old_AppController);

				try
                {
                    AppControllerBLO.Save(old_AppController);
					Alert(string.Format(msgManager.The_entity_has_been_changed, msg_AppController.SingularName, AppController), NotificationType.success);
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

			return View(AppControllerFormView);
        }
		 
 
        public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AppController AppController = AppControllerBLO.FindBaseEntityByID((long)id);
            if (AppController == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_AppController.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			AppControllerDetailsView AppControllerDetailsView = AppController;
			 return View(AppControllerDetailsView);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			AppController AppController = AppControllerBLO.FindBaseEntityByID((long)id);
			if (AppController == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_AppController.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            AppControllerBLO.Delete(AppController);
			Alert(string.Format(msgManager.The_entity_has_been_removed, msg_AppController.SingularName, AppController), NotificationType.success);
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
                ImportReport importReport = AppControllerBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/AppControllers/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
                DataTable dataTable = AppControllerBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_AppController.PluralName + ".xlsx");
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
                AppControllerBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class AppControllersController : BaseAppControllersController{};
}

