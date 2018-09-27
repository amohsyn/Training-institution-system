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
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Models.SeanceTrainings;
using TrainingIS.Entities.ModelsViews;
 
using System.Reflection;
using GApp.Models.DataAnnotations;
using GApp.Models.Pages;
using GApp.Models.GAppComponents;
namespace TrainingIS.WebApp.Controllers
{  
    // Generated by GApp v 0.3.0 
    public class BaseSeanceTrainingsController : TrainingIS_BaseController
    {
        protected SeanceTrainingBLO SeanceTrainingBLO = null;

		public BaseSeanceTrainingsController()
        {
            this.msgHelper = new MessagesService(typeof(SeanceTraining));
			this.SeanceTrainingBLO = new SeanceTrainingBLO(this._UnitOfWork, this.GAppContext) ;
        }

	    #region Pagination Methodes
		protected virtual List<Header_DataTable_GAppComponent> Get_GAppDataTable_Header_Text_And_Ids()
        {
            List<Header_DataTable_GAppComponent> herders = new List<Header_DataTable_GAppComponent>();

            foreach (PropertyInfo model_property in typeof(Index_SeanceTraining_Model).GetProperties(typeof(GAppDataTableAttribute)))
            {
                GAppDataTableAttribute gappDataTableAttribute = model_property.GetCustomAttribute(typeof(GAppDataTableAttribute)) as GAppDataTableAttribute;
                string OrderBy = string.IsNullOrEmpty(gappDataTableAttribute.OrderBy) ? model_property.Name : gappDataTableAttribute.OrderBy;

                Header_DataTable_GAppComponent header = new Header_DataTable_GAppComponent();
                header.Id = OrderBy;
                header.Name = model_property.getLocalName();
                header.ShortName = model_property.getLocalShortName();
                herders.Add(header);
            }
            return herders;
        }

        protected virtual List<string> GetSearchCreteria()
        {
            List<string> SearchCreteria = new List<string>();
            foreach (PropertyInfo model_property in typeof(Index_SeanceTraining_Model).GetProperties(typeof(GAppDataTableAttribute)))
            {
                GAppDataTableAttribute gappDataTableAttribute = model_property.GetCustomAttribute(typeof(GAppDataTableAttribute)) as GAppDataTableAttribute;
                string SearchBy = string.IsNullOrEmpty(gappDataTableAttribute.SearchBy) ? model_property.Name : gappDataTableAttribute.SearchBy;
                SearchCreteria.Add(gappDataTableAttribute.SearchBy);
            }
            foreach (PropertyInfo model_property in typeof(Index_SeanceTraining_Model).GetProperties(typeof(SearchByAttribute)))
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
					
			model_property = typeof(Index_SeanceTraining_Model).GetProperty(nameof(Index_SeanceTraining_Model.Group));
			FilterItem_GAppComponent FilterItem_Group = new FilterItem_GAppComponent();
			FilterItem_Group.Id = "SeancePlanning.Training.Group.Id_Filter";
			FilterItem_Group.Label = model_property.getLocalName();
			FilterItem_Group.Placeholder = model_property.getLocalName();
			FilterItem_Group.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_Group = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_Group.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_Group != null)
            {
                FilterItem_Group.Selected = filter_info_Group.Value;
            }

