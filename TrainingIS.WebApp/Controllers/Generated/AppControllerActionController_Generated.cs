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
using TrainingIS.Entities.Resources.AppControllerActionResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions; 
using GApp.Entities;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS.WebApp.Controllers
{  
    // Generated by Manager v 0.2.0 
    public class BaseAppControllerActionsController : BaseController
    {
        protected AppControllerActionBLO AppControllerActionBLO = null;

		public BaseAppControllerActionsController()
        {
            this.msgHelper = new MsgViews(typeof(AppControllerAction));
			this.AppControllerActionBLO = new AppControllerActionBLO(this._UnitOfWork);
        }

	    public virtual ActionResult Index()
        {
		    msgHelper.Index(msg);
            List<Default_AppControllerActionDetailsView> listDefault_AppControllerActionDetailsView = new List<Default_AppControllerActionDetailsView>();
			foreach (var item in AppControllerActionBLO.FindAll()){
                Default_AppControllerActionDetailsView Default_AppControllerActionDetailsView = new Default_AppControllerActionDetailsViewBLM(this._UnitOfWork)
                    .ConverTo_Default_AppControllerActionDetailsView(item);
                listDefault_AppControllerActionDetailsView.Add(Default_AppControllerActionDetailsView);
            }
			return View(listDefault_AppControllerActionDetailsView);
		}

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "Code,Description,AppControllerId")] Default_AppControllerActionFormView Default_AppControllerActionFormView)
        {
			AppControllerAction AppControllerAction = new AppControllerAction() ;
			AppControllerAction = new Default_AppControllerActionFormViewBLM(this._UnitOfWork)
										.ConverTo_AppControllerAction(Default_AppControllerActionFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				try
                {
                    AppControllerActionBLO.Save(AppControllerAction);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msg_AppControllerAction.SingularName, AppControllerAction), NotificationType.success);
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
			return View(Default_AppControllerActionFormView);
        }


		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);
            return View();
        } 
		 
        public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AppControllerAction AppControllerAction = AppControllerActionBLO.FindBaseEntityByID((long)id);
            if (AppControllerAction == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_AppControllerAction.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_AppControllerActionDetailsView Default_AppControllerActionDetailsView = new Default_AppControllerActionDetailsViewBLM(this._UnitOfWork)
							.ConverTo_Default_AppControllerActionDetailsView(AppControllerAction);


			 return View(Default_AppControllerActionDetailsView);

        }


		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AppControllerAction AppControllerAction = AppControllerActionBLO.FindBaseEntityByID((long)id);
            if (AppControllerAction == null)
            {
                return HttpNotFound();
            }			 
			Default_AppControllerActionFormView Default_AppControllerActionFormView = new Default_AppControllerActionFormViewBLM(this._UnitOfWork)
                                                                .ConverTo_Default_AppControllerActionFormView(AppControllerAction) ;

			return View(Default_AppControllerActionFormView);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "Code,Description,AppControllerId,Id")] Default_AppControllerActionFormView Default_AppControllerActionFormView)	
        {
			AppControllerAction AppControllerAction = new Default_AppControllerActionFormViewBLM(this._UnitOfWork)
                .ConverTo_AppControllerAction( Default_AppControllerActionFormView);


			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
                AppControllerAction old_AppControllerAction = AppControllerActionBLO.FindBaseEntityByID(AppControllerAction.Id);
                UpdateModel(old_AppControllerAction);

				try
                {
                    AppControllerActionBLO.Save(old_AppControllerAction);
					Alert(string.Format(msgManager.The_entity_has_been_changed, msg_AppControllerAction.SingularName, AppControllerAction), NotificationType.success);
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

			return View(Default_AppControllerActionFormView);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppControllerAction AppControllerAction = AppControllerActionBLO.FindBaseEntityByID((long) id);
            if (AppControllerAction == null)
            {
                return HttpNotFound();
            }
			Default_AppControllerActionDetailsView Default_AppControllerActionDetailsView = new Default_AppControllerActionDetailsView();
		    Default_AppControllerActionDetailsView = new Default_AppControllerActionDetailsViewBLM(this._UnitOfWork)
                .ConverTo_Default_AppControllerActionDetailsView(AppControllerAction);


			return View(Default_AppControllerActionDetailsView);
        } 

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			AppControllerAction AppControllerAction = AppControllerActionBLO.FindBaseEntityByID((long)id);
			if (AppControllerAction == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_AppControllerAction.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            AppControllerActionBLO.Delete(AppControllerAction);
			Alert(string.Format(msgManager.The_entity_has_been_removed, msg_AppControllerAction.SingularName, AppControllerAction), NotificationType.success);
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
                ImportReport importReport = AppControllerActionBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/AppControllerActions/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
                DataTable dataTable = AppControllerActionBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_AppControllerAction.PluralName + ".xlsx");
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
                AppControllerActionBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class AppControllerActionsController : BaseAppControllerActionsController{};
}

