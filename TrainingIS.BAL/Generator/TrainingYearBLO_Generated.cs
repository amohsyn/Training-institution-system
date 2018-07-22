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
using TrainingIS.Entities.Resources.TrainingYearResources;
using static TrainingIS.BLL.MessagesService;

namespace  TrainingIS.BLL
{
	public partial class BaseTrainingYearBLO : BaseBLO<TrainingYear>{
	    
		protected UnitOfWork _UnitOfWork = null;

		public BaseTrainingYearBLO(UnitOfWork UnitOfWork) : base()
        {
		    this._UnitOfWork = UnitOfWork;
            this.entityDAO = this._UnitOfWork.TrainingYearDAO;
        }
		 
		private BaseTrainingYearBLO() : base() {}


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
            ImportService importService = new ImportService(this.TypeEntity(), this._UnitOfWork);
            var entities = this.FindAll();
            DataTable entityDataTable = new DataTable(msg_TrainingYear.PluralName);

            var foreignKeys = this._UnitOfWork.context.GetForeignKeysIds(typeof(TrainingYear));
            var Keys =  this._UnitOfWork.context.GetKeyNames(typeof(TrainingYear));

            var navigationPropertiesNames = this.NavigationPropertiesNames();

            // Create DataColumn Names
            var Properties = typeof(TrainingYear).GetProperties();
            foreach (PropertyInfo item in Properties)
            {
                string local_name_of_property = importService.getLocalNameOfProperty(item);

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
                        string local_name_of_property = importService.getLocalNameOfProperty(item);

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

	 /// <summary>
    /// Import data to dataBase from DataTable
    /// </summary>
    /// <param name="dataTable"></param>
	public virtual string Import(DataTable dataTable)
        {
            ImportService importService = new ImportService(typeof(TrainingYear), this._UnitOfWork);
            int number_of_saved = 0;
            int number_of_updated = 0;

            Operation operation;
            var Properties = this.TypeEntity().GetProperties();
            foreach (DataRow dataRow in dataTable.Rows)
            {

                String reference = dataRow[nameof(BaseEntity.Reference)].ToString();

                #region Create or Louad TrainingYear Instance
                int index = dataTable.Rows.IndexOf(dataRow);
                // the Reference can't be empty
                if (string.IsNullOrEmpty(reference)){
                      string msg = string.Format(msgBLO.The_reference_of_the_entity_can_not_be_empty, index + 1);
                    importService.Report.AddMessage(msg, MessageTypes.Error);
                    continue;
                }
                // Add new if the entity not exist
                TrainingYear entity = this.FindBaseEntityByReference(reference);
                if (entity == null){
                    entity = new TrainingYear();
                    operation = Operation.Add;
                }else{
                    operation = Operation.Update;
                }
                #endregion


                importService.Fill_Value(entity, dataRow);
                     
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
                        string msg =   string.Format(msgBLO.Updatring_the_entity, entity);
                        importService.Report.AddMessage(msg, MessageTypes.Update_Success);
                    }
                }
                catch (Exception e)
                {
                    string msg = string.Format(" ! erreur à la ligne {0} :", index + 1) + e.Message ;
                    importService.Report.AddMessage(msg, MessageTypes.Error);
                    throw new ImportException(msg);

                }
            }

            // Resume
            string resume_msg = string.Format(msgBLO.In_total_there_is_the_insertion_of, number_of_saved) + " " + msg_TrainingYear.PluralName;
            importService.Report.AddMessage(resume_msg, MessageTypes.Resume_Info);
            resume_msg = string.Format(msgBLO.In_total_there_is_the_update_of, number_of_updated) + " " + msg_TrainingYear.PluralName;
            importService.Report.AddMessage(resume_msg, MessageTypes.Resume_Info);

            return importService.Report.getReport();
        }
	}

	public  partial class TrainingYearBLO : BaseTrainingYearBLO{
		public TrainingYearBLO(UnitOfWork UnitOfWork) : base(UnitOfWork) {}
	
	}
}
