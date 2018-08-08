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
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions; 
using GApp.Entities;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
namespace TrainingIS.WebApp.Controllers
{  
    // Generated by Manager v 0.2.0 
    public class BaseFormersController : BaseController
    {
        protected FormerBLO FormerBLO = null;

		public BaseFormersController()
        {
            this.msgHelper = new MessagesService(typeof(Former));
			this.FormerBLO = new FormerBLO(this._UnitOfWork);
        }

	    public virtual ActionResult Index()
        {
		    msgHelper.Index(msg);
            List<FormerIndexView> listFormerIndexView = new List<FormerIndexView>();
			foreach (var item in FormerBLO.FindAll()){
                FormerIndexView FormerIndexView = new FormerIndexViewBLM(this._UnitOfWork)
                    .ConverTo_FormerIndexView(item);
                listFormerIndexView.Add(FormerIndexView);
            }
			return View(listFormerIndexView);
		}

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			ViewBag.NationalityId = new SelectList(new NationalityBLO(this._UnitOfWork).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue));
            FormerFormView formerformview = new FormerFormViewBLM(this._UnitOfWork).CreateNew();
            return View(formerformview);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "RegistrationNumber,FirstName,LastName,FirstNameArabe,LastNameArabe,NationalityId,Sex,Birthdate,BirthPlace,CIN,Cellphone,Email,Address,CreateUserAccount,Login,Password")] FormerFormView FormerFormView)
        {
			Former Former = null ;
			Former = new FormerFormViewBLM(this._UnitOfWork)
										.ConverTo_Former(FormerFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    FormerBLO.Save(Former);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Former.SingularName.ToLower(), Former), NotificationType.success);
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
			ViewBag.NationalityId = new SelectList(new NationalityBLO(this._UnitOfWork).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Former.NationalityId);
			return View(FormerFormView);
        }
		 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Former Former = FormerBLO.FindBaseEntityByID((long)id);
            if (Former == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Former.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			FormerFormView FormerFormView = new FormerFormViewBLM(this._UnitOfWork)
                                                                .ConverTo_FormerFormView(Former) ;

			ViewBag.NationalityId = new SelectList(new NationalityBLO(this._UnitOfWork).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), FormerFormView.NationalityId);
 
			return View(FormerFormView);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "RegistrationNumber,FirstName,LastName,FirstNameArabe,LastNameArabe,NationalityId,Sex,Birthdate,BirthPlace,CIN,Cellphone,Email,Address,CreateUserAccount,Login,Password,Id")] FormerFormView FormerFormView)	
        {
			Former Former = new FormerFormViewBLM(this._UnitOfWork)
                .ConverTo_Former( FormerFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    FormerBLO.Save(Former);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Former.SingularName.ToLower(), Former), NotificationType.success);
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

			ViewBag.NationalityId = new SelectList(new NationalityBLO(this._UnitOfWork).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), FormerFormView.NationalityId);
			return View(FormerFormView);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Former Former = FormerBLO.FindBaseEntityByID((long) id);
            if (Former == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Former.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			FormerDetailsView FormerDetailsView = new FormerDetailsView();
		    FormerDetailsView = new FormerDetailsViewBLM(this._UnitOfWork)
                .ConverTo_FormerDetailsView(Former);


			return View(FormerDetailsView);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Former Former = FormerBLO.FindBaseEntityByID((long)id);
            if (Former == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Former.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			FormerDetailsView FormerDetailsView = new FormerDetailsViewBLM(this._UnitOfWork)
							.ConverTo_FormerDetailsView(Former);


			 return View(FormerDetailsView);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			Former Former = FormerBLO.FindBaseEntityByID((long)id);
			if (Former == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Former.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                FormerBLO.Delete(Former);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, Former.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Former.SingularName.ToLower(), Former), NotificationType.success);
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
                ImportReport importReport = FormerBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/Formers/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
                DataTable dataTable = FormerBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_Former.PluralName + ".xlsx");
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
                FormerBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class FormersController : BaseFormersController{};
}

