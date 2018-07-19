﻿using TrainingIS.Entities;
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
using TrainingIS.Entities.Resources.TraineeResources;

namespace  TrainingIS.BLL
{
	public partial class BaseTraineeBLO : BaseBLO<Trainee>{
	    
		protected UnitOfWork _UnitOfWork = null;

		public BaseTraineeBLO(UnitOfWork UnitOfWork) : base()
        {
		    this._UnitOfWork = UnitOfWork;
            this.entityDAO = this._UnitOfWork.TraineeDAO;
        }
		 
		private BaseTraineeBLO() : base() {}


		public virtual List<string> NavigationPropertiesNames()
        {
            EntityType entityType = this._UnitOfWork.context.getEntityType(this.TypeEntity());
            var NavigationMembers = entityType.NavigationProperties.Select(p => p.Name).ToList<string>();
            return NavigationMembers;
        }

		/// <summary>
        /// Get foreignKeys list for a Entity
        /// </summary>
        /// <param name="typeEntity">Type of Entity</param>
        /// <returns></returns>
        public virtual List<string> getForeignKeys(Type typeEntity)
        {
            EntityType entityType = this._UnitOfWork.context.getEntityType(typeEntity);
            var NavigationMembers = entityType.NavigationProperties.Select(p => p.Name).ToList<string>();
            List<string> ForeignKeys = new List<string>();

            // [Bug] the foreign key may be named diffrente of [EntityName + Id]
            for (int i = 0; i < NavigationMembers.Count(); i++)
            {
                ForeignKeys.Add(NavigationMembers[i] + "Id");
            }
            return ForeignKeys;
        }

		protected List<string> getKeys(Type typeEntity)
        {
            EntityType TraineeEntityType = this._UnitOfWork.context.getEntityType(typeEntity);
            var keys = TraineeEntityType.KeyProperties.Select(p => p.Name).ToList<string>();
            return keys;
        }

		 /// <summary>
        /// Convert All Entities to DataTable
        /// </summary>
        /// <returns>DataTable</returns>
        public virtual DataTable Export()
        {
            var entities = this.FindAll();
            DataTable entityDataTable = new DataTable("Entities");

            var foreignKeys = getForeignKeys(typeof(Trainee));
            var Keys = getKeys(typeof(Trainee));

            var navigationPropertiesNames = this.NavigationPropertiesNames();

            // Create DataColumn Names
            var Properties = typeof(Trainee).GetProperties();
            foreach (PropertyInfo item in Properties)
            {
                // d'ont show foreignKeys members
                if (!foreignKeys.Contains(item.Name) && !Keys.Contains(item.Name))
                {
                    DataColumn column = new DataColumn();
                    column.ColumnName = item.Name;
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
                        if (navigationPropertiesNames.Contains(item.Name))
                        {
                            // OneToOne or ManyToOne
                            var value = item.GetValue(entity) as BaseEntity;
                            if (value != null)
                                dataRow[item.Name] = value.Reference;
                        }
                        else
                        {
                            dataRow[item.Name] = item.GetValue(entity);
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
            string msg = "";
            int number_of_saved = 0;
            int number_of_updated = 0;
            Operation operation;
            var Properties = this.TypeEntity().GetProperties();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                String reference = dataRow[nameof(BaseEntity.Reference)].ToString();

                int index = dataTable.Rows.IndexOf(dataRow);

                // the Reference can't be empty
                if (string.IsNullOrEmpty(reference))
                {
                    msg += " * " + string.Format(msgBLO.The_reference_of_the_entity_can_not_be_empty, index + 1) + "<br>";
                    continue;
                }

                // Add new if the entity not exist
                Trainee entity = this.FindBaseEntityByReference(reference);
                if (entity == null)
                {
                    entity = new Trainee();
                    operation = Operation.Add;
                }
                else
                {
                    operation = Operation.Update;
                }
                // Fill primitive value from DataRow
                GApp.Core.Utils.ConversionUtil.FillBeanFieldsByDataRow_PrimitiveValue(entity, dataRow);

                // Fill none primitive value
                var navigationPropertiesNames = this.NavigationPropertiesNames();
                foreach (PropertyInfo propertyInfo in Properties)
                {
                    if (navigationPropertiesNames.Contains(propertyInfo.Name))
                    {
                        // Dynamic type Algo

                        //// if One to One or OneToMany
                        string navigationMemberReference = dataRow[propertyInfo.Name].ToString();
                        if (string.IsNullOrEmpty(navigationMemberReference))
                        {
                            propertyInfo.SetValue(entity, null);
                        }
                        else
                        {
                            Type navigationMemberType = propertyInfo.PropertyType;
                            DAL.TrainingISModel trainingISModel = this._UnitOfWork.context;
                            var navigationProperty_set = trainingISModel.Set(propertyInfo.PropertyType);
						    navigationProperty_set.Load();
                            var vlaue = navigationProperty_set.Local.OfType<BaseEntity>().Where(e => e.Reference == navigationMemberReference).FirstOrDefault();
                            if(vlaue == null)
                            {
                                // [ToDo] Transltate propertyInfo.PropertyType
                                msg += string.Format(" ! erreur à la ligne {0} : la référence {1} de l'objet {2} n'exist pas dans la base de données", index + 1, navigationMemberReference, propertyInfo.PropertyType) + "<br>";
                                throw new ImportLineException(msg);
                            }
                            else
                            {
                                propertyInfo.SetValue(entity, vlaue);
                            }
                        }
                        // if ManyToMany
                    }
                }

                try
                {
                    this.Save(entity);
                    if(operation == Operation.Add)
                    {
                        number_of_saved++;
                        msg += " + " + string.Format(msgBLO.Inserting_the_entity, entity) + "<br>";
                    }
                    else
                    {
                        number_of_updated++;
                        msg += " + " + string.Format(msgBLO.Updatring_the_entity, entity) + "<br>";
                    }
                }
                catch (Exception e)
                {
                    msg += string.Format( " ! erreur à la ligne {0} :", index+1) +  e.Message + "<br>";
                    throw new ImportLineException(msg);

                }
            }

            msg += "<hr>";
            msg += string.Format(msgBLO.In_total_there_is_the_insertion_of, number_of_saved) + " " + msg_Trainee.PluralName;
			msg += "<br>";
            msg += string.Format(msgBLO.In_total_there_is_the_update_of, number_of_updated) + " " + msg_Trainee.PluralName;
            return msg;
        }



 
	}

	public  partial class TraineeBLO : BaseTraineeBLO{
		public TraineeBLO(UnitOfWork UnitOfWork) : base(UnitOfWork) {}
	
	}
}
