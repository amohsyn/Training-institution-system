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
using TrainingIS.Entities.Resources.StateOfAbseceResources;
using static TrainingIS.BLL.MessagesService;
using TrainingIS.BLL.Resources;

namespace  TrainingIS.BLL
{
	public partial class BaseStateOfAbseceBLO : BaseBLO<StateOfAbsece>{
	    
		protected UnitOfWork _UnitOfWork = null;

		public BaseStateOfAbseceBLO(UnitOfWork UnitOfWork) : base()
        {
		    this._UnitOfWork = UnitOfWork;
            this.entityDAO = this._UnitOfWork.StateOfAbseceDAO;
        }
		 
		private BaseStateOfAbseceBLO() : base() {}


		public virtual List<string> NavigationPropertiesNames()
        {
            EntityType entityType = this._UnitOfWork.context.getEntityType(this.TypeEntity());
            var NavigationMembers = entityType.NavigationProperties.Select(p => p.Name).ToList<string>();
            return NavigationMembers;
        }

		/// <summary>
        /// Convert All Entities to DataTable
        /// </summary>
        /// <returns>DataTable</returns>
		public virtual DataTable Export()
        {
            // ImportService importService = new ImportService(this.TypeEntity(), this._UnitOfWork);
            var entities = this.FindAll();
            DataTable entityDataTable = new DataTable(msg_StateOfAbsece.PluralName);

            var foreignKeys = this._UnitOfWork.context.GetForeignKeysIds(typeof(StateOfAbsece));
            var Keys =  this._UnitOfWork.context.GetKeyNames(typeof(StateOfAbsece));

            var navigationPropertiesNames = this.NavigationPropertiesNames();

            // Create DataColumn Names
            var Properties = typeof(StateOfAbsece).GetProperties();
            foreach (PropertyInfo item in Properties)
            {
                string local_name_of_property = item.getLocalName();

                // d'ont show foreignKeys members
                if (!foreignKeys.Contains(item.Name) && !Keys.Contains(item.Name))
                {
                    DataColumn column = new DataColumn();
                    column.ColumnName = local_name_of_property;
                    entityDataTable.Columns.Add(column);
                }

            }

            foreach (var entity in entities)
            {
                DataRow dataRow = entityDataTable.NewRow();
                foreach (PropertyInfo item in Properties)
                {
                    if (!foreignKeys.Contains(item.Name) && !Keys.Contains(item.Name))
                    {
                        string local_name_of_property = item.getLocalName();

                        if (navigationPropertiesNames.Contains(item.Name))
                        {
                            // OneToOne or ManyToOne
                            var value = item.GetValue(entity) as BaseEntity;
                            if (value != null)
                                dataRow[local_name_of_property] = value.Reference;
                        }
                        else
                        {
                            dataRow[local_name_of_property] = item.GetValue(entity);
                        }

                    }
                }
                entityDataTable.Rows.Add(dataRow);
            }
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
            ImportService importService = new ImportService(dataTable, typeof(StateOfAbsece), navigationPropertiesNames, foreignKeys);

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
                StateOfAbsece entity = this.Load_Or_CreateEntity(importService, entity_reference);
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
                catch (GApp.DAL.Exceptions.GAppDataBaseException e)
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
            this.entityDAO = new StateOfAbseceDAO(unitOfWorkImport.context);
        }
        private StateOfAbsece Load_Or_CreateEntity(ImportService importService, string entity_reference)
        {
            Operation operation;
            StateOfAbsece entity = this.FindBaseEntityByReference(entity_reference);
            if (entity == null) // Add new if the entity not exist
            {
                entity = new StateOfAbsece();
                operation = Operation.Add;
            }
            else
            {
                operation = Operation.Update;
            }
            return entity;
        }
        #endregion

	}

	public  partial class StateOfAbseceBLO : BaseStateOfAbseceBLO{
		public StateOfAbseceBLO(UnitOfWork UnitOfWork) : base(UnitOfWork) {}
	
	}
}
