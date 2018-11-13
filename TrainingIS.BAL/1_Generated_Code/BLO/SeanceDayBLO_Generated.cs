﻿using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Data.Entity;
using GApp.BLL;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.Entities;
using System.Data.Entity.Core.Metadata.Edm;
using TrainingIS.DAL;
using TrainingIS.BLL.Services.Import;
using TrainingIS.BLL.Resources;
using static GApp.BLL.Services.MessagesService;
using GApp.Models.DataAnnotations;
using GApp.Core.Context;
using TrainingIS.Entities.Resources.SeanceDayResources;
using TrainingIS.Entities.ModelsViews;
using GApp.Models.Pages;
using TrainingIS.BLL.Base;
using TrainingIS.Entities.Resources.SeanceDayResources;
using TrainingIS.Entities.ModelsViews;
 

namespace  TrainingIS.BLL
{ 
	public partial class BaseSeanceDayBLO : TrainingIS_BaseBLO<SeanceDay>{
	    

		public BaseSeanceDayBLO(UnitOfWork<TrainingISModel> UnitOfWork,GAppContext GAppContext) : base(new SeanceDayDAO(UnitOfWork.context),GAppContext)
        {
		    this._UnitOfWork = UnitOfWork;
			this.PluralName = msg_SeanceDay.PluralName;
        }

		public virtual List<string> GetSearchCreteria()
        {
            List<string> SearchCreteria = new List<string>();
            foreach (PropertyInfo model_property in typeof(Default_Details_SeanceDay_Model).GetProperties(typeof(GAppDataTableAttribute)))
            {
                GAppDataTableAttribute gappDataTableAttribute = model_property.GetCustomAttribute(typeof(GAppDataTableAttribute)) as GAppDataTableAttribute;
                string SearchBy = string.IsNullOrEmpty(gappDataTableAttribute.SearchBy) ? model_property.Name : gappDataTableAttribute.SearchBy;
                SearchCreteria.Add(gappDataTableAttribute.SearchBy);
            }
            foreach (PropertyInfo model_property in typeof(Default_Details_SeanceDay_Model).GetProperties(typeof(SearchByAttribute)))
            {
                var attributes = model_property.GetCustomAttributes(typeof(SearchByAttribute));
                foreach (var attribute in attributes)
                {
                    SearchCreteria.Add((attribute as SearchByAttribute).PropertyPath);
                }

            }
            return SearchCreteria;
        }

		public virtual List<string> NavigationPropertiesNames()
        {
            EntityType entityType = this._UnitOfWork.context.getEntityType(this.TypeEntity());
            var NavigationMembers = entityType.NavigationProperties.Select(p => p.Name).ToList<string>();
            return NavigationMembers;
        }

		public override int Save(SeanceDay item)
        {
            var value = base.Save(item);
            return value;
        }


		public virtual IQueryable<SeanceDay> Find_as_Queryable(
            FilterRequestParams filterRequestParams,
            List<string> SearchCreteria,
            out int totalRecords,
			Func<SeanceDay, bool> Condition = null)
        {
            // Default PageSize and CurrentPage
            if (filterRequestParams.pageSize == null) filterRequestParams.pageSize = 50;
            if (filterRequestParams.currentPage == null) filterRequestParams.currentPage = 0;

           IQueryable<SeanceDay> Query = this.entityDAO
                .Find(filterRequestParams, SearchCreteria,out totalRecords,Condition);
            return Query;
        }

		/// <summary>
        /// Export all data to DataTable
        /// </summary>
        /// <returns>DataTable contain all data in database</returns>
        public virtual DataTable Export()
        {
            ExportService exportService = new ExportService(typeof(SeanceDay));
            DataTable entityDataTable = exportService.CreateDataTable(msg_SeanceDay.PluralName);
            exportService.Fill(entityDataTable, this.FindAll().ToList<object>());
            return entityDataTable;
        }
		
