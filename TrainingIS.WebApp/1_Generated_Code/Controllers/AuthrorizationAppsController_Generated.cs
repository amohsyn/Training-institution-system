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
using GApp.Entities.Resources.AuthrorizationAppResources;
using TrainingIS.Entities.ModelsViews;
 
using System.Reflection;
using GApp.Models.DataAnnotations;
using GApp.Models.Pages;
using GApp.Models.GAppComponents;
namespace TrainingIS.WebApp.Controllers
{  
    // Generated by GApp v 0.3.0 
    public class BaseAuthrorizationAppsController : TrainingIS_BaseController
    {
        protected AuthrorizationAppBLO AuthrorizationAppBLO = null;

		public BaseAuthrorizationAppsController()
        {
            this.msgHelper = new MessagesService(typeof(AuthrorizationApp));
			this.AuthrorizationAppBLO = new AuthrorizationAppBLO(this._UnitOfWork, this.GAppContext) ;
        }

	    #region Pagination Methodes
		protected virtual Dictionary<string, string> Get_GAppDataTable_Header_Text_And_Ids()
        {
            Dictionary<string, string> headerTextAndIDs = new Dictionary<string, string>();
            foreach (PropertyInfo model_property in typeof(Default_Details_AuthrorizationApp_Model).GetProperties(typeof(GAppDataTableAttribute)))
            {
                GAppDataTableAttribute gappDataTableAttribute = model_property.GetCustomAttribute(typeof(GAppDataTableAttribute)) as GAppDataTableAttribute;
                string OrderBy = string.IsNullOrEmpty(gappDataTableAttribute.OrderBy) ? model_property.Name : gappDataTableAttribute.OrderBy;
                headerTextAndIDs.Add(OrderBy, model_property.getLocalName());
            }
            return headerTextAndIDs;
        }
        protected virtual List<string> GetSearchCreteria()
        {
            List<string> SearchCreteria = new List<string>();
            foreach (PropertyInfo model_property in typeof(Default_Details_AuthrorizationApp_Model).GetProperties(typeof(GAppDataTableAttribute)))
            {
                GAppDataTableAttribute gappDataTableAttribute = model_property.GetCustomAttribute(typeof(GAppDataTableAttribute)) as GAppDataTableAttribute;
                string SearchBy = string.IsNullOrEmpty(gappDataTableAttribute.SearchBy) ? model_property.Name : gappDataTableAttribute.SearchBy;
                SearchCreteria.Add(gappDataTableAttribute.SearchBy);
            }
            foreach (PropertyInfo model_property in typeof(Default_Details_AuthrorizationApp_Model).GetProperties(typeof(SearchByAttribute)))
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
					
			model_property = typeof(Default_Details_AuthrorizationApp_Model).GetProperty(nameof(Default_Details_AuthrorizationApp_Model.RoleApp));
			FilterItem_GAppComponent FilterItem_RoleApp = new FilterItem_GAppComponent();
			FilterItem_RoleApp.Id = "RoleApp.Id_Filter";
			FilterItem_RoleApp.Label = model_property.getLocalName();
			FilterItem_RoleApp.Placeholder = model_property.getLocalName();
			FilterItem_RoleApp.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_RoleApp = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_RoleApp.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_RoleApp != null)
            {
                FilterItem_RoleApp.Selected = filter_info_RoleApp.Value;
            }

