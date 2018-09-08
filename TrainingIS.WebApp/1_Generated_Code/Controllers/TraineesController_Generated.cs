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
using TrainingIS.WebApp.Manager.Views.msgs;
using TrainingIS.WebApp.Helpers;
using GApp.DAL.Exceptions; 
using GApp.Entities;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities.Base;
using GApp.WebApp.Controllers;
using GApp.BLL.Services;
using GApp.BLL.Enums;
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Models.Trainees;
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS.WebApp.Controllers
{  
    // Generated by GApp v 0.3.0 
    public class BaseTraineesController : BaseController<TrainingISModel>
    {
        protected TraineeBLO TraineeBLO = null;

		public BaseTraineesController()
        {
            this.msgHelper = new MessagesService(typeof(Trainee));
			this.TraineeBLO = new TraineeBLO(this._UnitOfWork, this.GAppContext) ;
        }

	    public virtual ActionResult Index()
        {
		    msgHelper.Index(msg);
            List<Index_Trainee_Model> listIndex_Trainee_Model = new List<Index_Trainee_Model>();
			foreach (var item in TraineeBLO.FindAll()){
                Index_Trainee_Model Index_Trainee_Model = new Index_Trainee_ModelBLM(this._UnitOfWork, this.GAppContext) 
                    .ConverTo_Index_Trainee_Model(item);
                listIndex_Trainee_Model.Add(Index_Trainee_Model);
            }
			return View(listIndex_Trainee_Model);
		}

		protected void Fill_ViewBag_Create(Default_Form_Trainee_Model Default_Form_Trainee_Model)
        {
		ViewBag.GroupId = new SelectList(new GroupBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Trainee_Model.GroupId);
		ViewBag.NationalityId = new SelectList(new NationalityBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Trainee_Model.NationalityId);
		ViewBag.SchoollevelId = new SelectList(new SchoollevelBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Trainee_Model.SchoollevelId);
		ViewBag.SpecialtyId = new SelectList(new SpecialtyBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Trainee_Model.SpecialtyId);
		ViewBag.YearStudyId = new SelectList(new YearStudyBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Trainee_Model.YearStudyId);



        }

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			Default_Form_Trainee_Model default_form_trainee_model = new Default_Form_Trainee_ModelBLM(this._UnitOfWork, this.GAppContext) .CreateNew();
			this.Fill_ViewBag_Create(default_form_trainee_model);
			return View(default_form_trainee_model);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create(Default_Form_Trainee_Model Default_Form_Trainee_Model)
        {
			Trainee Trainee = null ;
			Trainee = new Default_Form_Trainee_ModelBLM(this._UnitOfWork, this.GAppContext) 
										.ConverTo_Trainee(Default_Form_Trainee_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    TraineeBLO.Save(Trainee);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Trainee.SingularName.ToLower(), Trainee), NotificationType.success);
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
			this.Fill_ViewBag_Create(Default_Form_Trainee_Model);
			Default_Form_Trainee_Model = new Default_Form_Trainee_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_Form_Trainee_Model(Trainee);
			return View(Default_Form_Trainee_Model);
        }

		protected void Fill_Edit_ViewBag(Default_Form_Trainee_Model Default_Form_Trainee_Model)
        {
			ViewBag.GroupId = new SelectList(new GroupBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Trainee_Model.GroupId);
			ViewBag.NationalityId = new SelectList(new NationalityBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Trainee_Model.NationalityId);
			ViewBag.SchoollevelId = new SelectList(new SchoollevelBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Trainee_Model.SchoollevelId);
			ViewBag.SpecialtyId = new SelectList(new SpecialtyBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Trainee_Model.SpecialtyId);
			ViewBag.YearStudyId = new SelectList(new YearStudyBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Trainee_Model.YearStudyId);
 



        }
		 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Trainee Trainee = TraineeBLO.FindBaseEntityByID((long)id);
            if (Trainee == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Trainee.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			Default_Form_Trainee_Model Default_Form_Trainee_Model = new Default_Form_Trainee_ModelBLM(this._UnitOfWork, this.GAppContext) 
                                                                .ConverTo_Default_Form_Trainee_Model(Trainee) ;

			this.Fill_Edit_ViewBag(Default_Form_Trainee_Model);
			return View(Default_Form_Trainee_Model);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit(Default_Form_Trainee_Model Default_Form_Trainee_Model)	
        {
			Trainee Trainee = new Default_Form_Trainee_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Trainee( Default_Form_Trainee_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    TraineeBLO.Save(Trainee);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Trainee.SingularName.ToLower(), Trainee), NotificationType.success);
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
			this.Fill_Edit_ViewBag(Default_Form_Trainee_Model);
			Default_Form_Trainee_Model = new Default_Form_Trainee_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_Form_Trainee_Model(Trainee);
			return View(Default_Form_Trainee_Model);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee Trainee = TraineeBLO.FindBaseEntityByID((long) id);
            if (Trainee == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Trainee.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			Default_Details_Trainee_Model Default_Details_Trainee_Model = new Default_Details_Trainee_Model();
		    Default_Details_Trainee_Model = new Default_Details_Trainee_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Default_Details_Trainee_Model(Trainee);


			return View(Default_Details_Trainee_Model);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Trainee Trainee = TraineeBLO.FindBaseEntityByID((long)id);
            if (Trainee == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Trainee.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_Details_Trainee_Model Default_Details_Trainee_Model = new Default_Details_Trainee_ModelBLM(this._UnitOfWork, this.GAppContext) 
							.ConverTo_Default_Details_Trainee_Model(Trainee);


			 return View(Default_Details_Trainee_Model);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			Trainee Trainee = TraineeBLO.FindBaseEntityByID((long)id);
			if (Trainee == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Trainee.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                TraineeBLO.Delete(Trainee);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, Trainee.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Trainee.SingularName.ToLower(), Trainee), NotificationType.success);
            return RedirectToAction("Index");
        }

		public virtual ActionResult Import()
        { 
			this.Create_Files_Directory_If_Not_Exist();
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
                ImportReport importReport = TraineeBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
					Session["repport_name"] = msg_Trainee.PluralName;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/Trainees/LastRepportFile\">Télécharger le rapport d'importation</a>";
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

		protected void Create_Files_Directory_If_Not_Exist()
        {
            string Files_path = Server.MapPath("~/Content/Files");
            if(!Directory.Exists(Files_path))
            {
                Directory.CreateDirectory(Files_path);

            }
        }

        public virtual FileResult Export()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dataTable = TraineeBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
					string FileName = string.Format("{0}-{1}", msg_Trainee.PluralName, DateTime.Now.ToShortDateString());
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName + ".xlsx");
                }
            }
        }

		public FileResult LastRepportFile()
        {
            // [Bug] if the user try to Import multiple data in the same time
            if (Session["path_repport"] != null)
            {
                string path = Session["path_repport"] as string;
				string name = Session["repport_name"] as string;

                var fileStream = new FileStream(path, FileMode.Open);
                string FileName = string.Format("Rapport d'import-{0}-{1}",name,DateTime.Now.ToShortDateString());
                return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName + ".xlsx");

            }
            return null;

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                TraineeBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class TraineesController : BaseTraineesController{};
}

