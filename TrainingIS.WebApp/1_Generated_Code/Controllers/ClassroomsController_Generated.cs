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
using TrainingIS.Entities.Resources.ClassroomResources;
using TrainingIS.Entities.ModelsViews;
 
using System.Reflection;
using GApp.Models.DataAnnotations;
using GApp.Models.Pages;
using GApp.Models.GAppComponents;
using GApp.Exceptions;
using TrainingIS.Entities.enums;
using GApp.Core.Localization;

namespace TrainingIS.WebApp.Controllers
{  
    // Generated by GApp v 0.3.0 
    public class BaseClassroomsController : TrainingIS_BaseController
    {
        protected ClassroomBLO ClassroomBLO = null;

		public BaseClassroomsController()
        {
            this.msgHelper = new MessagesService(typeof(Classroom));
			this.ClassroomBLO = new ClassroomBLO(this._UnitOfWork, this.GAppContext) ;
        }

	    #region Pagination Methodes
		protected virtual List<Header_DataTable_GAppComponent> Get_GAppDataTable_Header_Text_And_Ids()
        {
            List<Header_DataTable_GAppComponent> herders = new List<Header_DataTable_GAppComponent>();

            foreach (PropertyInfo model_property in typeof(Default_Classroom_Index_Model).GetProperties(typeof(GAppDataTableAttribute)))
            {
                GAppDataTableAttribute gappDataTableAttribute = model_property.GetCustomAttribute(typeof(GAppDataTableAttribute)) as GAppDataTableAttribute;

				if (gappDataTableAttribute.isColumn == false) continue;

                string OrderBy = string.IsNullOrEmpty(gappDataTableAttribute.OrderBy) ? model_property.Name : gappDataTableAttribute.OrderBy;

                Header_DataTable_GAppComponent header = new Header_DataTable_GAppComponent();
                header.Id = OrderBy;
                header.Name = model_property.getLocalName();
                header.ShortName = model_property.getLocalShortName();
                herders.Add(header);
            }
            return herders;
        }

		[Obsolete("Use  GetSearchCreteria BLO")]
        protected virtual List<string> GetSearchCreteria()
        {
            List<string> SearchCreteria = new List<string>();
            foreach (PropertyInfo model_property in typeof(Default_Classroom_Index_Model).GetProperties(typeof(GAppDataTableAttribute)))
            {
                GAppDataTableAttribute gappDataTableAttribute = model_property.GetCustomAttribute(typeof(GAppDataTableAttribute)) as GAppDataTableAttribute;
                string SearchBy = string.IsNullOrEmpty(gappDataTableAttribute.SearchBy) ? model_property.Name : gappDataTableAttribute.SearchBy;
                SearchCreteria.Add(gappDataTableAttribute.SearchBy);
            }
            foreach (PropertyInfo model_property in typeof(Default_Classroom_Index_Model).GetProperties(typeof(SearchByAttribute)))
            {
                var attributes = model_property.GetCustomAttributes(typeof(SearchByAttribute));
                foreach (var attribute in attributes)
                {
                    SearchCreteria.Add((attribute as SearchByAttribute).PropertyPath);
                }

            }
            return SearchCreteria;
        }
        protected virtual void InitFilter(Index_GAppPage index_page, string FilterBy,string SearchBy)
        {

			var filters_by_infos = DataTable_GAppComponent.ParseFilterBy(FilterBy).ToList();

            
			PropertyInfo model_property = null;
		
            FilterItem_GAppComponent SeachFilter = new FilterItem_GAppComponent();
            SeachFilter.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Search;
            SeachFilter.Label = "Recherche";
			SeachFilter.Selected = SearchBy;
            SeachFilter.Placeholder = SeachFilter.Label;
            index_page.Filter.FilterItems.Add(SeachFilter);

            // Selected Values
            var Filter_Items = DataTable_GAppComponent.ParseFilterBy(FilterBy);
        }
		#endregion

 
		public virtual ActionResult Index(FilterRequestParams filterRequestParams)
        {
            msgHelper.Index(msg);
            Int32 _TotalRecords = 0;
            List<string> SearchCreteria = this.GetSearchCreteria();

            List<Default_Classroom_Index_Model> _ListDefault_Classroom_Index_Model = null;
            try
            {
                filterRequestParams = this.Save_OR_Load_filterRequestParams_State(filterRequestParams);
               _ListDefault_Classroom_Index_Model = new Default_Classroom_Index_ModelBLM(this._UnitOfWork, this.GAppContext)
                   .Find(filterRequestParams, SearchCreteria, out _TotalRecords);

            }
            catch (Exception ex)
            {
                filterRequestParams = new FilterRequestParams();
				this.Delete_filterRequestParams_State();
                _ListDefault_Classroom_Index_Model = new Default_Classroom_Index_ModelBLM(this._UnitOfWork, this.GAppContext)
                  .Find(filterRequestParams, SearchCreteria, out _TotalRecords);
                Alert(ex.Message, NotificationType.warning);
            }

            Index_GAppPage index_page = new Index_GAppPage(filterRequestParams,this.Get_GAppDataTable_Header_Text_And_Ids(), _TotalRecords);
			index_page.Title = msg["Index_Title"];
			this.InitFilter(index_page, filterRequestParams.FilterBy, filterRequestParams.SearchBy);

            ViewBag.index_page = index_page;

            return View(_ListDefault_Classroom_Index_Model);
        }


