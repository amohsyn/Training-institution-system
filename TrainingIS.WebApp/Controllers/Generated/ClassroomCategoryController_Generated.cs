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
using TrainingIS.Entities.Resources.ClassroomCategoryResources;
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
    public class BaseClassroomCategoriesController : BaseController
    {
        protected ClassroomCategoryBLO ClassroomCategoryBLO = null;

		public BaseClassroomCategoriesController()
        {
            this.msgHelper = new MessagesService(typeof(ClassroomCategory));
			this.ClassroomCategoryBLO = new ClassroomCategoryBLO(this._UnitOfWork);
        }

	    public virtual ActionResult Index()
        {
		    msgHelper.Index(msg);
            List<Default_ClassroomCategoryDetailsView> listDefault_ClassroomCategoryDetailsView = new List<Default_ClassroomCategoryDetailsView>();
			foreach (var item in ClassroomCategoryBLO.FindAll()){
                Default_ClassroomCategoryDetailsView Default_ClassroomCategoryDetailsView = new Default_ClassroomCategoryDetailsViewBLM(this._UnitOfWork)
                    .ConverTo_Default_ClassroomCategoryDetailsView(item);
                listDefault_ClassroomCategoryDetailsView.Add(Default_ClassroomCategoryDetailsView);
            }
			return View(listDefault_ClassroomCategoryDetailsView);
		}

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
            Default_ClassroomCategoryFormView default_classroomcategoryformview = new Default_ClassroomCategoryFormViewBLM(this._UnitOfWork).CreateNew();
            return View(default_classroomcategoryformview);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create([Bind(Include = "Code,Name,Description")] Default_ClassroomCategoryFormView Default_ClassroomCategoryFormView)
        {
			ClassroomCategory ClassroomCategory = null ;
			ClassroomCategory = new Default_ClassroomCategoryFormViewBLM(this._UnitOfWork)
										.ConverTo_ClassroomCategory(Default_ClassroomCategoryFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    ClassroomCategoryBLO.Save(ClassroomCategory);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_ClassroomCategory.SingularName.ToLower(), ClassroomCategory), NotificationType.success);
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
			return View(Default_ClassroomCategoryFormView);
        }
		 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClassroomCategory ClassroomCategory = ClassroomCategoryBLO.FindBaseEntityByID((long)id);
            if (ClassroomCategory == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_ClassroomCategory.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			Default_ClassroomCategoryFormView Default_ClassroomCategoryFormView = new Default_ClassroomCategoryFormViewBLM(this._UnitOfWork)
                                                                .ConverTo_Default_ClassroomCategoryFormView(ClassroomCategory) ;

 
			return View(Default_ClassroomCategoryFormView);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit([Bind(Include = "Code,Name,Description,Id")] Default_ClassroomCategoryFormView Default_ClassroomCategoryFormView)	
        {
			ClassroomCategory ClassroomCategory = new Default_ClassroomCategoryFormViewBLM(this._UnitOfWork)
                .ConverTo_ClassroomCategory( Default_ClassroomCategoryFormView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    ClassroomCategoryBLO.Save(ClassroomCategory);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_ClassroomCategory.SingularName.ToLower(), ClassroomCategory), NotificationType.success);
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

			return View(Default_ClassroomCategoryFormView);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassroomCategory ClassroomCategory = ClassroomCategoryBLO.FindBaseEntityByID((long) id);
            if (ClassroomCategory == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_ClassroomCategory.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			Default_ClassroomCategoryDetailsView Default_ClassroomCategoryDetailsView = new Default_ClassroomCategoryDetailsView();
		    Default_ClassroomCategoryDetailsView = new Default_ClassroomCategoryDetailsViewBLM(this._UnitOfWork)
                .ConverTo_Default_ClassroomCategoryDetailsView(ClassroomCategory);


			return View(Default_ClassroomCategoryDetailsView);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ClassroomCategory ClassroomCategory = ClassroomCategoryBLO.FindBaseEntityByID((long)id);
            if (ClassroomCategory == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_ClassroomCategory.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_ClassroomCategoryDetailsView Default_ClassroomCategoryDetailsView = new Default_ClassroomCategoryDetailsViewBLM(this._UnitOfWork)
							.ConverTo_Default_ClassroomCategoryDetailsView(ClassroomCategory);


			 return View(Default_ClassroomCategoryDetailsView);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			ClassroomCategory ClassroomCategory = ClassroomCategoryBLO.FindBaseEntityByID((long)id);
			if (ClassroomCategory == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_ClassroomCategory.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                ClassroomCategoryBLO.Delete(ClassroomCategory);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, ClassroomCategory.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_ClassroomCategory.SingularName.ToLower(), ClassroomCategory), NotificationType.success);
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
                ImportReport importReport = ClassroomCategoryBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/ClassroomCategories/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
                DataTable dataTable = ClassroomCategoryBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", msg_ClassroomCategory.PluralName + ".xlsx");
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
                ClassroomCategoryBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class ClassroomCategoriesController : BaseClassroomCategoriesController{};
}

