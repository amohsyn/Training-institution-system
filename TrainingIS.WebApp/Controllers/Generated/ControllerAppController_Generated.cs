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
using TrainingIS.Entities.Resources.ControllerAppResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions; 
using GApp.Entities;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS.WebApp.Controllers
{  
    // Generated by Manager v 0.2.0 
    public class BaseControllerAppsController : BaseController
    {
        protected ControllerAppBLO ControllerAppBLO = null;

		public BaseControllerAppsController()
        {
            this.msgHelper = new MessagesService(typeof(ControllerApp));
			this.ControllerAppBLO = new ControllerAppBLO(this._UnitOfWork);
        }

	    public virtual ActionResult Index()
        {
		    msgHelper.Index(msg);
            List<Default_ControllerAppDetailsView> listDefault_ControllerAppDetailsView = new List<Default_ControllerAppDetailsView>();
			foreach (var item in ControllerAppBLO.FindAll()){
                Default_ControllerAppDetailsView Default_ControllerAppDetailsView = new Default_ControllerAppDetailsViewBLM(this._UnitOfWork)
                    .ConverTo_Default_ControllerAppDetailsView(item);
                listDefault_ControllerAppDetailsView.Add(Default_ControllerAppDetailsView);
            }
			return View(listDefault_ControllerAppDetailsView);
		}

		private void Fill_ViewBag(){



		}

		private void Fill_ViewBag_Create(Default_ControllerAppFormView Default_ControllerAppFormView)
        {
			this.Fill_ViewBag();		
        }

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			Default_ControllerAppFormView default_controllerappformview = new Default_ControllerAppFormViewBLM(this._UnitOfWork).CreateNew();
			this.Fill_ViewBag_Create(default_controllerappformview);
			return View(default_controllerappformview);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "Code,Name,Description")] Default_ControllerAppFormView Default_ControllerAppFormView)
        {
			ControllerApp ControllerApp = null ;
			ControllerApp = new Default_ControllerAppFormViewBLM(this._UnitOfWork)
										.ConverTo_ControllerApp(Default_ControllerAppFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    ControllerAppBLO.Save(ControllerApp);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_ControllerApp.SingularName.ToLower(), ControllerApp), NotificationType.success);
					return RedirectToAction("Index");
                }
                catch (GAppDbException ex)
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
			this.Fill_ViewBag_Create(Default_ControllerAppFormView);
			return View(Default_ControllerAppFormView);
        }

		private void Fill_Edit_ViewBag(Default_ControllerAppFormView Default_ControllerAppFormView)
        {
 
			this.Fill_ViewBag();
        }
		 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ControllerApp ControllerApp = ControllerAppBLO.FindBaseEntityByID((long)id);
            if (ControllerApp == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_ControllerApp.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			Default_ControllerAppFormView Default_ControllerAppFormView = new Default_ControllerAppFormViewBLM(this._UnitOfWork)
                                                                .ConverTo_Default_ControllerAppFormView(ControllerApp) ;

			this.Fill_Edit_ViewBag(Default_ControllerAppFormView);
			return View(Default_ControllerAppFormView);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "Code,Name,Description,Id")] Default_ControllerAppFormView Default_ControllerAppFormView)	
        {
			ControllerApp ControllerApp = new Default_ControllerAppFormViewBLM(this._UnitOfWork)
                .ConverTo_ControllerApp( Default_ControllerAppFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    ControllerAppBLO.Save(ControllerApp);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_ControllerApp.SingularName.ToLower(), ControllerApp), NotificationType.success);
					return RedirectToAction("Index");
                }
                catch (GAppDbException ex)
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
			this.Fill_Edit_ViewBag(Default_ControllerAppFormView);
			return View(Default_ControllerAppFormView);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControllerApp ControllerApp = ControllerAppBLO.FindBaseEntityByID((long) id);
            if (ControllerApp == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_ControllerApp.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			Default_ControllerAppDetailsView Default_ControllerAppDetailsView = new Default_ControllerAppDetailsView();
		    Default_ControllerAppDetailsView = new Default_ControllerAppDetailsViewBLM(this._UnitOfWork)
                .ConverTo_Default_ControllerAppDetailsView(ControllerApp);


			return View(Default_ControllerAppDetailsView);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ControllerApp ControllerApp = ControllerAppBLO.FindBaseEntityByID((long)id);
            if (ControllerApp == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_ControllerApp.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_ControllerAppDetailsView Default_ControllerAppDetailsView = new Default_ControllerAppDetailsViewBLM(this._UnitOfWork)
							.ConverTo_Default_ControllerAppDetailsView(ControllerApp);


			 return View(Default_ControllerAppDetailsView);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			ControllerApp ControllerApp = ControllerAppBLO.FindBaseEntityByID((long)id);
			if (ControllerApp == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_ControllerApp.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                ControllerAppBLO.Delete(ControllerApp);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, ControllerApp.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_ControllerApp.SingularName.ToLower(), ControllerApp), NotificationType.success);
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
                ImportReport importReport = ControllerAppBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/ControllerApps/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
                DataTable dataTable = ControllerAppBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_ControllerApp.PluralName + ".xlsx");
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
                ControllerAppBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class ControllerAppsController : BaseControllerAppsController{};
}

