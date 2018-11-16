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
using GApp.Entities.Resources.ApplicationParamResources;
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
    public class BaseApplicationParamsController : TrainingIS_BaseController
    {
        protected ApplicationParamBLO ApplicationParamBLO = null;

		public BaseApplicationParamsController()
        {
            this.msgHelper = new MessagesService(typeof(ApplicationParam));
			this.ApplicationParamBLO = new ApplicationParamBLO(this._UnitOfWork, this.GAppContext) ;
        }

	    #region Pagination Methodes
		protected virtual List<Header_DataTable_GAppComponent> Get_GAppDataTable_Header_Text_And_Ids()
        {
            List<Header_DataTable_GAppComponent> herders = new List<Header_DataTable_GAppComponent>();

            foreach (PropertyInfo model_property in typeof(Default_ApplicationParam_Index_Model).GetProperties(typeof(GAppDataTableAttribute)))
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
					
			model_property = typeof(Default_ApplicationParam_Index_Model).GetProperty(nameof(Default_ApplicationParam_Index_Model.Name));
			FilterItem_GAppComponent FilterItem_Name = new FilterItem_GAppComponent();
			FilterItem_Name.Id = "Name_Filter";
			FilterItem_Name.Label = model_property.getLocalName();
			FilterItem_Name.Placeholder = model_property.getLocalName();
			FilterItem_Name.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Text;
			var filter_info_Name = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_Name.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_Name != null)
            {
                FilterItem_Name.Selected = filter_info_Name.Value;
            }

			index_page.Filter.FilterItems.Add(FilterItem_Name);

	    			
			model_property = typeof(Default_ApplicationParam_Index_Model).GetProperty(nameof(Default_ApplicationParam_Index_Model.Value));
			FilterItem_GAppComponent FilterItem_Value = new FilterItem_GAppComponent();
			FilterItem_Value.Id = "Value_Filter";
			FilterItem_Value.Label = model_property.getLocalName();
			FilterItem_Value.Placeholder = model_property.getLocalName();
			FilterItem_Value.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Text;
			var filter_info_Value = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_Value.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_Value != null)
            {
                FilterItem_Value.Selected = filter_info_Value.Value;
            }

			index_page.Filter.FilterItems.Add(FilterItem_Value);

	    
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
            List<string> SearchCreteria = this.ApplicationParamBLO.GetSearchCreteria();

            List<Default_ApplicationParam_Index_Model> _ListDefault_ApplicationParam_Index_Model = null;
            try
            {
                filterRequestParams = this.Save_OR_Load_filterRequestParams_State(filterRequestParams);
               _ListDefault_ApplicationParam_Index_Model = new Default_ApplicationParam_Index_ModelBLM(this._UnitOfWork, this.GAppContext)
                   .Find(filterRequestParams, SearchCreteria, out _TotalRecords);

            }
            catch (Exception ex)
            {
                filterRequestParams = new FilterRequestParams();
				this.Delete_filterRequestParams_State();
                _ListDefault_ApplicationParam_Index_Model = new Default_ApplicationParam_Index_ModelBLM(this._UnitOfWork, this.GAppContext)
                  .Find(filterRequestParams, SearchCreteria, out _TotalRecords);
                Alert(ex.Message, NotificationType.warning);
            }

            Index_GAppPage index_page = new Index_GAppPage(filterRequestParams,this.Get_GAppDataTable_Header_Text_And_Ids(), _TotalRecords);
			index_page.Title = msg["Index_Title"];
			this.InitFilter(index_page, filterRequestParams.FilterBy, filterRequestParams.SearchBy);

            ViewBag.index_page = index_page;

            return View(_ListDefault_ApplicationParam_Index_Model);
        }


		protected virtual void Fill_ViewBag_Create(Default_ApplicationParam_Create_Model Default_ApplicationParam_Create_Model)
        {



        }

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			Default_ApplicationParam_Create_Model default_applicationparam_create_model = new Default_ApplicationParam_Create_ModelBLM(this._UnitOfWork, this.GAppContext) .CreateNew();
			this.Fill_ViewBag_Create(default_applicationparam_create_model);
			return View(default_applicationparam_create_model);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create(Default_ApplicationParam_Create_Model Default_ApplicationParam_Create_Model)
        {
			ApplicationParam ApplicationParam = null ;
			ApplicationParam = new Default_ApplicationParam_Create_ModelBLM(this._UnitOfWork, this.GAppContext) 
										.ConverTo_ApplicationParam(Default_ApplicationParam_Create_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    ApplicationParamBLO.Save(ApplicationParam);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_ApplicationParam.SingularName.ToLower(), ApplicationParam), NotificationType.success);
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
			this.Fill_ViewBag_Create(Default_ApplicationParam_Create_Model);
			Default_ApplicationParam_Create_Model = new Default_ApplicationParam_Create_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_ApplicationParam_Create_Model(ApplicationParam);
			return View(Default_ApplicationParam_Create_Model);
        }

		protected virtual void Fill_Edit_ViewBag(Default_ApplicationParam_Edit_Model Default_ApplicationParam_Edit_Model)
        {
 



        }
		 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationParam ApplicationParam = ApplicationParamBLO.FindBaseEntityByID((long)id);
            if (ApplicationParam == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_ApplicationParam.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			Default_ApplicationParam_Edit_Model Default_ApplicationParam_Edit_Model = new Default_ApplicationParam_Edit_ModelBLM(this._UnitOfWork, this.GAppContext) 
                                                                .ConverTo_Default_ApplicationParam_Edit_Model(ApplicationParam) ;

			this.Fill_Edit_ViewBag(Default_ApplicationParam_Edit_Model);
			return View(Default_ApplicationParam_Edit_Model);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit(Default_ApplicationParam_Edit_Model Default_ApplicationParam_Edit_Model)	
        {
			ApplicationParam ApplicationParam = new Default_ApplicationParam_Edit_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_ApplicationParam( Default_ApplicationParam_Edit_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    ApplicationParamBLO.Save(ApplicationParam);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_ApplicationParam.SingularName.ToLower(), ApplicationParam), NotificationType.success);
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
			this.Fill_Edit_ViewBag(Default_ApplicationParam_Edit_Model);
			Default_ApplicationParam_Edit_Model = new Default_ApplicationParam_Edit_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_ApplicationParam_Edit_Model(ApplicationParam);
			return View(Default_ApplicationParam_Edit_Model);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationParam ApplicationParam = ApplicationParamBLO.FindBaseEntityByID((long) id);
            if (ApplicationParam == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_ApplicationParam.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			Default_ApplicationParam_Details_Model Default_ApplicationParam_Details_Model = new Default_ApplicationParam_Details_Model();
		    Default_ApplicationParam_Details_Model = new Default_ApplicationParam_Details_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Default_ApplicationParam_Details_Model(ApplicationParam);


			return View(Default_ApplicationParam_Details_Model);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationParam ApplicationParam = ApplicationParamBLO.FindBaseEntityByID((long)id);
            if (ApplicationParam == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_ApplicationParam.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_ApplicationParam_Details_Model Default_ApplicationParam_Details_Model = new Default_ApplicationParam_Details_ModelBLM(this._UnitOfWork, this.GAppContext) 
							.ConverTo_Default_ApplicationParam_Details_Model(ApplicationParam);


			 return View(Default_ApplicationParam_Details_Model);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			ApplicationParam ApplicationParam = ApplicationParamBLO.FindBaseEntityByID((long)id);
			if (ApplicationParam == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_ApplicationParam.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                ApplicationParamBLO.Delete(ApplicationParam);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, ApplicationParam.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_ApplicationParam.SingularName.ToLower(), ApplicationParam), NotificationType.success);
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
                ImportReport importReport = ApplicationParamBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
					Session["repport_name"] = msg_ApplicationParam.PluralName;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/ApplicationParams/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
        /// [Generalize] Export the filterted ApplicationParams
        /// </summary>
        /// <returns></returns>
        public virtual FileResult Export()
        {
            var dataTable = this.ApplicationParamBLO.Export(this.GetType().Name);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string FileName = string.Format("{0}-{1}", msg_ApplicationParam.PluralName, DateTime.Now.ToShortDateString());
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName + ".xlsx");
                }
            }
        }
        public virtual FileResult Import_File_Example()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dataTable = ApplicationParamBLO.Import_File_Example();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string FileName = string.Format("{0}-{1}", msg_ApplicationParam.PluralName, DateTime.Now.ToShortDateString());
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName + ".xlsx");
                }
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ApplicationParamBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class ApplicationParamsController : BaseApplicationParamsController{};
}