			var All_Data_RoleApp = new RoleAppBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_RoleApp_msg = string.Format("tous les {0}",msg_AuthrorizationApp.PluralName.ToLower());
            All_Data_RoleApp.Insert(0, new RoleApp { Id = 0, ToStringValue = All_RoleApp_msg });
            FilterItem_RoleApp.Data = All_Data_RoleApp.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_RoleApp);

	    			
			model_property = typeof(Default_Details_AuthrorizationApp_Model).GetProperty(nameof(Default_Details_AuthrorizationApp_Model.ControllerApp));
			FilterItem_GAppComponent FilterItem_ControllerApp = new FilterItem_GAppComponent();
			FilterItem_ControllerApp.Id = "ControllerApp.Id_Filter";
			FilterItem_ControllerApp.Label = model_property.getLocalName();
			FilterItem_ControllerApp.Placeholder = model_property.getLocalName();
			FilterItem_ControllerApp.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_ControllerApp = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_ControllerApp.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_ControllerApp != null)
            {
                FilterItem_ControllerApp.Selected = filter_info_ControllerApp.Value;
            }

			var All_Data_ControllerApp = new ControllerAppBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_ControllerApp_msg = string.Format("tous les {0}",msg_AuthrorizationApp.PluralName.ToLower());
            All_Data_ControllerApp.Insert(0, new ControllerApp { Id = 0, ToStringValue = All_ControllerApp_msg });
            FilterItem_ControllerApp.Data = All_Data_ControllerApp.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_ControllerApp);

	    
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
		    filterRequestParams = this.Save_OR_Load_filterRequestParams_State(filterRequestParams);
            msgHelper.Index(msg);
 
            Int32 _TotalRecords = 0;
            List<string> SearchCreteria = this.GetSearchCreteria();
            List<Default_Details_AuthrorizationApp_Model> _ListDefault_Details_AuthrorizationApp_Model = new Default_Details_AuthrorizationApp_ModelBLM(this._UnitOfWork, this.GAppContext)
               .Find(filterRequestParams.OrderBy, filterRequestParams.FilterBy, filterRequestParams.SearchBy, SearchCreteria, filterRequestParams.currentPage, filterRequestParams.pageSize, out _TotalRecords);

            Index_GAppPage index_page = new Index_GAppPage(this.Get_GAppDataTable_Header_Text_And_Ids(), _TotalRecords, filterRequestParams.OrderBy, filterRequestParams.SearchBy, filterRequestParams.currentPage, filterRequestParams.pageSize);
            index_page.Title = msg["Index_Title"];
			this.InitFilter(index_page, filterRequestParams.FilterBy, filterRequestParams.SearchBy);

            ViewBag.index_page = index_page;

            return View(_ListDefault_Details_AuthrorizationApp_Model);
        }


		protected void Fill_ViewBag_Create(Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model)
        {
		ViewBag.ControllerAppId = new SelectList(new ControllerAppBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_AuthrorizationApp_Model.ControllerAppId);
		ViewBag.RoleAppId = new SelectList(new RoleAppBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_AuthrorizationApp_Model.RoleAppId);


			// SelectFilters 
			ViewBag.Data_ActionControllerApps = new ActionControllerAppBLO(this._UnitOfWork, this.GAppContext) .FindAll().ToList<BaseEntity>();

        }

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			Default_Form_AuthrorizationApp_Model default_form_authrorizationapp_model = new Default_Form_AuthrorizationApp_ModelBLM(this._UnitOfWork, this.GAppContext) .CreateNew();
			this.Fill_ViewBag_Create(default_form_authrorizationapp_model);
			return View(default_form_authrorizationapp_model);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create(Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model)
        {
			AuthrorizationApp AuthrorizationApp = null ;
			AuthrorizationApp = new Default_Form_AuthrorizationApp_ModelBLM(this._UnitOfWork, this.GAppContext) 
										.ConverTo_AuthrorizationApp(Default_Form_AuthrorizationApp_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    AuthrorizationAppBLO.Save(AuthrorizationApp);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_AuthrorizationApp.SingularName.ToLower(), AuthrorizationApp), NotificationType.success);
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
			this.Fill_ViewBag_Create(Default_Form_AuthrorizationApp_Model);
			Default_Form_AuthrorizationApp_Model = new Default_Form_AuthrorizationApp_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_Form_AuthrorizationApp_Model(AuthrorizationApp);
			return View(Default_Form_AuthrorizationApp_Model);
        }

		protected void Fill_Edit_ViewBag(Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model)
        {
			ViewBag.ControllerAppId = new SelectList(new ControllerAppBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_AuthrorizationApp_Model.ControllerAppId);
			ViewBag.RoleAppId = new SelectList(new RoleAppBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_Form_AuthrorizationApp_Model.RoleAppId);
 


			// SelectFilters 
			ViewBag.Data_ActionControllerApps = new ActionControllerAppBLO(this._UnitOfWork, this.GAppContext) .FindAll().ToList<BaseEntity>();

        }
		 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AuthrorizationApp AuthrorizationApp = AuthrorizationAppBLO.FindBaseEntityByID((long)id);
            if (AuthrorizationApp == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_AuthrorizationApp.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model = new Default_Form_AuthrorizationApp_ModelBLM(this._UnitOfWork, this.GAppContext) 
                                                                .ConverTo_Default_Form_AuthrorizationApp_Model(AuthrorizationApp) ;

			this.Fill_Edit_ViewBag(Default_Form_AuthrorizationApp_Model);
			return View(Default_Form_AuthrorizationApp_Model);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit(Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model)	
        {
			AuthrorizationApp AuthrorizationApp = new Default_Form_AuthrorizationApp_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_AuthrorizationApp( Default_Form_AuthrorizationApp_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    AuthrorizationAppBLO.Save(AuthrorizationApp);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_AuthrorizationApp.SingularName.ToLower(), AuthrorizationApp), NotificationType.success);
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
			this.Fill_Edit_ViewBag(Default_Form_AuthrorizationApp_Model);
			Default_Form_AuthrorizationApp_Model = new Default_Form_AuthrorizationApp_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_Form_AuthrorizationApp_Model(AuthrorizationApp);
			return View(Default_Form_AuthrorizationApp_Model);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuthrorizationApp AuthrorizationApp = AuthrorizationAppBLO.FindBaseEntityByID((long) id);
            if (AuthrorizationApp == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_AuthrorizationApp.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			Default_Details_AuthrorizationApp_Model Default_Details_AuthrorizationApp_Model = new Default_Details_AuthrorizationApp_Model();
		    Default_Details_AuthrorizationApp_Model = new Default_Details_AuthrorizationApp_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Default_Details_AuthrorizationApp_Model(AuthrorizationApp);


			return View(Default_Details_AuthrorizationApp_Model);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AuthrorizationApp AuthrorizationApp = AuthrorizationAppBLO.FindBaseEntityByID((long)id);
            if (AuthrorizationApp == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_AuthrorizationApp.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_Details_AuthrorizationApp_Model Default_Details_AuthrorizationApp_Model = new Default_Details_AuthrorizationApp_ModelBLM(this._UnitOfWork, this.GAppContext) 
							.ConverTo_Default_Details_AuthrorizationApp_Model(AuthrorizationApp);


			 return View(Default_Details_AuthrorizationApp_Model);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			AuthrorizationApp AuthrorizationApp = AuthrorizationAppBLO.FindBaseEntityByID((long)id);
			if (AuthrorizationApp == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_AuthrorizationApp.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                AuthrorizationAppBLO.Delete(AuthrorizationApp);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, AuthrorizationApp.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_AuthrorizationApp.SingularName.ToLower(), AuthrorizationApp), NotificationType.success);
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
                ImportReport importReport = AuthrorizationAppBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
					Session["repport_name"] = msg_AuthrorizationApp.PluralName;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/AuthrorizationApps/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
                DataTable dataTable = AuthrorizationAppBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
					string FileName = string.Format("{0}-{1}", msg_AuthrorizationApp.PluralName, DateTime.Now.ToShortDateString());
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
                AuthrorizationAppBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class AuthrorizationAppsController : BaseAuthrorizationAppsController{};
}

