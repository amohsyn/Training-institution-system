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
using TrainingIS.Entities.Resources.SeancePlanningResources;
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
    public class BaseSeancePlanningsController : TrainingIS_BaseController
    {
        protected SeancePlanningBLO SeancePlanningBLO = null;

		public BaseSeancePlanningsController()
        {
            this.msgHelper = new MessagesService(typeof(SeancePlanning));
			this.SeancePlanningBLO = new SeancePlanningBLO(this._UnitOfWork, this.GAppContext) ;
        }

	    #region Pagination Methodes
		protected virtual List<Header_DataTable_GAppComponent> Get_GAppDataTable_Header_Text_And_Ids()
        {
            List<Header_DataTable_GAppComponent> herders = new List<Header_DataTable_GAppComponent>();

            foreach (PropertyInfo model_property in typeof(Default_SeancePlanning_Index_Model).GetProperties(typeof(GAppDataTableAttribute)))
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
					
			model_property = typeof(Default_SeancePlanning_Index_Model).GetProperty(nameof(Default_SeancePlanning_Index_Model.Schedule));
			FilterItem_GAppComponent FilterItem_Schedule = new FilterItem_GAppComponent();
			FilterItem_Schedule.Id = "Schedule.Id_Filter";
			FilterItem_Schedule.Label = model_property.getLocalName();
			FilterItem_Schedule.Placeholder = model_property.getLocalName();
			FilterItem_Schedule.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_Schedule = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_Schedule.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_Schedule != null)
            {
                FilterItem_Schedule.Selected = filter_info_Schedule.Value;
            }

			var All_Data_Schedule = new ScheduleBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_Schedule_msg = string.Format("tous les {0}",msg_SeancePlanning.PluralName.ToLower());
            All_Data_Schedule.Insert(0, new Schedule { Id = 0, ToStringValue = All_Schedule_msg });
            FilterItem_Schedule.Data = All_Data_Schedule.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_Schedule);

	    			
			model_property = typeof(Default_SeancePlanning_Index_Model).GetProperty(nameof(Default_SeancePlanning_Index_Model.Training));
			FilterItem_GAppComponent FilterItem_Training = new FilterItem_GAppComponent();
			FilterItem_Training.Id = "Training.Id_Filter";
			FilterItem_Training.Label = model_property.getLocalName();
			FilterItem_Training.Placeholder = model_property.getLocalName();
			FilterItem_Training.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_Training = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_Training.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_Training != null)
            {
                FilterItem_Training.Selected = filter_info_Training.Value;
            }

			var All_Data_Training = new TrainingBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_Training_msg = string.Format("tous les {0}",msg_SeancePlanning.PluralName.ToLower());
            All_Data_Training.Insert(0, new Training { Id = 0, ToStringValue = All_Training_msg });
            FilterItem_Training.Data = All_Data_Training.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_Training);

	    			
			model_property = typeof(Default_SeancePlanning_Index_Model).GetProperty(nameof(Default_SeancePlanning_Index_Model.SeanceDay));
			FilterItem_GAppComponent FilterItem_SeanceDay = new FilterItem_GAppComponent();
			FilterItem_SeanceDay.Id = "SeanceDay.Id_Filter";
			FilterItem_SeanceDay.Label = model_property.getLocalName();
			FilterItem_SeanceDay.Placeholder = model_property.getLocalName();
			FilterItem_SeanceDay.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_SeanceDay = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_SeanceDay.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_SeanceDay != null)
            {
                FilterItem_SeanceDay.Selected = filter_info_SeanceDay.Value;
            }

			var All_Data_SeanceDay = new SeanceDayBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_SeanceDay_msg = string.Format("tous les {0}",msg_SeancePlanning.PluralName.ToLower());
            All_Data_SeanceDay.Insert(0, new SeanceDay { Id = 0, ToStringValue = All_SeanceDay_msg });
            FilterItem_SeanceDay.Data = All_Data_SeanceDay.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_SeanceDay);

	    			
			model_property = typeof(Default_SeancePlanning_Index_Model).GetProperty(nameof(Default_SeancePlanning_Index_Model.SeanceNumber));
			FilterItem_GAppComponent FilterItem_SeanceNumber = new FilterItem_GAppComponent();
			FilterItem_SeanceNumber.Id = "SeanceNumber.Id_Filter";
			FilterItem_SeanceNumber.Label = model_property.getLocalName();
			FilterItem_SeanceNumber.Placeholder = model_property.getLocalName();
			FilterItem_SeanceNumber.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_SeanceNumber = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_SeanceNumber.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_SeanceNumber != null)
            {
                FilterItem_SeanceNumber.Selected = filter_info_SeanceNumber.Value;
            }

			var All_Data_SeanceNumber = new SeanceNumberBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_SeanceNumber_msg = string.Format("tous les {0}",msg_SeancePlanning.PluralName.ToLower());
            All_Data_SeanceNumber.Insert(0, new SeanceNumber { Id = 0, ToStringValue = All_SeanceNumber_msg });
            FilterItem_SeanceNumber.Data = All_Data_SeanceNumber.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_SeanceNumber);

	    			
			model_property = typeof(Default_SeancePlanning_Index_Model).GetProperty(nameof(Default_SeancePlanning_Index_Model.Classroom));
			FilterItem_GAppComponent FilterItem_Classroom = new FilterItem_GAppComponent();
			FilterItem_Classroom.Id = "Classroom.Id_Filter";
			FilterItem_Classroom.Label = model_property.getLocalName();
			FilterItem_Classroom.Placeholder = model_property.getLocalName();
			FilterItem_Classroom.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_Classroom = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_Classroom.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_Classroom != null)
            {
                FilterItem_Classroom.Selected = filter_info_Classroom.Value;
            }

			var All_Data_Classroom = new ClassroomBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_Classroom_msg = string.Format("tous les {0}",msg_SeancePlanning.PluralName.ToLower());
            All_Data_Classroom.Insert(0, new Classroom { Id = 0, ToStringValue = All_Classroom_msg });
            FilterItem_Classroom.Data = All_Data_Classroom.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_Classroom);

	    
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
            List<string> SearchCreteria = this.SeancePlanningBLO.GetSearchCreteria();

            List<Default_SeancePlanning_Index_Model> _ListDefault_SeancePlanning_Index_Model = null;
            try
            {
                filterRequestParams = this.Save_OR_Load_filterRequestParams_State(filterRequestParams);
               _ListDefault_SeancePlanning_Index_Model = new Default_SeancePlanning_Index_ModelBLM(this._UnitOfWork, this.GAppContext)
                   .Find(filterRequestParams, SearchCreteria, out _TotalRecords);

            }
            catch (Exception ex)
            {
                filterRequestParams = new FilterRequestParams();
				this.Delete_filterRequestParams_State();
                _ListDefault_SeancePlanning_Index_Model = new Default_SeancePlanning_Index_ModelBLM(this._UnitOfWork, this.GAppContext)
                  .Find(filterRequestParams, SearchCreteria, out _TotalRecords);
                Alert(ex.Message, NotificationType.warning);
            }

            Index_GAppPage index_page = new Index_GAppPage(filterRequestParams,this.Get_GAppDataTable_Header_Text_And_Ids(), _TotalRecords);
			index_page.Title = msg["Index_Title"];
			this.InitFilter(index_page, filterRequestParams.FilterBy, filterRequestParams.SearchBy);

            ViewBag.index_page = index_page;

            return View(_ListDefault_SeancePlanning_Index_Model);
        }


		protected virtual void Fill_ViewBag_Create(Default_SeancePlanning_Create_Model Default_SeancePlanning_Create_Model)
        {
			ViewBag.ClassroomId = new SelectList(new ClassroomBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_SeancePlanning_Create_Model.ClassroomId);
			ViewBag.ScheduleId = new SelectList(new ScheduleBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_SeancePlanning_Create_Model.ScheduleId);
			ViewBag.SeanceDayId = new SelectList(new SeanceDayBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_SeancePlanning_Create_Model.SeanceDayId);
			ViewBag.SeanceNumberId = new SelectList(new SeanceNumberBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_SeancePlanning_Create_Model.SeanceNumberId);
			ViewBag.TrainingId = new SelectList(new TrainingBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_SeancePlanning_Create_Model.TrainingId);



        }

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			Default_SeancePlanning_Create_Model default_seanceplanning_create_model = new Default_SeancePlanning_Create_ModelBLM(this._UnitOfWork, this.GAppContext) .CreateNew();
			this.Fill_ViewBag_Create(default_seanceplanning_create_model);
			return View(default_seanceplanning_create_model);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create(Default_SeancePlanning_Create_Model Default_SeancePlanning_Create_Model)
        {
			SeancePlanning SeancePlanning = null ;
			SeancePlanning = new Default_SeancePlanning_Create_ModelBLM(this._UnitOfWork, this.GAppContext) 
										.ConverTo_SeancePlanning(Default_SeancePlanning_Create_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    SeancePlanningBLO.Save(SeancePlanning);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_SeancePlanning.SingularName.ToLower(), SeancePlanning), NotificationType.success);
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
			this.Fill_ViewBag_Create(Default_SeancePlanning_Create_Model);
			Default_SeancePlanning_Create_Model = new Default_SeancePlanning_Create_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_SeancePlanning_Create_Model(SeancePlanning);
			return View(Default_SeancePlanning_Create_Model);
        }

		protected virtual void Fill_Edit_ViewBag(Default_SeancePlanning_Edit_Model Default_SeancePlanning_Edit_Model)
        {
			ViewBag.ClassroomId = new SelectList(new ClassroomBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_SeancePlanning_Edit_Model.ClassroomId);
			ViewBag.ScheduleId = new SelectList(new ScheduleBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_SeancePlanning_Edit_Model.ScheduleId);
			ViewBag.SeanceDayId = new SelectList(new SeanceDayBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_SeancePlanning_Edit_Model.SeanceDayId);
			ViewBag.SeanceNumberId = new SelectList(new SeanceNumberBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_SeancePlanning_Edit_Model.SeanceNumberId);
			ViewBag.TrainingId = new SelectList(new TrainingBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Default_SeancePlanning_Edit_Model.TrainingId);
 



        }
		 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeancePlanning SeancePlanning = SeancePlanningBLO.FindBaseEntityByID((long)id);
            if (SeancePlanning == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeancePlanning.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			Default_SeancePlanning_Edit_Model Default_SeancePlanning_Edit_Model = new Default_SeancePlanning_Edit_ModelBLM(this._UnitOfWork, this.GAppContext) 
                                                                .ConverTo_Default_SeancePlanning_Edit_Model(SeancePlanning) ;

			this.Fill_Edit_ViewBag(Default_SeancePlanning_Edit_Model);
			return View(Default_SeancePlanning_Edit_Model);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit(Default_SeancePlanning_Edit_Model Default_SeancePlanning_Edit_Model)	
        {
			if(Default_SeancePlanning_Edit_Model.Id == 0)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeancePlanning.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			SeancePlanning SeancePlanning = new Default_SeancePlanning_Edit_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_SeancePlanning( Default_SeancePlanning_Edit_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    SeancePlanningBLO.Save(SeancePlanning);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_SeancePlanning.SingularName.ToLower(), SeancePlanning), NotificationType.success);
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
			this.Fill_Edit_ViewBag(Default_SeancePlanning_Edit_Model);
			Default_SeancePlanning_Edit_Model = new Default_SeancePlanning_Edit_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Default_SeancePlanning_Edit_Model(SeancePlanning);
			return View(Default_SeancePlanning_Edit_Model);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeancePlanning SeancePlanning = SeancePlanningBLO.FindBaseEntityByID((long) id);
            if (SeancePlanning == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeancePlanning.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			Default_SeancePlanning_Details_Model Default_SeancePlanning_Details_Model = new Default_SeancePlanning_Details_Model();
		    Default_SeancePlanning_Details_Model = new Default_SeancePlanning_Details_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Default_SeancePlanning_Details_Model(SeancePlanning);


			return View(Default_SeancePlanning_Details_Model);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeancePlanning SeancePlanning = SeancePlanningBLO.FindBaseEntityByID((long)id);
            if (SeancePlanning == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeancePlanning.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_SeancePlanning_Details_Model Default_SeancePlanning_Details_Model = new Default_SeancePlanning_Details_ModelBLM(this._UnitOfWork, this.GAppContext) 
							.ConverTo_Default_SeancePlanning_Details_Model(SeancePlanning);


			 return View(Default_SeancePlanning_Details_Model);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			SeancePlanning SeancePlanning = SeancePlanningBLO.FindBaseEntityByID((long)id);
			if (SeancePlanning == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeancePlanning.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                SeancePlanningBLO.Delete(SeancePlanning);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, SeancePlanning.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_SeancePlanning.SingularName.ToLower(), SeancePlanning), NotificationType.success);
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
                ImportReport importReport = SeancePlanningBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
					Session["repport_name"] = msg_SeancePlanning.PluralName;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/SeancePlannings/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
        /// [Generalize] Export the filterted SeancePlannings
        /// </summary>
        /// <returns></returns>
        public virtual FileResult Export()
        {
            var dataTable = this.SeancePlanningBLO.Export(this.GetType().Name);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string FileName = SeancePlanningBLO.Get_Export_File_Name();
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName);
                }
            }
        }
        public virtual FileResult Import_File_Example()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dataTable = SeancePlanningBLO.Import_File_Example();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string FileName = this.SeancePlanningBLO.Get_Import_File_Name();
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName);
                }
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SeancePlanningBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class SeancePlanningsController : BaseSeancePlanningsController{};
}

