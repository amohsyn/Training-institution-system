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
using TrainingIS.Entities.Resources.TrainingTypeResources;
using static TrainingIS.BLL.MessagesService;
using TrainingIS.BLL.Resources;

namespace  TrainingIS.BLL
{
	public partial class BaseTrainingTypeBLO : BaseBLO<TrainingType>{
	    
		protected UnitOfWork _UnitOfWork = null;

		public BaseTrainingTypeBLO(UnitOfWork UnitOfWork) : base()
        {
		    this._UnitOfWork = UnitOfWork;
            this.entityDAO = this._UnitOfWork.TrainingTypeDAO;
        }
		 
		private BaseTrainingTypeBLO() : base() {}


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
            DataTable entityDataTable = new DataTable(msg_TrainingType.PluralName);

            var foreignKeys = this._UnitOfWork.context.GetForeignKeysIds(typeof(TrainingType));
            var Keys =  this._UnitOfWork.context.GetKeyNames(typeof(TrainingType));

            var navigationPropertiesNames = this.NavigationPropertiesNames();

            // Create DataColumn Names
            var Properties = typeof(TrainingType).GetProperties();
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

		

		


    protected enum Operation { Add, Update};

	private void InitUnitOfWork()
		{
            // UnitofWorkInitialization
            UnitOfWork unitOfWorkImport = new UnitOfWork();
            this._UnitOfWork = unitOfWorkImport;
            this.entityDAO = new TrainingTypeDAO(unitOfWorkImport.context);
		}

	 /// <summary>
    /// Import data to dataBase from DataTable
    /// </summary>
    /// <param name="dataTable"></param>
	public virtual string Import(DataTable dataTable)
        {

  // Chekc Reference colone existance
            string refernce_name = nameof(BaseEntity.Reference);
            string local_reference_name = this.TypeEntity().GetProperty(refernce_name).getLocalName();
            if (!dataTable.Columns.Contains(local_reference_name))
            {
                string msg = msg_BLO.The_reference_column_does_not_exist_in_the_import_excel_file;
                throw new ImportException(msg);
            }

            // Init ImportService
            List<string>  navigationPropertiesNames = this._UnitOfWork.context.GetForeignKeyNames(this.TypeEntity()).ToList<string>();
            List<string> foreignKeys = this._UnitOfWork.context.GetForeignKeysIds(this.TypeEntity()).ToList<string>();
            ImportService importService = new ImportService(dataTable,typeof(TrainingType), navigationPropertiesNames, foreignKeys);
            int number_of_saved = 0;
            int number_of_updated = 0;

            Operation operation;
            var Properties = this.TypeEntity().GetProperties();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                // Create UnitOfWork by Row
                this.InitUnitOfWork();

                String reference = dataRow[local_reference_name].ToString();

                #region Create or Louad TrainingType Instance
                int index = dataTable.Rows.IndexOf(dataRow);
                // the Reference can't be empty
                if (string.IsNullOrEmpty(reference))
                {
                    string msg = string.Format(msgBLO.The_reference_of_the_entity_can_not_be_empty, index + 1);
                    importService.Report.AddMessage(msg, MessageTypes.Error);
                    continue;
                }
                // Add new if the entity not exist
                TrainingType entity = this.FindBaseEntityByReference(reference);
                if (entity == null)
                {
                    entity = new TrainingType();
                    operation = Operation.Add;
                }
                else
                {
                    operation = Operation.Update;
                }
                #endregion


                importService.Fill_Value(entity, dataRow, this._UnitOfWork);

                // Save or Update Entity
                try
                {

                    this.Save(entity);

                    if (operation == Operation.Add)
                    {
                        number_of_saved++;
                        string msg = string.Format(msgBLO.Inserting_the_entity, entity);
                        importService.Report.AddMessage(msg, MessageTypes.Add_Success);

                    }
                    else
                    {
                        number_of_updated++;
                        string msg = string.Format(msgBLO.Updatring_the_entity, entity);
                        importService.Report.AddMessage(msg, MessageTypes.Update_Success);
                    }
                }
                catch (GApp.DAL.Exceptions.DataBaseEntityValidationException e)
                {
                    string msg = string.Format(" ! erreur à la ligne {0} :", index + 1) + e.Message;
                    importService.Report.AddMessage(msg, MessageTypes.Error);
                   // throw new ImportException(msg);

                }
                catch (GApp.DAL.Exceptions.GAppDataBaseException e)
                {
                    string msg = string.Format(" ! erreur à la ligne {0} :", index + 1) + e.Message;

                    importService.Report.AddMessage(msg, MessageTypes.Error);
                    // throw new ImportException(msg);

                }
                catch (Exception e)
                {
                    string msg = string.Format(" ! erreur à la ligne {0} :", index + 1) + e.Message;
                    importService.Report.AddMessage(msg, MessageTypes.Error);
                   //  throw new ImportException(msg);

                }

                // Init Context after each insert
                // this._UnitOfWork.Reset();
                //this.Init();
                
            }

            // Resume
            string resume_msg = string.Format(msgBLO.In_total_there_is_the_insertion_of, number_of_saved) + " " + msg_TrainingType.PluralName;
            importService.Report.AddMessage(resume_msg, MessageTypes.Resume_Info);
            resume_msg = string.Format(msgBLO.In_total_there_is_the_update_of, number_of_updated) + " " + msg_TrainingType.PluralName;
            importService.Report.AddMessage(resume_msg, MessageTypes.Resume_Info);

            return importService.Report.get_HTML_Report();        }
	}

	public  partial class TrainingTypeBLO : BaseTrainingTypeBLO{
		public TrainingTypeBLO(UnitOfWork UnitOfWork) : base(UnitOfWork) {}
	
	}
}