		/// <summary>
		/// Import data to dataBase from DataTable
		/// </summary>
		/// <param name="dataTable"></param>
		public virtual ImportReport Import(DataTable dataTable, string FileName = "")
		{
				// Chekc Reference colone existance
				string local_reference_name = this.CheckExistanceOfReferenceColumn(dataTable);

				// Creae ImportService instance
				List<string> navigationPropertiesNames = this._UnitOfWork.context.GetForeignKeyNames(this.TypeEntity()).ToList<string>();
				List<string> foreignKeys = this._UnitOfWork.context.GetForeignKeysIds(this.TypeEntity()).ToList<string>();
				ImportService importService = new ImportService(dataTable, typeof(SeanceDay), this.GAppContext);

				foreach (DataRow dataRow in dataTable.Rows)
				{
					// Create UnitOfWork by Row
					// this.InitUnitOfWork();

					// Read entity reference : the Reference can't be empty
					String entity_reference = dataRow[local_reference_name].ToString();
					int index = dataRow.Table.Rows.IndexOf(dataRow);
					if (string.IsNullOrEmpty(entity_reference))
					{
						string msg = string.Format(msgBLO.The_reference_of_the_entity_can_not_be_empty, index + 1);
						importService.Report.AddMessage(msg, MessageTypes.Error, dataRow);
						continue;
					}

					// Load or Create Entity

					Operation operation;
					SeanceDay entity = this.Load_Or_CreateEntity(importService, entity_reference);
					if (entity.Id == 0) operation = Operation.Add;
					else operation = Operation.Update;


					// Save or Update Entity
					try
					{
						// Fill value from DataRow
						importService.Fill_Value(entity, dataRow, this._UnitOfWork);

						// Save Entity to database
						this.Save(entity);

						if (operation == Operation.Add)
						{
							string msg = string.Format(msgBLO.Inserting_the_entity, entity);
							importService.Report.AddMessage(msg, MessageTypes.Add_Success);
						}
						else
						{
							string msg = string.Format(msgBLO.Updatring_the_entity, entity);
							importService.Report.AddMessage(msg, MessageTypes.Update_Success);
						}
					}
					catch (GApp.DAL.Exceptions.DataBaseEntityValidationException e)
					{
						string msg = string.Format(" ! erreur à la ligne {0} :", index + 1) + e.Message;
						if (operation == Operation.Add)
							importService.Report.AddMessage(msg, MessageTypes.Add_Error, dataRow);
						else
							importService.Report.AddMessage(msg, MessageTypes.Update_Error, dataRow);
					}
					catch (GApp.DAL.Exceptions.GAppDbException e)
					{
						string msg = string.Format(" ! erreur à la ligne {0} :", index + 1) + e.Message;

						if (operation == Operation.Add)
							importService.Report.AddMessage(msg, MessageTypes.Add_Error, dataRow);
						else
							importService.Report.AddMessage(msg, MessageTypes.Update_Error, dataRow);
					}
                    catch (Exception e)
                    {
                        // [Bug] must log the new exception
                        string msg = string.Format(" ! erreur à la ligne {0} :", index + 1) + e.Message;

                        if (operation == Operation.Add)
                            importService.Report.AddMessage(msg, MessageTypes.Add_Error, dataRow);
                        else
                            importService.Report.AddMessage(msg, MessageTypes.Update_Error, dataRow);
                    }
				}

				// Log Work
				this.LogWork(FileName);
           
				return importService.Report;       
			}
			 
			#region Import private function
			protected enum Operation { Add, Update};
			private void LogWork(string FileName)
			{  
				this.InitUnitOfWork();
				LogWork logWork = new LogWork();
				logWork.OperationReference = FileName;
				logWork.OperationWorkType = OperationWorkTypes.Import;
			    logWork.UserId = this.GAppContext.Current_User_Name;
				logWork.EntityType = this.TypeEntity().Name;
				new LogWorkBLO(this._UnitOfWork,this.GAppContext).Save(logWork);
			}

			private string CheckExistanceOfReferenceColumn(DataTable dataTable)
			{
				string refernce_name = nameof(BaseEntity.Reference);
				string local_reference_name = this.TypeEntity().GetProperty(refernce_name).getLocalName();
				if (!dataTable.Columns.Contains(local_reference_name))
				{
					string msg = msg_BLO.The_reference_column_does_not_exist_in_the_import_excel_file;
					throw new ImportException(msg);
				}
				return local_reference_name;
			}
			private void InitUnitOfWork()
			{
				// UnitofWorkInitialization
				this._UnitOfWork = new UnitOfWork<TrainingISModel>();
				this.entityDAO = new SeanceDayDAO(_UnitOfWork.context);
			}
			private SeanceDay Load_Or_CreateEntity(ImportService importService, string entity_reference)
			{
				Operation operation;
				SeanceDay entity = this.FindBaseEntityByReference(entity_reference);
				if (entity == null) // Add new if the entity not exist
				{
					entity = new SeanceDayBLO(this._UnitOfWork, this.GAppContext).CreateInstance();
					operation = Operation.Add;
				}
				else
				{
					operation = Operation.Update;
				}
				return entity;
			}
			#endregion

			/// <summary>
			/// Load from DataBase id the entity not attached with the current context
			/// </summary>
			/// <param name="item"></param>
			/// <returns></returns>
			protected virtual SeanceDay Load_if_not_attached_in_current_context(SeanceDay item)
			{

				SeanceDay entity = null;

				// if the item is in current context
				var item_in_context = this._UnitOfWork.context.SeanceDays.Local.Where(a => a.Id == item.Id);
				if (item_in_context == null)
				{
					entity = this.FindBaseEntityByID(item.Id);
				}
				else
				{
					entity = item;
				}
				return entity;
			}


	}

	public  partial class SeanceDayBLO : BaseSeanceDayBLO{
		public SeanceDayBLO(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork,GAppContext) {}
	 
	}
}


