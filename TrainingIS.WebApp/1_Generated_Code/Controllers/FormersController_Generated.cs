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
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Models.FormerModelsViews;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
 
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
    public class BaseFormersController : TrainingIS_BaseController
    {
        protected FormerBLO FormerBLO = null;

		public BaseFormersController()
        {
            this.msgHelper = new MessagesService(typeof(Former));
			this.FormerBLO = new FormerBLO(this._UnitOfWork, this.GAppContext) ;
        }

	    #region Pagination Methodes
		protected virtual List<Header_DataTable_GAppComponent> Get_GAppDataTable_Header_Text_And_Ids()
        {
            List<Header_DataTable_GAppComponent> herders = new List<Header_DataTable_GAppComponent>();

            foreach (PropertyInfo model_property in typeof(FormerIndexView).GetProperties(typeof(GAppDataTableAttribute)))
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
					
			model_property = typeof(FormerIndexView).GetProperty(nameof(FormerIndexView.FormerSpecialty));
			FilterItem_GAppComponent FilterItem_FormerSpecialty = new FilterItem_GAppComponent();
			FilterItem_FormerSpecialty.Id = "FormerSpecialty.Id_Filter";
			FilterItem_FormerSpecialty.Label = model_property.getLocalName();
			FilterItem_FormerSpecialty.Placeholder = model_property.getLocalName();
			FilterItem_FormerSpecialty.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_FormerSpecialty = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_FormerSpecialty.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_FormerSpecialty != null)
            {
                FilterItem_FormerSpecialty.Selected = filter_info_FormerSpecialty.Value;
            }

			var All_Data_FormerSpecialty = new FormerSpecialtyBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_FormerSpecialty_msg = string.Format("tous les {0}",msg_Former.PluralName.ToLower());
            All_Data_FormerSpecialty.Insert(0, new FormerSpecialty { Id = 0, ToStringValue = All_FormerSpecialty_msg });
            FilterItem_FormerSpecialty.Data = All_Data_FormerSpecialty.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_FormerSpecialty);

	    
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
            List<string> SearchCreteria = this.FormerBLO.GetSearchCreteria();

            List<FormerIndexView> _ListFormerIndexView = null;
            try
            {
                filterRequestParams = this.Save_OR_Load_filterRequestParams_State(filterRequestParams);
               _ListFormerIndexView = new FormerIndexViewBLM(this._UnitOfWork, this.GAppContext)
                   .Find(filterRequestParams, SearchCreteria, out _TotalRecords);

            }
            catch (Exception ex)
            {
                filterRequestParams = new FilterRequestParams();
				this.Delete_filterRequestParams_State();
                _ListFormerIndexView = new FormerIndexViewBLM(this._UnitOfWork, this.GAppContext)
                  .Find(filterRequestParams, SearchCreteria, out _TotalRecords);
                Alert(ex.Message, NotificationType.warning);
            }

            Index_GAppPage index_page = new Index_GAppPage(filterRequestParams,this.Get_GAppDataTable_Header_Text_And_Ids(), _TotalRecords);
			index_page.Title = msg["Index_Title"];
			this.InitFilter(index_page, filterRequestParams.FilterBy, filterRequestParams.SearchBy);

            ViewBag.index_page = index_page;

            return View(_ListFormerIndexView);
        }


		protected virtual void Fill_ViewBag_Create(Create_Former_Model Create_Former_Model)
        {
		ViewBag.FormerSpecialtyId = new SelectList(new FormerSpecialtyBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Create_Former_Model.FormerSpecialtyId);
		ViewBag.NationalityId = new SelectList(new NationalityBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Create_Former_Model.NationalityId);



        }

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			Create_Former_Model create_former_model = new Create_Former_ModelBLM(this._UnitOfWork, this.GAppContext) .CreateNew();
			this.Fill_ViewBag_Create(create_former_model);
			return View(create_former_model);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create(Create_Former_Model Create_Former_Model)
        {
			Former Former = null ;
			Former = new Create_Former_ModelBLM(this._UnitOfWork, this.GAppContext) 
										.ConverTo_Former(Create_Former_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    FormerBLO.Save(Former);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Former.SingularName.ToLower(), Former), NotificationType.success);
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
			this.Fill_ViewBag_Create(Create_Former_Model);
			Create_Former_Model = new Create_Former_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Create_Former_Model(Former);
			return View(Create_Former_Model);
        }

		protected virtual void Fill_Edit_ViewBag(Edit_Former_Model Edit_Former_Model)
        {
			ViewBag.FormerSpecialtyId = new SelectList(new FormerSpecialtyBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Edit_Former_Model.FormerSpecialtyId);
			ViewBag.NationalityId = new SelectList(new NationalityBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Edit_Former_Model.NationalityId);
 



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
			Edit_Former_Model Edit_Former_Model = new Edit_Former_ModelBLM(this._UnitOfWork, this.GAppContext) 
                                                                .ConverTo_Edit_Former_Model(Former) ;

			this.Fill_Edit_ViewBag(Edit_Former_Model);
			return View(Edit_Former_Model);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit(Edit_Former_Model Edit_Former_Model)	
        {
			Former Former = new Edit_Former_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Former( Edit_Former_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    FormerBLO.Save(Former);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Former.SingularName.ToLower(), Former), NotificationType.success);
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
			this.Fill_Edit_ViewBag(Edit_Former_Model);
			Edit_Former_Model = new Edit_Former_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Edit_Former_Model(Former);
			return View(Edit_Former_Model);
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
		    FormerDetailsView = new FormerDetailsViewBLM(this._UnitOfWork, this.GAppContext) 
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

			FormerDetailsView FormerDetailsView = new FormerDetailsViewBLM(this._UnitOfWork, this.GAppContext) 
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
            catch (GAppException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Former.SingularName.ToLower(), Former), NotificationType.success);
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
                ImportReport importReport = FormerBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
					Session["repport_name"] = msg_Former.PluralName;
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
        /// [Generalize] Export the filterted Formers
        /// </summary>
        /// <returns></returns>
        public virtual FileResult Export()
        {
            var dataTable = this.FormerBLO.Export(this.GetType().Name);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string FileName = FormerBLO.Get_Export_File_Name();
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName);
                }
            }
        }
        public virtual FileResult Import_File_Example()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dataTable = FormerBLO.Import_File_Example();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string FileName = this.FormerBLO.Get_Import_File_Name();
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName);
                }
            }
        }
        #endregion

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

