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
using TrainingIS.Entities.Resources.StateOfAbseceResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions; 
using GApp.Entities;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS.WebApp.Controllers
{  
    // Generated by Manager v 0.2.0 
    public class BaseStateOfAbsecesController : BaseController
    {
        protected StateOfAbseceBLO StateOfAbseceBLO = null;

		public BaseStateOfAbsecesController()
        {
            this.msgHelper = new MsgViews(typeof(StateOfAbsece));
			this.StateOfAbseceBLO = new StateOfAbseceBLO(this._UnitOfWork);
        }

	    public virtual ActionResult Index()
        {
		    msgHelper.Index(msg);
            List<Default_StateOfAbseceDetailsView> listDefault_StateOfAbseceDetailsView = new List<Default_StateOfAbseceDetailsView>();
			foreach (var item in StateOfAbseceBLO.FindAll()){
                Default_StateOfAbseceDetailsView Default_StateOfAbseceDetailsView = new Default_StateOfAbseceDetailsViewBLM(this._UnitOfWork)
                    .ConverTo_Default_StateOfAbseceDetailsView(item);
                listDefault_StateOfAbseceDetailsView.Add(Default_StateOfAbseceDetailsView);
            }
			return View(listDefault_StateOfAbseceDetailsView);
		}

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "Name,Category,Value,TraineeId")] Default_StateOfAbseceFormView Default_StateOfAbseceFormView)
        {
			StateOfAbsece StateOfAbsece = new StateOfAbsece() ;
			StateOfAbsece = new Default_StateOfAbseceFormViewBLM(this._UnitOfWork)
										.ConverTo_StateOfAbsece(Default_StateOfAbseceFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				try
                {
                    StateOfAbseceBLO.Save(StateOfAbsece);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msg_StateOfAbsece.SingularName, StateOfAbsece), NotificationType.success);
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
			return View(Default_StateOfAbseceFormView);
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

            StateOfAbsece StateOfAbsece = StateOfAbseceBLO.FindBaseEntityByID((long)id);
            if (StateOfAbsece == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_StateOfAbsece.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_StateOfAbseceDetailsView Default_StateOfAbseceDetailsView = new Default_StateOfAbseceDetailsViewBLM(this._UnitOfWork)
							.ConverTo_Default_StateOfAbseceDetailsView(StateOfAbsece);


			 return View(Default_StateOfAbseceDetailsView);

        }


		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StateOfAbsece StateOfAbsece = StateOfAbseceBLO.FindBaseEntityByID((long)id);
            if (StateOfAbsece == null)
            {
                return HttpNotFound();
            }			 
			Default_StateOfAbseceFormView Default_StateOfAbseceFormView = new Default_StateOfAbseceFormViewBLM(this._UnitOfWork)
                                                                .ConverTo_Default_StateOfAbseceFormView(StateOfAbsece) ;

			return View(Default_StateOfAbseceFormView);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "Name,Category,Value,TraineeId,Id")] Default_StateOfAbseceFormView Default_StateOfAbseceFormView)	
        {
			StateOfAbsece StateOfAbsece = new Default_StateOfAbseceFormViewBLM(this._UnitOfWork)
                .ConverTo_StateOfAbsece( Default_StateOfAbseceFormView);


			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
                StateOfAbsece old_StateOfAbsece = StateOfAbseceBLO.FindBaseEntityByID(StateOfAbsece.Id);
                UpdateModel(old_StateOfAbsece);

				try
                {
                    StateOfAbseceBLO.Save(old_StateOfAbsece);
					Alert(string.Format(msgManager.The_entity_has_been_changed, msg_StateOfAbsece.SingularName, StateOfAbsece), NotificationType.success);
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

			return View(Default_StateOfAbseceFormView);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateOfAbsece StateOfAbsece = StateOfAbseceBLO.FindBaseEntityByID((long) id);
            if (StateOfAbsece == null)
            {
                return HttpNotFound();
            }
			Default_StateOfAbseceDetailsView Default_StateOfAbseceDetailsView = new Default_StateOfAbseceDetailsView();
		    Default_StateOfAbseceDetailsView = new Default_StateOfAbseceDetailsViewBLM(this._UnitOfWork)
                .ConverTo_Default_StateOfAbseceDetailsView(StateOfAbsece);


			return View(Default_StateOfAbseceDetailsView);
        } 

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			StateOfAbsece StateOfAbsece = StateOfAbseceBLO.FindBaseEntityByID((long)id);
			if (StateOfAbsece == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_StateOfAbsece.SingularName);
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            StateOfAbseceBLO.Delete(StateOfAbsece);
			Alert(string.Format(msgManager.The_entity_has_been_removed, msg_StateOfAbsece.SingularName, StateOfAbsece), NotificationType.success);
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
                ImportReport importReport = StateOfAbseceBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/StateOfAbseces/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
                DataTable dataTable = StateOfAbseceBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_StateOfAbsece.PluralName + ".xlsx");
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
                StateOfAbseceBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class StateOfAbsecesController : BaseStateOfAbsecesController{};
}

