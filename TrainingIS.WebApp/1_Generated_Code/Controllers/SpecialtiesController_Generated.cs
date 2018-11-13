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
using TrainingIS.Entities.Resources.SpecialtyResources;
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
    public class BaseSpecialtiesController : TrainingIS_BaseController
    {
        protected SpecialtyBLO SpecialtyBLO = null;

		public BaseSpecialtiesController()
        {
            this.msgHelper = new MessagesService(typeof(Specialty));
			this.SpecialtyBLO = new SpecialtyBLO(this._UnitOfWork, this.GAppContext) ;
        }

	    #region Pagination Methodes
		protected virtual List<Header_DataTable_GAppComponent> Get_GAppDataTable_Header_Text_And_Ids()
        {
            List<Header_DataTable_GAppComponent> herders = new List<Header_DataTable_GAppComponent>();

            foreach (PropertyInfo model_property in typeof(Default_Details_Specialty_Model).GetProperties(typeof(GAppDataTableAttribute)))
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
            foreach (PropertyInfo model_property in typeof(Default_Details_Specialty_Model).GetProperties(typeof(GAppDataTableAttribute)))
            {
                GAppDataTableAttribute gappDataTableAttribute = model_property.GetCustomAttribute(typeof(GAppDataTableAttribute)) as GAppDataTableAttribute;
                string SearchBy = string.IsNullOrEmpty(gappDataTableAttribute.SearchBy) ? model_property.Name : gappDataTableAttribute.SearchBy;
                SearchCreteria.Add(gappDataTableAttribute.SearchBy);
            }
            foreach (PropertyInfo model_property in typeof(Default_Details_Specialty_Model).GetProperties(typeof(SearchByAttribute)))
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
					
			model_property = typeof(Default_Details_Specialty_Model).GetProperty(nameof(Default_Details_Specialty_Model.Sector));
			FilterItem_GAppComponent FilterItem_Sector = new FilterItem_GAppComponent();
			FilterItem_Sector.Id = "Sector.Id_Filter";
			FilterItem_Sector.Label = model_property.getLocalName();
			FilterItem_Sector.Placeholder = model_property.getLocalName();
			FilterItem_Sector.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_Sector = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_Sector.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_Sector != null)
            {
                FilterItem_Sector.Selected = filter_info_Sector.Value;
            }

			var All_Data_Sector = new SectorBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_Sector_msg = string.Format("tous les {0}",msg_Specialty.PluralName.ToLower());
            All_Data_Sector.Insert(0, new Sector { Id = 0, ToStringValue = All_Sector_msg });
            FilterItem_Sector.Data = All_Data_Sector.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_Sector);

	    			
			model_property = typeof(Default_Details_Specialty_Model).GetProperty(nameof(Default_Details_Specialty_Model.TrainingLevel));
			FilterItem_GAppComponent FilterItem_TrainingLevel = new FilterItem_GAppComponent();
			FilterItem_TrainingLevel.Id = "TrainingLevel.Id_Filter";
			FilterItem_TrainingLevel.Label = model_property.getLocalName();
			FilterItem_TrainingLevel.Placeholder = model_property.getLocalName();
			FilterItem_TrainingLevel.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_TrainingLevel = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_TrainingLevel.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_TrainingLevel != null)
            {
                FilterItem_TrainingLevel.Selected = filter_info_TrainingLevel.Value;
            }

			var All_Data_TrainingLevel = new TrainingLevelBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_TrainingLevel_msg = string.Format("tous les {0}",msg_Specialty.PluralName.ToLower());
            All_Data_TrainingLevel.Insert(0, new TrainingLevel { Id = 0, ToStringValue = All_TrainingLevel_msg });
            FilterItem_TrainingLevel.Data = All_Data_TrainingLevel.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_TrainingLevel);

	    
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

            List<Default_Details_Specialty_Model> _ListDefault_Details_Specialty_Model = null;
            try
            {
                filterRequestParams = this.Save_OR_Load_filterRequestParams_State(filterRequestParams);
               _ListDefault_Details_Specialty_Model = new Default_Details_Specialty_ModelBLM(this._UnitOfWork, this.GAppContext)
                   .Find(filterRequestParams, SearchCreteria, out _TotalRecords);

            }
            catch (Exception ex)
            {
                filterRequestParams = new FilterRequestParams();
				this.Delete_filterRequestParams_State();
                _ListDefault_Details_Specialty_Model = new Default_Details_Specialty_ModelBLM(this._UnitOfWork, this.GAppContext)
                  .Find(filterRequestParams, SearchCreteria, out _TotalRecords);
                Alert(ex.Message, NotificationType.warning);
            }

            Index_GAppPage index_page = new Index_GAppPage(filterRequestParams,this.Get_GAppDataTable_Header_Text_And_Ids(), _TotalRecords);
			index_page.Title = msg["Index_Title"];
			this.InitFilter(index_page, filterRequestParams.FilterBy, filterRequestParams.SearchBy);

            ViewBag.index_page = index_page;

            return View(_ListDefault_Details_Specialty_Model);
        }


		protected virtual void Fill_ViewBag_Create(Default_Form_Specialty_Model Default_Form_Specialty_Model)
        {
		ViewBag.SectorId = new SelectList(new SectorBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Specialty_Model.SectorId);
		ViewBag.TrainingLevelId = new SelectList(new TrainingLevelBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Specialty_Model.TrainingLevelId);



        }

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			Default_Form_Specialty_Model default_form_specialty_model = new Default_Form_Specialty_ModelBLM(this._UnitOfWork, this.GAppContext) .CreateNew();
			this.Fill_ViewBag_Create(default_form_specialty_model);
			return View(default_form_specialty_model);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create(Default_Form_Specialty_Model Default_Form_Specialty_Model)
        {
			Specialty Specialty = null ;
			Specialty = new Default_Form_Specialty_ModelBLM(this._UnitOfWork, this.GAppContext) 
										.ConverTo_Specialty(Default_Form_Specialty_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    SpecialtyBLO.Save(Specialty);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Specialty.SingularName.ToLower(), Specialty), NotificationType.success);
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
			this.Fill_ViewBag_Create(Default_Form_Specialty_Model);
			Default_Form_Specialty_Model = new Default_Form_Specialty_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_Form_Specialty_Model(Specialty);
			return View(Default_Form_Specialty_Model);
        }

		protected virtual void Fill_Edit_ViewBag(Default_Form_Specialty_Model Default_Form_Specialty_Model)
        {
			ViewBag.SectorId = new SelectList(new SectorBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Specialty_Model.SectorId);
			ViewBag.TrainingLevelId = new SelectList(new TrainingLevelBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_Specialty_Model.TrainingLevelId);
 



        }
		 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Specialty Specialty = SpecialtyBLO.FindBaseEntityByID((long)id);
            if (Specialty == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Specialty.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			Default_Form_Specialty_Model Default_Form_Specialty_Model = new Default_Form_Specialty_ModelBLM(this._UnitOfWork, this.GAppContext) 
                                                                .ConverTo_Default_Form_Specialty_Model(Specialty) ;

			this.Fill_Edit_ViewBag(Default_Form_Specialty_Model);
			return View(Default_Form_Specialty_Model);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit(Default_Form_Specialty_Model Default_Form_Specialty_Model)	
        {
			Specialty Specialty = new Default_Form_Specialty_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Specialty( Default_Form_Specialty_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    SpecialtyBLO.Save(Specialty);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Specialty.SingularName.ToLower(), Specialty), NotificationType.success);
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
			this.Fill_Edit_ViewBag(Default_Form_Specialty_Model);
			Default_Form_Specialty_Model = new Default_Form_Specialty_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_Form_Specialty_Model(Specialty);
			return View(Default_Form_Specialty_Model);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty Specialty = SpecialtyBLO.FindBaseEntityByID((long) id);
            if (Specialty == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Specialty.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			Default_Details_Specialty_Model Default_Details_Specialty_Model = new Default_Details_Specialty_Model();
		    Default_Details_Specialty_Model = new Default_Details_Specialty_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Default_Details_Specialty_Model(Specialty);


			return View(Default_Details_Specialty_Model);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Specialty Specialty = SpecialtyBLO.FindBaseEntityByID((long)id);
            if (Specialty == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Specialty.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_Details_Specialty_Model Default_Details_Specialty_Model = new Default_Details_Specialty_ModelBLM(this._UnitOfWork, this.GAppContext) 
							.ConverTo_Default_Details_Specialty_Model(Specialty);


			 return View(Default_Details_Specialty_Model);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			Specialty Specialty = SpecialtyBLO.FindBaseEntityByID((long)id);
			if (Specialty == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Specialty.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                SpecialtyBLO.Delete(Specialty);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, Specialty.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Specialty.SingularName.ToLower(), Specialty), NotificationType.success);
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
                ImportReport importReport = SpecialtyBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
					Session["repport_name"] = msg_Specialty.PluralName;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/Specialties/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
                DataTable dataTable = SpecialtyBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
					string FileName = string.Format("{0}-{1}", msg_Specialty.PluralName, DateTime.Now.ToShortDateString());
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
                SpecialtyBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class SpecialtiesController : BaseSpecialtiesController{};
}