		protected virtual void Fill_ViewBag_Create(Default_Classroom_Create_Model Default_Classroom_Create_Model)
        {
		ViewBag.ClassroomCategoryId = new SelectList(new ClassroomCategoryBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Classroom_Create_Model.ClassroomCategoryId);



        }

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			Default_Classroom_Create_Model default_classroom_create_model = new Default_Classroom_Create_ModelBLM(this._UnitOfWork, this.GAppContext) .CreateNew();
			this.Fill_ViewBag_Create(default_classroom_create_model);
			return View(default_classroom_create_model);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create(Default_Classroom_Create_Model Default_Classroom_Create_Model)
        {
			Classroom Classroom = null ;
			Classroom = new Default_Classroom_Create_ModelBLM(this._UnitOfWork, this.GAppContext) 
										.ConverTo_Classroom(Default_Classroom_Create_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    ClassroomBLO.Save(Classroom);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Classroom.SingularName.ToLower(), Classroom), NotificationType.success);
					return RedirectToAction("Index");
                }
                catch (GAppException ex)
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
			this.Fill_ViewBag_Create(Default_Classroom_Create_Model);
			Default_Classroom_Create_Model = new Default_Classroom_Create_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_Classroom_Create_Model(Classroom);
			return View(Default_Classroom_Create_Model);
        }

		protected virtual void Fill_Edit_ViewBag(Default_Classroom_Edit_Model Default_Classroom_Edit_Model)
        {
			ViewBag.ClassroomCategoryId = new SelectList(new ClassroomCategoryBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Classroom_Edit_Model.ClassroomCategoryId);
 



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
			Default_Classroom_Edit_Model Default_Classroom_Edit_Model = new Default_Classroom_Edit_ModelBLM(this._UnitOfWork, this.GAppContext) 
                                                                .ConverTo_Default_Classroom_Edit_Model(Classroom) ;

			this.Fill_Edit_ViewBag(Default_Classroom_Edit_Model);
			return View(Default_Classroom_Edit_Model);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit(Default_Classroom_Edit_Model Default_Classroom_Edit_Model)	
        {
			Classroom Classroom = new Default_Classroom_Edit_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Classroom( Default_Classroom_Edit_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    ClassroomBLO.Save(Classroom);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Classroom.SingularName.ToLower(), Classroom), NotificationType.success);
					return RedirectToAction("Index");
                }
                catch (GAppException ex)
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
			this.Fill_Edit_ViewBag(Default_Classroom_Edit_Model);
			Default_Classroom_Edit_Model = new Default_Classroom_Edit_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_Classroom_Edit_Model(Classroom);
			return View(Default_Classroom_Edit_Model);
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
			Default_Classroom_Details_Model Default_Classroom_Details_Model = new Default_Classroom_Details_Model();
		    Default_Classroom_Details_Model = new Default_Classroom_Details_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Default_Classroom_Details_Model(Classroom);


			return View(Default_Classroom_Details_Model);
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

			Default_Classroom_Details_Model Default_Classroom_Details_Model = new Default_Classroom_Details_ModelBLM(this._UnitOfWork, this.GAppContext) 
							.ConverTo_Default_Classroom_Details_Model(Classroom);


			 return View(Default_Classroom_Details_Model);

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
            catch (GAppException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Classroom.SingularName.ToLower(), Classroom), NotificationType.success);
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
                ImportReport importReport = ClassroomBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
					Session["repport_name"] = msg_Classroom.PluralName;
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
					string FileName = string.Format("{0}-{1}", msg_Classroom.PluralName, DateTime.Now.ToShortDateString());
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName + ".xlsx");
                }
            }
        }

		public virtual FileResult LastRepportFile()
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
                ClassroomBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class ClassroomsController : BaseClassroomsController{};
}

