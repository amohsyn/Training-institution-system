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
using TrainingIS.Entities.Resources.SanctionResources;
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
    public class BaseSanctionsController : TrainingIS_BaseController
    {
        protected SanctionBLO SanctionBLO = null;

		public BaseSanctionsController()
        {
            this.msgHelper = new MessagesService(typeof(Sanction));
			this.SanctionBLO = new SanctionBLO(this._UnitOfWork, this.GAppContext) ;
        }

	    #region Pagination Methodes
		protected virtual List<Header_DataTable_GAppComponent> Get_GAppDataTable_Header_Text_And_Ids()
        {
            List<Header_DataTable_GAppComponent> herders = new List<Header_DataTable_GAppComponent>();

            foreach (PropertyInfo model_property in typeof(Sanction_Index_Model).GetProperties(typeof(GAppDataTableAttribute)))
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
					
			model_property = typeof(Sanction_Index_Model).GetProperty(nameof(Sanction_Index_Model.isLastSanction));
			FilterItem_GAppComponent FilterItem_isLastSanction = new FilterItem_GAppComponent();
			FilterItem_isLastSanction.Id = "isLastSanction_Filter";
			FilterItem_isLastSanction.Label = model_property.getLocalName();
			FilterItem_isLastSanction.Placeholder = model_property.getLocalName();
			FilterItem_isLastSanction.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Boolean;
			var filter_info_isLastSanction = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_isLastSanction.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_isLastSanction != null)
            {
                FilterItem_isLastSanction.Selected = filter_info_isLastSanction.Value;
            }

			index_page.Filter.FilterItems.Add(FilterItem_isLastSanction);

	    			
			model_property = typeof(Sanction_Index_Model).GetProperty(nameof(Sanction_Index_Model.isActif));
			FilterItem_GAppComponent FilterItem_isActif = new FilterItem_GAppComponent();
			FilterItem_isActif.Id = "Trainee.isActif_Filter";
			FilterItem_isActif.Label = model_property.getLocalName();
			FilterItem_isActif.Placeholder = model_property.getLocalName();
			FilterItem_isActif.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Enum;
			var filter_info_isActif = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_isActif.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_isActif != null)
            {
                FilterItem_isActif.Selected = filter_info_isActif.Value;
            }

            var All_Data_isActif = GAppEnumLocalization.Get_IntValue_And_LocalValue<IsActifEnum>();
            FilterItem_isActif.Data = All_Data_isActif.ToDictionary(entity => entity.Key.ToString(), entity => entity.Value);
            FilterItem_isActif.Data.Add("", string.Format("tous les {0}", typeof(IsActifEnum).GetProperty("isActif")));
			index_page.Filter.FilterItems.Add(FilterItem_isActif);

	    			
			model_property = typeof(Sanction_Index_Model).GetProperty(nameof(Sanction_Index_Model.Trainee));
			FilterItem_GAppComponent FilterItem_Trainee = new FilterItem_GAppComponent();
			FilterItem_Trainee.Id = "Trainee.Id_Filter";
			FilterItem_Trainee.Label = model_property.getLocalName();
			FilterItem_Trainee.Placeholder = model_property.getLocalName();
			FilterItem_Trainee.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_Trainee = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_Trainee.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_Trainee != null)
            {
                FilterItem_Trainee.Selected = filter_info_Trainee.Value;
            }

			var All_Data_Trainee = new TraineeBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_Trainee_msg = string.Format("tous les {0}",msg_Sanction.PluralName.ToLower());
            All_Data_Trainee.Insert(0, new Trainee { Id = 0, ToStringValue = All_Trainee_msg });
            FilterItem_Trainee.Data = All_Data_Trainee.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_Trainee);

	    			
			model_property = typeof(Sanction_Index_Model).GetProperty(nameof(Sanction_Index_Model.SanctionCategory));
			FilterItem_GAppComponent FilterItem_SanctionCategory = new FilterItem_GAppComponent();
			FilterItem_SanctionCategory.Id = "SanctionCategory.Id_Filter";
			FilterItem_SanctionCategory.Label = model_property.getLocalName();
			FilterItem_SanctionCategory.Placeholder = model_property.getLocalName();
			FilterItem_SanctionCategory.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_SanctionCategory = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_SanctionCategory.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_SanctionCategory != null)
            {
                FilterItem_SanctionCategory.Selected = filter_info_SanctionCategory.Value;
            }

			var All_Data_SanctionCategory = new SanctionCategoryBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_SanctionCategory_msg = string.Format("tous les {0}",msg_Sanction.PluralName.ToLower());
            All_Data_SanctionCategory.Insert(0, new SanctionCategory { Id = 0, ToStringValue = All_SanctionCategory_msg });
            FilterItem_SanctionCategory.Data = All_Data_SanctionCategory.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_SanctionCategory);

	    			
			model_property = typeof(Sanction_Index_Model).GetProperty(nameof(Sanction_Index_Model.SanctionState));
			FilterItem_GAppComponent FilterItem_SanctionState = new FilterItem_GAppComponent();
			FilterItem_SanctionState.Id = "SanctionState_Filter";
			FilterItem_SanctionState.Label = model_property.getLocalName();
			FilterItem_SanctionState.Placeholder = model_property.getLocalName();
			FilterItem_SanctionState.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Enum;
			var filter_info_SanctionState = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_SanctionState.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_SanctionState != null)
            {
                FilterItem_SanctionState.Selected = filter_info_SanctionState.Value;
            }

            var All_Data_SanctionState = GAppEnumLocalization.Get_IntValue_And_LocalValue<SanctionStates>();
            FilterItem_SanctionState.Data = All_Data_SanctionState.ToDictionary(entity => entity.Key.ToString(), entity => entity.Value);
            FilterItem_SanctionState.Data.Add("", string.Format("tous les {0}", typeof(SanctionStates).GetProperty("SanctionState")));
			index_page.Filter.FilterItems.Add(FilterItem_SanctionState);

	    			
			model_property = typeof(Sanction_Index_Model).GetProperty(nameof(Sanction_Index_Model.Meeting));
			FilterItem_GAppComponent FilterItem_Meeting = new FilterItem_GAppComponent();
			FilterItem_Meeting.Id = "Meeting.Id_Filter";
			FilterItem_Meeting.Label = model_property.getLocalName();
			FilterItem_Meeting.Placeholder = model_property.getLocalName();
			FilterItem_Meeting.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_Meeting = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_Meeting.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_Meeting != null)
            {
                FilterItem_Meeting.Selected = filter_info_Meeting.Value;
            }

			var All_Data_Meeting = new MeetingBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_Meeting_msg = string.Format("tous les {0}",msg_Sanction.PluralName.ToLower());
            All_Data_Meeting.Insert(0, new Meeting { Id = 0, ToStringValue = All_Meeting_msg });
            FilterItem_Meeting.Data = All_Data_Meeting.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_Meeting);

	    
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
            List<string> SearchCreteria = this.SanctionBLO.GetSearchCreteria();

            List<Sanction_Index_Model> _ListSanction_Index_Model = null;
            try
            {
                filterRequestParams = this.Save_OR_Load_filterRequestParams_State(filterRequestParams);
               _ListSanction_Index_Model = new Sanction_Index_ModelBLM(this._UnitOfWork, this.GAppContext)
                   .Find(filterRequestParams, SearchCreteria, out _TotalRecords);

            }
            catch (Exception ex)
            {
                filterRequestParams = new FilterRequestParams();
				this.Delete_filterRequestParams_State();
                _ListSanction_Index_Model = new Sanction_Index_ModelBLM(this._UnitOfWork, this.GAppContext)
                  .Find(filterRequestParams, SearchCreteria, out _TotalRecords);
                Alert(ex.Message, NotificationType.warning);
            }

            Index_GAppPage index_page = new Index_GAppPage(filterRequestParams,this.Get_GAppDataTable_Header_Text_And_Ids(), _TotalRecords);
			index_page.Title = msg["Index_Title"];
			this.InitFilter(index_page, filterRequestParams.FilterBy, filterRequestParams.SearchBy);

            ViewBag.index_page = index_page;

            return View(_ListSanction_Index_Model);
        }


		protected virtual void Fill_ViewBag_Create(Sanction_Create_Model Sanction_Create_Model)
        {
			ViewBag.MeetingId = new SelectList(new MeetingBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Sanction_Create_Model.MeetingId);
			ViewBag.SanctionCategoryId = new SelectList(new SanctionCategoryBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Sanction_Create_Model.SanctionCategoryId);
			ViewBag.TraineeId = new SelectList(new TraineeBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Sanction_Create_Model.TraineeId);



        }

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			Sanction_Create_Model sanction_create_model = new Sanction_Create_ModelBLM(this._UnitOfWork, this.GAppContext) .CreateNew();
			this.Fill_ViewBag_Create(sanction_create_model);
			return View(sanction_create_model);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create(Sanction_Create_Model Sanction_Create_Model)
        {
			Sanction Sanction = null ;
			Sanction = new Sanction_Create_ModelBLM(this._UnitOfWork, this.GAppContext) 
										.ConverTo_Sanction(Sanction_Create_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    SanctionBLO.Save(Sanction);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Sanction.SingularName.ToLower(), Sanction), NotificationType.success);
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
			this.Fill_ViewBag_Create(Sanction_Create_Model);
			Sanction_Create_Model = new Sanction_Create_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Sanction_Create_Model(Sanction);
			return View(Sanction_Create_Model);
        }

		protected virtual void Fill_Edit_ViewBag(Sanction_Edit_Model Sanction_Edit_Model)
        {
			ViewBag.MeetingId = new SelectList(new MeetingBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Sanction_Edit_Model.MeetingId);
			ViewBag.SanctionCategoryId = new SelectList(new SanctionCategoryBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Sanction_Edit_Model.SanctionCategoryId);
			ViewBag.TraineeId = new SelectList(new TraineeBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Sanction_Edit_Model.TraineeId);
 



        }
		 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sanction Sanction = SanctionBLO.FindBaseEntityByID((long)id);
            if (Sanction == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Sanction.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			Sanction_Edit_Model Sanction_Edit_Model = new Sanction_Edit_ModelBLM(this._UnitOfWork, this.GAppContext) 
                                                                .ConverTo_Sanction_Edit_Model(Sanction) ;

			this.Fill_Edit_ViewBag(Sanction_Edit_Model);
			return View(Sanction_Edit_Model);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit(Sanction_Edit_Model Sanction_Edit_Model)	
        {
			if(Sanction_Edit_Model.Id == 0)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Sanction.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Sanction Sanction = new Sanction_Edit_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Sanction( Sanction_Edit_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    SanctionBLO.Save(Sanction);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Sanction.SingularName.ToLower(), Sanction), NotificationType.success);
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
			this.Fill_Edit_ViewBag(Sanction_Edit_Model);
			Sanction_Edit_Model = new Sanction_Edit_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Sanction_Edit_Model(Sanction);
			return View(Sanction_Edit_Model);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sanction Sanction = SanctionBLO.FindBaseEntityByID((long) id);
            if (Sanction == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Sanction.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			Default_Sanction_Details_Model Default_Sanction_Details_Model = new Default_Sanction_Details_Model();
		    Default_Sanction_Details_Model = new Default_Sanction_Details_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Default_Sanction_Details_Model(Sanction);


			return View(Default_Sanction_Details_Model);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Sanction Sanction = SanctionBLO.FindBaseEntityByID((long)id);
            if (Sanction == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Sanction.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_Sanction_Details_Model Default_Sanction_Details_Model = new Default_Sanction_Details_ModelBLM(this._UnitOfWork, this.GAppContext) 
							.ConverTo_Default_Sanction_Details_Model(Sanction);


			 return View(Default_Sanction_Details_Model);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			Sanction Sanction = SanctionBLO.FindBaseEntityByID((long)id);
			if (Sanction == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_Sanction.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                SanctionBLO.Delete(Sanction);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, Sanction.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_Sanction.SingularName.ToLower(), Sanction), NotificationType.success);
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
                ImportReport importReport = SanctionBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
					Session["repport_name"] = msg_Sanction.PluralName;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/Sanctions/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
        /// [Generalize] Export the filterted Sanctions
        /// </summary>
        /// <returns></returns>
        public virtual FileResult Export()
        {
            var dataTable = this.SanctionBLO.Export(this.GetType().Name);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string FileName = SanctionBLO.Get_Export_File_Name();
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName);
                }
            }
        }
        public virtual FileResult Import_File_Example()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dataTable = SanctionBLO.Import_File_Example();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string FileName = this.SanctionBLO.Get_Import_File_Name();
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName);
                }
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SanctionBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class SanctionsController : BaseSanctionsController{};
}

