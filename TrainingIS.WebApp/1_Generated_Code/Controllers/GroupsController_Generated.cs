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
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
 
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
    public class BaseGroupsController : TrainingIS_BaseController
    {
        protected GroupBLO GroupBLO = null;

		public BaseGroupsController()
        {
            this.msgHelper = new MessagesService(typeof(Group));
			this.GroupBLO = new GroupBLO(this._UnitOfWork, this.GAppContext) ;
        }

	    #region Pagination Methodes
		protected virtual List<Header_DataTable_GAppComponent> Get_GAppDataTable_Header_Text_And_Ids()
        {
            List<Header_DataTable_GAppComponent> herders = new List<Header_DataTable_GAppComponent>();

            foreach (PropertyInfo model_property in typeof(IndexGroupView).GetProperties(typeof(GAppDataTableAttribute)))
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

        protected virtual void InitFilter(Index_GAppPage index_page, string FilterBy,string SearchBy)
        {

			var filters_by_infos = DataTable_GAppComponent.ParseFilterBy(FilterBy).ToList();

            
			PropertyInfo model_property = null;
					
			model_property = typeof(IndexGroupView).GetProperty(nameof(IndexGroupView.YearStudy));
			FilterItem_GAppComponent FilterItem_YearStudy = new FilterItem_GAppComponent();
			FilterItem_YearStudy.Id = "YearStudy.Id_Filter";
			FilterItem_YearStudy.Label = model_property.getLocalName();
			FilterItem_YearStudy.Placeholder = model_property.getLocalName();
			FilterItem_YearStudy.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_YearStudy = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_YearStudy.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_YearStudy != null)
            {
                FilterItem_YearStudy.Selected = filter_info_YearStudy.Value;
            }

			var All_Data_YearStudy = new YearStudyBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_YearStudy_msg = string.Format("tous les {0}",msg_Group.PluralName.ToLower());
            All_Data_YearStudy.Insert(0, new YearStudy { Id = 0, ToStringValue = All_YearStudy_msg });
            FilterItem_YearStudy.Data = All_Data_YearStudy.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_YearStudy);

	    			
			model_property = typeof(IndexGroupView).GetProperty(nameof(IndexGroupView.Specialty));
			FilterItem_GAppComponent FilterItem_Specialty = new FilterItem_GAppComponent();
			FilterItem_Specialty.Id = "Specialty.Id_Filter";
			FilterItem_Specialty.Label = model_property.getLocalName();
			FilterItem_Specialty.Placeholder = model_property.getLocalName();
			FilterItem_Specialty.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_Specialty = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_Specialty.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_Specialty != null)
            {
                FilterItem_Specialty.Selected = filter_info_Specialty.Value;
            }

			var All_Data_Specialty = new SpecialtyBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_Specialty_msg = string.Format("tous les {0}",msg_Group.PluralName.ToLower());
            All_Data_Specialty.Insert(0, new Specialty { Id = 0, ToStringValue = All_Specialty_msg });
            FilterItem_Specialty.Data = All_Data_Specialty.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_Specialty);

	    			
			model_property = typeof(IndexGroupView).GetProperty(nameof(IndexGroupView.TrainingType));
			FilterItem_GAppComponent FilterItem_TrainingType = new FilterItem_GAppComponent();
			FilterItem_TrainingType.Id = "TrainingType.Id_Filter";
			FilterItem_TrainingType.Label = model_property.getLocalName();
			FilterItem_TrainingType.Placeholder = model_property.getLocalName();
			FilterItem_TrainingType.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_TrainingType = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_TrainingType.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_TrainingType != null)
            {
                FilterItem_TrainingType.Selected = filter_info_TrainingType.Value;
            }

			var All_Data_TrainingType = new TrainingTypeBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_TrainingType_msg = string.Format("tous les {0}",msg_Group.PluralName.ToLower());
            All_Data_TrainingType.Insert(0, new TrainingType { Id = 0, ToStringValue = All_TrainingType_msg });
            FilterItem_TrainingType.Data = All_Data_TrainingType.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_TrainingType);

	    
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
            List<string> SearchCreteria = this.GroupBLO.GetSearchCreteria();

            List<IndexGroupView> _ListIndexGroupView = null;
            try
            {
                filterRequestParams = this.Save_OR_Load_filterRequestParams_State(filterRequestParams);
               _ListIndexGroupView = new IndexGroupViewBLM(this._UnitOfWork, this.GAppContext)
                   .Find(filterRequestParams, SearchCreteria, out _TotalRecords);

            }
            catch (Exception ex)
            {
                filterRequestParams = new FilterRequestParams();
				this.Delete_filterRequestParams_State();
                _ListIndexGroupView = new IndexGroupViewBLM(this._UnitOfWork, this.GAppContext)
                  .Find(filterRequestParams, SearchCreteria, out _TotalRecords);
                Alert(ex.Message, NotificationType.warning);
            }

            Index_GAppPage index_page = new Index_GAppPage(filterRequestParams,this.Get_GAppDataTable_Header_Text_And_Ids(), _TotalRecords);
			index_page.Title = msg["Index_Title"];
			this.InitFilter(index_page, filterRequestParams.FilterBy, filterRequestParams.SearchBy);

            ViewBag.index_page = index_page;

            return View(_ListIndexGroupView);
        }


		protected virtual void Fill_ViewBag_Create(CreateGroupView CreateGroupView)
        {
			ViewBag.SpecialtyId = new SelectList(new SpecialtyBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), CreateGroupView.SpecialtyId);
			ViewBag.TrainingTypeId = new SelectList(new TrainingTypeBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), CreateGroupView.TrainingTypeId);
			ViewBag.TrainingYearId = new SelectList(new TrainingYearBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), CreateGroupView.TrainingYearId);
			ViewBag.YearStudyId = new SelectList(new YearStudyBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), CreateGroupView.YearStudyId);



        }

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			CreateGroupView creategroupview = new CreateGroupViewBLM(this._UnitOfWork, this.GAppContext) .CreateNew();
			this.Fill_ViewBag_Create(creategroupview);
			return View(creategroupview);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create(CreateGroupView CreateGroupView)
        {
			Group Group = null ;
			Group = new CreateGroupViewBLM(this._UnitOfWork, this.GAppContext) 
										.ConverTo_Group(CreateGroupView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    GroupBLO.Save(Group);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Group.SingularName.ToLower(), Group), NotificationType.success);
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
			this.Fill_ViewBag_Create(CreateGroupView);
			CreateGroupView = new CreateGroupViewBLM(this._UnitOfWork, this.GAppContext).ConverTo_CreateGroupView(Group);
			return View(CreateGroupView);
        }

		protected virtual void Fill_Edit_ViewBag(EditGroupView EditGroupView)
        {
			ViewBag.SpecialtyId = new SelectList(new SpecialtyBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), EditGroupView.SpecialtyId);
			ViewBag.TrainingTypeId = new SelectList(new TrainingTypeBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), EditGroupView.TrainingTypeId);
			ViewBag.TrainingYearId = new SelectList(new TrainingYearBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), EditGroupView.TrainingYearId);
			ViewBag.YearStudyId = new SelectList(new YearStudyBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), EditGroupView.YearStudyId);
 



        }
		 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group Group = GroupBLO.FindBaseEntityByID((long)id);
            if (Group == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Group.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			EditGroupView EditGroupView = new EditGroupViewBLM(this._UnitOfWork, this.GAppContext) 
                                                                .ConverTo_EditGroupView(Group) ;

			this.Fill_Edit_ViewBag(EditGroupView);
			return View(EditGroupView);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit(EditGroupView EditGroupView)	
        {
			if(EditGroupView.Id == 0)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Group.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Group Group = new EditGroupViewBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Group( EditGroupView);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    GroupBLO.Save(Group);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Group.SingularName.ToLower(), Group), NotificationType.success);
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
			this.Fill_Edit_ViewBag(EditGroupView);
			EditGroupView = new EditGroupViewBLM(this._UnitOfWork, this.GAppContext).ConverTo_EditGroupView(Group);
			return View(EditGroupView);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group Group = GroupBLO.FindBaseEntityByID((long) id);
            if (Group == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Group.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			DetailsGroupView DetailsGroupView = new DetailsGroupView();
		    DetailsGroupView = new DetailsGroupViewBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_DetailsGroupView(Group);


			return View(DetailsGroupView);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group Group = GroupBLO.FindBaseEntityByID((long)id);
            if (Group == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Group.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			DetailsGroupView DetailsGroupView = new DetailsGroupViewBLM(this._UnitOfWork, this.GAppContext) 
							.ConverTo_DetailsGroupView(Group);


			 return View(DetailsGroupView);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			Group Group = GroupBLO.FindBaseEntityByID((long)id);
			if (Group == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Group.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                GroupBLO.Delete(Group);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, Group.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Group.SingularName.ToLower(), Group), NotificationType.success);
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
                ImportReport importReport = GroupBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
					Session["repport_name"] = msg_Group.PluralName;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/Groups/LastRepportFile\">Télécharger le rapport d'importation</a>";
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

		#region Export and Import
        /// <summary>
        /// [Generalize] Export the filterted Groups
        /// </summary>
        /// <returns></returns>
        public virtual FileResult Export()
        {
            var dataTable = this.GroupBLO.Export(this.GetType().Name);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string FileName = GroupBLO.Get_Export_File_Name();
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName);
                }
            }
        }
        public virtual FileResult Import_File_Example()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dataTable = GroupBLO.Import_File_Example();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string FileName = this.GroupBLO.Get_Import_File_Name();
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName);
                }
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                GroupBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class GroupsController : BaseGroupsController{};
}