			var All_Data_Group = new GroupBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_Group_msg = string.Format("tous les {0}",msg_SeanceTraining.PluralName.ToLower());
            All_Data_Group.Insert(0, new Group { Id = 0, ToStringValue = All_Group_msg });
            FilterItem_Group.Data = All_Data_Group.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_Group);

	    			
			model_property = typeof(Index_SeanceTraining_Model).GetProperty(nameof(Index_SeanceTraining_Model.ModuleTraining));
			FilterItem_GAppComponent FilterItem_ModuleTraining = new FilterItem_GAppComponent();
			FilterItem_ModuleTraining.Id = "SeancePlanning.Training.ModuleTraining.Id_Filter";
			FilterItem_ModuleTraining.Label = model_property.getLocalName();
			FilterItem_ModuleTraining.Placeholder = model_property.getLocalName();
			FilterItem_ModuleTraining.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Select;
			var filter_info_ModuleTraining = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_ModuleTraining.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_ModuleTraining != null)
            {
                FilterItem_ModuleTraining.Selected = filter_info_ModuleTraining.Value;
            }

			var All_Data_ModuleTraining = new ModuleTrainingBLO(this._UnitOfWork, this.GAppContext).FindAll();
			string All_ModuleTraining_msg = string.Format("tous les {0}",msg_SeanceTraining.PluralName.ToLower());
            All_Data_ModuleTraining.Insert(0, new ModuleTraining { Id = 0, ToStringValue = All_ModuleTraining_msg });
            FilterItem_ModuleTraining.Data = All_Data_ModuleTraining.ToDictionary(entity => entity.Id.ToString(), entity => entity.ToStringValue);
			index_page.Filter.FilterItems.Add(FilterItem_ModuleTraining);

	    			
			model_property = typeof(Index_SeanceTraining_Model).GetProperty(nameof(Index_SeanceTraining_Model.FormerValidation));
			FilterItem_GAppComponent FilterItem_FormerValidation = new FilterItem_GAppComponent();
			FilterItem_FormerValidation.Id = "FormerValidation_Filter";
			FilterItem_FormerValidation.Label = model_property.getLocalName();
			FilterItem_FormerValidation.Placeholder = model_property.getLocalName();
			FilterItem_FormerValidation.FilterItem_Category = FilterItem_GAppComponent.FilterItem_Categories.Text;
			var filter_info_FormerValidation = filters_by_infos
                .Where(f => f.PropertyName == FilterItem_FormerValidation.Id.RemoveFromEnd("_Filter"))
                .FirstOrDefault();
            if(filter_info_FormerValidation != null)
            {
                FilterItem_FormerValidation.Selected = filter_info_FormerValidation.Value;
            }

			index_page.Filter.FilterItems.Add(FilterItem_FormerValidation);

	    
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
            List<Index_SeanceTraining_Model> _ListIndex_SeanceTraining_Model = new Index_SeanceTraining_ModelBLM(this._UnitOfWork, this.GAppContext)
               .Find(filterRequestParams.OrderBy, filterRequestParams.FilterBy, filterRequestParams.SearchBy, SearchCreteria, filterRequestParams.currentPage, filterRequestParams.pageSize, out _TotalRecords);

            Index_GAppPage index_page = new Index_GAppPage(this.Get_GAppDataTable_Header_Text_And_Ids(), _TotalRecords, filterRequestParams.OrderBy, filterRequestParams.SearchBy, filterRequestParams.currentPage, filterRequestParams.pageSize);
            index_page.Title = msg["Index_Title"];
			this.InitFilter(index_page, filterRequestParams.FilterBy, filterRequestParams.SearchBy);

            ViewBag.index_page = index_page;

            return View(_ListIndex_SeanceTraining_Model);
        }


		protected void Fill_ViewBag_Create(Create_SeanceTraining_Model Create_SeanceTraining_Model)
        {
		ViewBag.SeancePlanningId = new SelectList(new SeancePlanningBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Create_SeanceTraining_Model.SeancePlanningId);



        }

		public virtual ActionResult Create()
        {
			msgHelper.Create(msg);		
			Create_SeanceTraining_Model create_seancetraining_model = new Create_SeanceTraining_ModelBLM(this._UnitOfWork, this.GAppContext) .CreateNew();
			this.Fill_ViewBag_Create(create_seancetraining_model);
			return View(create_seancetraining_model);
        } 

		[HttpPost] 
        [ValidateAntiForgeryToken]
		public virtual ActionResult Create(Create_SeanceTraining_Model Create_SeanceTraining_Model)
        {
			SeanceTraining SeanceTraining = null ;
			SeanceTraining = new Create_SeanceTraining_ModelBLM(this._UnitOfWork, this.GAppContext) 
										.ConverTo_SeanceTraining(Create_SeanceTraining_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				
				try
                {
                    SeanceTrainingBLO.Save(SeanceTraining);
					Alert(string.Format(msgManager.The_Entity_was_well_created, msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_SeanceTraining.SingularName.ToLower(), SeanceTraining), NotificationType.success);
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
			this.Fill_ViewBag_Create(Create_SeanceTraining_Model);
			Create_SeanceTraining_Model = new Create_SeanceTraining_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Create_SeanceTraining_Model(SeanceTraining);
			return View(Create_SeanceTraining_Model);
        }

		protected void Fill_Edit_ViewBag(Create_SeanceTraining_Model Create_SeanceTraining_Model)
        {
			ViewBag.SeancePlanningId = new SelectList(new SeancePlanningBLO(this._UnitOfWork, this.GAppContext) .FindAll(), "Id", nameof(TrainingIS_BaseEntity.ToStringValue), Create_SeanceTraining_Model.SeancePlanningId);
 



        }
		 
		public virtual ActionResult Edit(long? id)
        {
			bool dataBaseException = false;
			msgHelper.Edit(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeanceTraining SeanceTraining = SeanceTrainingBLO.FindBaseEntityByID((long)id);
            if (SeanceTraining == null)
            {
                string msg = string.Format(msgManager.You_try_to_edit_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeanceTraining.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }			 
			Create_SeanceTraining_Model Create_SeanceTraining_Model = new Create_SeanceTraining_ModelBLM(this._UnitOfWork, this.GAppContext) 
                                                                .ConverTo_Create_SeanceTraining_Model(SeanceTraining) ;

			this.Fill_Edit_ViewBag(Create_SeanceTraining_Model);
			return View(Create_SeanceTraining_Model);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public virtual ActionResult Edit(Create_SeanceTraining_Model Create_SeanceTraining_Model)	
        {
			SeanceTraining SeanceTraining = new Create_SeanceTraining_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_SeanceTraining( Create_SeanceTraining_Model);

			bool dataBaseException = false;
            if (ModelState.IsValid)
            {
				

				try
                {
                    SeanceTrainingBLO.Save(SeanceTraining);
					Alert(string.Format(msgManager.The_entity_has_been_changed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_SeanceTraining.SingularName.ToLower(), SeanceTraining), NotificationType.success);
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
			this.Fill_Edit_ViewBag(Create_SeanceTraining_Model);
			Create_SeanceTraining_Model = new Create_SeanceTraining_ModelBLM(this._UnitOfWork, this.GAppContext).ConverTo_Create_SeanceTraining_Model(SeanceTraining);
			return View(Create_SeanceTraining_Model);
        }

		public virtual ActionResult Details(long? id)
        {
		    msgHelper.Details(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeanceTraining SeanceTraining = SeanceTrainingBLO.FindBaseEntityByID((long) id);
            if (SeanceTraining == null)
            {
                string msg = string.Format(msgManager.You_try_to_show_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeanceTraining.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
			Default_Details_SeanceTraining_Model Default_Details_SeanceTraining_Model = new Default_Details_SeanceTraining_Model();
		    Default_Details_SeanceTraining_Model = new Default_Details_SeanceTraining_ModelBLM(this._UnitOfWork, this.GAppContext) 
                .ConverTo_Default_Details_SeanceTraining_Model(SeanceTraining);


			return View(Default_Details_SeanceTraining_Model);
        } 

		 public virtual ActionResult Delete(long? id)
        {
			msgHelper.Delete(msg);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeanceTraining SeanceTraining = SeanceTrainingBLO.FindBaseEntityByID((long)id);
            if (SeanceTraining == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeanceTraining.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }

			Default_Details_SeanceTraining_Model Default_Details_SeanceTraining_Model = new Default_Details_SeanceTraining_ModelBLM(this._UnitOfWork, this.GAppContext) 
							.ConverTo_Default_Details_SeanceTraining_Model(SeanceTraining);


			 return View(Default_Details_SeanceTraining_Model);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(long id)
        {
			SeanceTraining SeanceTraining = SeanceTrainingBLO.FindBaseEntityByID((long)id);
			if (SeanceTraining == null)
            {
			    string msg = string.Format(msgManager.You_try_to_delete_that_does_not_exist, msgHelper.UndefindedArticle(), msg_SeanceTraining.SingularName.ToLower());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            
			try
            {
                SeanceTrainingBLO.Delete(SeanceTraining);
            }
            catch (GAppDbUpdate_ForeignKeyViolation_Exception ex)
            {
                string msg = string.Format(msgManager.You_can_not_delete_this_entity_because_it_is_already_related_to_another_object, SeanceTraining.ToString());
                Alert(msg, NotificationType.error);
                return RedirectToAction("Index");
            }
            catch (GAppDbException ex)
            {
                Alert(ex.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

			Alert(string.Format(msgManager.The_entity_has_been_removed,msgHelper.DefinitArticle().FirstLetterToUpperCase(), msg_SeanceTraining.SingularName.ToLower(), SeanceTraining), NotificationType.success);
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
                ImportReport importReport = SeanceTrainingBLO.Import(firstTable, FileName);

                // Save ExcelRepport file to Server
                DataSet DataSet_report = importReport.get_DataSet_Report();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(DataSet_report);
                    string path_repport = ControllerContext.HttpContext.Server.MapPath("~/Content/Files/" + "Repport_" + FileName + ".xlsx");
                    Session["path_repport"] = path_repport;
					Session["repport_name"] = msg_SeanceTraining.PluralName;
                    wb.SaveAs(path_repport);

                    // Add DownLoad Link to Repport
                    string a_download = "<a href=\"/SeanceTrainings/LastRepportFile\">Télécharger le rapport d'importation</a>";
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
                DataTable dataTable = SeanceTrainingBLO.Export();
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
					string FileName = string.Format("{0}-{1}", msg_SeanceTraining.PluralName, DateTime.Now.ToShortDateString());
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
                SeanceTrainingBLO.Dispose();
            }
            base.Dispose(disposing);
        }
    }

	public partial class SeanceTrainingsController : BaseSeanceTrainingsController{};
}

