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
using TrainingIS.Entities.Resources.ClassroomResources;
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
    public class BaseClassroomsController : BaseController
    {
        protected ClassroomBLO ClassroomBLO = null;

		public BaseClassroomsController()
        {
            this.msgHelper = new MessagesService(typeof(Classroom));
			this.ClassroomBLO = new ClassroomBLO(this._UnitOfWork);
        }

	    public virtual ActionResult Index()
        {
		    msgHelper.Index(msg);
            List<Default_ClassroomDetailsView> listDefault_ClassroomDetailsView = new List<Default_ClassroomDetailsView>();
			foreach (var item in ClassroomBLO.FindAll()){
                Default_ClassroomDetailsView Default_ClassroomDetailsView = new Default_ClassroomDetailsViewBLM(this._UnitOfWork)
                    .ConverTo_Default_ClassroomDetailsView(item);
                listDefault_ClassroomDetailsView.Add(Default_ClassroomDetailsView);
            }
			return View(listDefault_ClassroomDetailsView);
		}

		private void Fill_ViewBag(){



		}

		private void Fill_ViewBag_Create(Default_ClassroomFormView Default_ClassroomFormView)
        {
		ViewBag.ClassroomCategoryId = new SelectList(new ClassroomCategoryBLO(this._UnitOfWork).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_ClassroomFormView.ClassroomCategoryId);
			this.Fill_ViewBag();		
        }

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			Default_ClassroomFormView default_classroomformview = new Default_ClassroomFormViewBLM(this._UnitOfWork).CreateNew();
			this.Fill_ViewBag_Create(default_classroomformview);
			return View(default_classroomformview);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "Code,Name,ClassroomCategoryId,Description")] Default_ClassroomFormView Default_ClassroomFormView)
        {
			Classroom Classroom = null ;
			Classroom = new Default_ClassroomFormViewBLM(this._UnitOfWork)
										.ConverTo_Classroom(Default_ClassroomFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    ClassroomBLO.Save(Classroom);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Classroom.SingularName.ToLower(), Classroom), NotificationType.success);
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
			this.Fill_ViewBag_Create(Default_ClassroomFormView);
			return View(Default_ClassroomFormView);
        }

		private void Fill_Edit_ViewBag(Default_ClassroomFormView Default_ClassroomFormView)
        {
			ViewBag.ClassroomCategoryId = new SelectList(new ClassroomCategoryBLO(this._UnitOfWork).FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_ClassroomFormView.ClassroomCategoryId);
 
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

            Classroom Classroom = ClassroomBLO.FindBaseEntityByID((long)id);
            if (Classroom == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Classroom.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			Default_ClassroomFormView Default_ClassroomFormView = new Default_ClassroomFormViewBLM(this._UnitOfWork)
                                                                .ConverTo_Default_ClassroomFormView(Classroom) ;

			this.Fill_Edit_ViewBag(Default_ClassroomFormView);
			return View(Default_ClassroomFormView);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "Code,Name,ClassroomCategoryId,Description,Id")] Default_ClassroomFormView Default_ClassroomFormView)	
        {
			Classroom Classroom = new Default_ClassroomFormViewBLM(this._UnitOfWork)
                .ConverTo_Classroom( Default_ClassroomFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    ClassroomBLO.Save(Classroom);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Classroom.SingularName.ToLower(), Classroom), NotificationType.success);
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
			this.Fill_Edit_ViewBag(Default_ClassroomFormView);
			return View(Default_ClassroomFormView);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Classroom Classroom = ClassroomBLO.FindBaseEntityByID((long) id);
            if (Classroom == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Classroom.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			Default_ClassroomDetailsView Default_ClassroomDetailsView = new Default_ClassroomDetailsView();
		    Default_ClassroomDetailsView = new Default_ClassroomDetailsViewBLM(this._UnitOfWork)
                .ConverTo_Default_ClassroomDetailsView(Classroom);


			return View(Default_ClassroomDetailsView);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Classroom Classroom = ClassroomBLO.FindBaseEntityByID((long)id);
            if (Classroom == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Classroom.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_ClassroomDetailsView Default_ClassroomDetailsView = new Default_ClassroomDetailsViewBLM(this._UnitOfWork)
							.ConverTo_Default_ClassroomDetailsView(Classroom);


			 return View(Default_ClassroomDetailsView);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			Classroom Classroom = ClassroomBLO.FindBaseEntityByID((long)id);
			if (Classroom == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Classroom.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                ClassroomBLO.Delete(Classroom);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, Classroom.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Classroom.SingularName.ToLower(), Classroom), NotificationType.success);
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
                ImportReport importReport = ClassroomBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/Classrooms/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
                DataTable dataTable = ClassroomBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_Classroom.PluralName + ".xlsx");
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
                ClassroomBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class ClassroomsController : BaseClassroomsController{};
}
