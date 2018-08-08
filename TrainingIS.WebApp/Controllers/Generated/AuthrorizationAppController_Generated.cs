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
using TrainingIS.Entities.Resources.AuthrorizationAppResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions; 
using GApp.Entities;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.ModelsViews.Authorizations;
namespace TrainingIS.WebApp.Controllers
{  
    // Generated by Manager v 0.2.0 
    public class BaseAuthrorizationAppsController : BaseController
    {
        protected AuthrorizationAppBLO AuthrorizationAppBLO = null;

		public BaseAuthrorizationAppsController()
        {
            this.msgHelper = new MessagesService(typeof(AuthrorizationApp));
			this.AuthrorizationAppBLO = new AuthrorizationAppBLO(this._UnitOfWork);
        }

	    public virtual ActionResult Index()
        {
		    msgHelper.Index(msg);
            List<Default_AuthrorizationAppDetailsView> listDefault_AuthrorizationAppDetailsView = new List<Default_AuthrorizationAppDetailsView>();
			foreach (var item in AuthrorizationAppBLO.FindAll()){
                Default_AuthrorizationAppDetailsView Default_AuthrorizationAppDetailsView = new Default_AuthrorizationAppDetailsViewBLM(this._UnitOfWork)
                    .ConverTo_Default_AuthrorizationAppDetailsView(item);
                listDefault_AuthrorizationAppDetailsView.Add(Default_AuthrorizationAppDetailsView);
            }
			return View(listDefault_AuthrorizationAppDetailsView);
		}

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			ViewBag.ControllerAppId = new SelectList(new ControllerAppBLO(this._UnitOfWork).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue));
			ViewBag.RoleAppId = new SelectList(new RoleAppBLO(this._UnitOfWork).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue));
            AuthrorizationAppFormView authrorizationappformview = new AuthrorizationAppFormViewBLM(this._UnitOfWork).CreateNew();
            return View(authrorizationappformview);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "RoleAppId,ControllerAppId,isAllAction,Selected_ActionControllerApps")] AuthrorizationAppFormView AuthrorizationAppFormView)
        {
			AuthrorizationApp AuthrorizationApp = null ;
			AuthrorizationApp = new AuthrorizationAppFormViewBLM(this._UnitOfWork)
										.ConverTo_AuthrorizationApp(AuthrorizationAppFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    AuthrorizationAppBLO.Save(AuthrorizationApp);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_AuthrorizationApp.SingularName.ToLower(), AuthrorizationApp), NotificationType.success);
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
			ViewBag.ControllerAppId = new SelectList(new ControllerAppBLO(this._UnitOfWork).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), AuthrorizationApp.ControllerAppId);
			ViewBag.RoleAppId = new SelectList(new RoleAppBLO(this._UnitOfWork).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), AuthrorizationApp.RoleAppId);
			return View(AuthrorizationAppFormView);
        }
		 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AuthrorizationApp AuthrorizationApp = AuthrorizationAppBLO.FindBaseEntityByID((long)id);
            if (AuthrorizationApp == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_AuthrorizationApp.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			AuthrorizationAppFormView AuthrorizationAppFormView = new AuthrorizationAppFormViewBLM(this._UnitOfWork)
                                                                .ConverTo_AuthrorizationAppFormView(AuthrorizationApp) ;

			ViewBag.ControllerAppId = new SelectList(new ControllerAppBLO(this._UnitOfWork).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), AuthrorizationAppFormView.ControllerAppId);
			ViewBag.RoleAppId = new SelectList(new RoleAppBLO(this._UnitOfWork).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), AuthrorizationAppFormView.RoleAppId);
 
			return View(AuthrorizationAppFormView);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "RoleAppId,ControllerAppId,isAllAction,Selected_ActionControllerApps,Id")] AuthrorizationAppFormView AuthrorizationAppFormView)	
        {
			AuthrorizationApp AuthrorizationApp = new AuthrorizationAppFormViewBLM(this._UnitOfWork)
                .ConverTo_AuthrorizationApp( AuthrorizationAppFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    AuthrorizationAppBLO.Save(AuthrorizationApp);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_AuthrorizationApp.SingularName.ToLower(), AuthrorizationApp), NotificationType.success);
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

			ViewBag.ControllerAppId = new SelectList(new ControllerAppBLO(this._UnitOfWork).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), AuthrorizationAppFormView.ControllerAppId);
			ViewBag.RoleAppId = new SelectList(new RoleAppBLO(this._UnitOfWork).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), AuthrorizationAppFormView.RoleAppId);
			return View(AuthrorizationAppFormView);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthrorizationApp AuthrorizationApp = AuthrorizationAppBLO.FindBaseEntityByID((long) id);
            if (AuthrorizationApp == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_AuthrorizationApp.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			Default_AuthrorizationAppDetailsView Default_AuthrorizationAppDetailsView = new Default_AuthrorizationAppDetailsView();
		    Default_AuthrorizationAppDetailsView = new Default_AuthrorizationAppDetailsViewBLM(this._UnitOfWork)
                .ConverTo_Default_AuthrorizationAppDetailsView(AuthrorizationApp);


			return View(Default_AuthrorizationAppDetailsView);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AuthrorizationApp AuthrorizationApp = AuthrorizationAppBLO.FindBaseEntityByID((long)id);
            if (AuthrorizationApp == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_AuthrorizationApp.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_AuthrorizationAppDetailsView Default_AuthrorizationAppDetailsView = new Default_AuthrorizationAppDetailsViewBLM(this._UnitOfWork)
							.ConverTo_Default_AuthrorizationAppDetailsView(AuthrorizationApp);


			 return View(Default_AuthrorizationAppDetailsView);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			AuthrorizationApp AuthrorizationApp = AuthrorizationAppBLO.FindBaseEntityByID((long)id);
			if (AuthrorizationApp == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_AuthrorizationApp.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                AuthrorizationAppBLO.Delete(AuthrorizationApp);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, AuthrorizationApp.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_AuthrorizationApp.SingularName.ToLower(), AuthrorizationApp), NotificationType.success);
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
                ImportReport importReport = AuthrorizationAppBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/AuthrorizationApps/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
                DataTable dataTable = AuthrorizationAppBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_AuthrorizationApp.PluralName + ".xlsx");
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
                AuthrorizationAppBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class AuthrorizationAppsController : BaseAuthrorizationAppsController{};
}

