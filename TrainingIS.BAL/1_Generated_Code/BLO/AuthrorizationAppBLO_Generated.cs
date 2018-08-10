using TrainingIS.Entities;
using GApp.BLL;
using TrainingIS.DAL;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System;
using System.Data;
using System.Reflection;
using GApp.Entities;
using TrainingIS.Entities.Resources.AuthrorizationAppResources;
using static TrainingIS.BLL.MessagesService;
using TrainingIS.BLL.Resources;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using GApp.Core.MetaDatas.Attributes;
using TrainingIS.BLL.Services.Import;

namespace  TrainingIS.BLL
{ 
	public partial class BaseAuthrorizationAppBLO : BaseBLO<AuthrorizationApp>{
	    
		protected UnitOfWork _UnitOfWork = null;

		public BaseAuthrorizationAppBLO(UnitOfWork UnitOfWork) : base(UnitOfWork.AuthrorizationAppDAO)
        {
		    this._UnitOfWork = UnitOfWork;
        }

		public virtual List<string> NavigationPropertiesNames()
        {
            EntityType entityType = this._UnitOfWork.context.getEntityType(this.TypeEntity());
            var NavigationMembers = entityType.NavigationProperties.Select(p => p.Name).ToList<string>();
            return NavigationMembers;
        }

		public override List<AuthrorizationApp> FindAll()
        {
            return base.FindAll().OrderBy(entity => entity.Ordre).ToList();
        }

		/// <summary>
        /// Export all data to DataTable
        /// </summary>
        /// <returns>DataTable contain all data in database</returns>
        public virtual DataTable Export()
        {
            ExportService exportService = new ExportService(typeof(AuthrorizationApp));
            DataTable entityDataTable = exportService.CreateDataTable(msg_AuthrorizationApp.PluralName);
            exportService.Fill(entityDataTable, this.FindAll().ToList<BaseEntity>());
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
				ImportService importService = new ImportService(dataTable, typeof(AuthrorizationApp), navigationPropertiesNames, foreignKeys);

				foreach (DataRow dataRow in dataTable.Rows)
				{
					// Create UnitOfWork by Row
					this.InitUnitOfWork();

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
					AuthrorizationApp entity = this.Load_Or_CreateEntity(importService, entity_reference);
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
				logWork.UserId = this._UnitOfWork.User_Identity_Name;
				logWork.EntityType = this.TypeEntity().Name;
				new LogWorkBLO(this._UnitOfWork).Save(logWork);
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
				UnitOfWork unitOfWorkImport = this._UnitOfWork.CreateNewUnitOfWork();
				this._UnitOfWork = unitOfWorkImport;
				this.entityDAO = new AuthrorizationAppDAO(unitOfWorkImport.context);
			}
			private AuthrorizationApp Load_Or_CreateEntity(ImportService importService, string entity_reference)
			{
				Operation operation;
				AuthrorizationApp entity = this.FindBaseEntityByReference(entity_reference);
				if (entity == null) // Add new if the entity not exist
				{
					entity = new AuthrorizationApp();
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
			/// Validation GApp attributes
			/// </summary>
			/// <param name="entity"></param>
			/// <returns></returns> 
			public IList<ValidationResult> Validate(AuthrorizationApp entity)
			{
				IList<ValidationResult> validationResults = new List<ValidationResult>();

				// Validation Unique 
				var ls_properties_with_unique = entity.GetType().GetProperties(typeof(UniqueAttribute));
				foreach (var PropertyInfo in ls_properties_with_unique)
				{
				   bool isUnique = this.IsUnique(entity, PropertyInfo);
					if (!isUnique)
					{
						// [Bug] Localization
						string error_msg = string.Format("Le champ {0} doit être unique , il exist déja un entity avec cette valeur dans la base de données", PropertyInfo.getLocalName());
						List<string> property_that_have_error = new List<string>();
						property_that_have_error.Add(PropertyInfo.getLocalName());
						ValidationResult validationResult = 
							new ValidationResult(error_msg, 
							property_that_have_error.AsEnumerable());
						validationResults.Add(validationResult);
					}
                   
				}
				return  (validationResults == null)?  null: validationResults;
			}

			/// <summary>
			/// Check if the value of property is Unique
			/// </summary>
			/// <param name="entity"></param>
			/// <param name="propertyInfo"></param>
			/// <returns></returns>
			public bool IsUnique(AuthrorizationApp entity, PropertyInfo propertyInfo)
			{
				// Edit Case
				if (entity.Id != 0)
				{
					// Edit Case
					AuthrorizationApp AuthrorizationApp_db = this.FindBaseEntityByID(entity.Id);
					if (propertyInfo.GetValue(AuthrorizationApp_db).ToString() == propertyInfo.GetValue(entity).ToString())
						return true;
				}

				if (propertyInfo.GetValue(entity) != null)
				{
					var context = this._UnitOfWork.context;
             
					var param = Expression.Parameter(typeof(AuthrorizationApp));
					var body = Expression.Equal(Expression.Property(param, propertyInfo),
						Expression.Constant(propertyInfo.GetValue(entity)));
					var lambda_expression = Expression.Lambda<Func<AuthrorizationApp, bool>>(body, param);

					var exsitant_entity = context.AuthrorizationApps.Where(lambda_expression).FirstOrDefault();
					return (exsitant_entity == null) ? true : false;
				}
				else
					return true;
            
			}

	}

	public  partial class AuthrorizationAppBLO : BaseAuthrorizationAppBLO{
		public AuthrorizationAppBLO(UnitOfWork UnitOfWork) : base(UnitOfWork) {}
	
	}
}
