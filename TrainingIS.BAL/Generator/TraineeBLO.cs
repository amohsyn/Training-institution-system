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
using TrainingIS.Entities.Resources.TraineeResources;

namespace  TrainingIS.BLL
{
	public partial class TraineeBLO : BaseBLO<Trainee>{
	    
		public TraineeBLO(DbContext context) : base()
        {
            this.entityDAO = new TraineeDAO(context);
        }
		 
		public TraineeBLO() : base()
        {
           this.entityDAO = new TraineeDAO(TrainingISModel.CreateContext());
        }


		public List<string> NavigationPropertiesNames()
        {
            EntityType entityType = DAL.TrainingISModel.CreateContext().getEntityType(this.TypeEntity());
            var NavigationMembers = entityType.NavigationProperties.Select(p => p.Name).ToList<string>();
            return NavigationMembers;
        }

		/// <summary>
        /// Get foreignKeys list for a Entity
        /// </summary>
        /// <param name="typeEntity">Type of Entity</param>
        /// <returns></returns>
        public List<string> getForeignKeys(Type typeEntity)
        {
            EntityType entityType = DAL.TrainingISModel.CreateContext().getEntityType(typeEntity);
            var NavigationMembers = entityType.NavigationProperties.Select(p => p.Name).ToList<string>();
            List<string> ForeignKeys = new List<string>();

            // [Bug] the foreign key may be named diffrente of [EntityName + Id]
            for (int i = 0; i < NavigationMembers.Count(); i++)
            {
                ForeignKeys.Add(NavigationMembers[i] + "Id");
            }
            return ForeignKeys;
        }

		private List<string> getKeys(Type typeEntity)
        {
            EntityType TraineeEntityType = DAL.TrainingISModel.CreateContext().getEntityType(typeEntity);
            var keys = TraineeEntityType.KeyProperties.Select(p => p.Name).ToList<string>();
            return keys;
        }

		 /// <summary>
        /// Convert All Entities to DataTable
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable Export()
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


		


		 /// <summary>
        /// Import data to dataBase from DataTable
        /// </summary>
        /// <param name="dataTable"></param>
        public string Import(DataTable dataTable)
        {
            string msg = "";
            int number_of_saved = 0;
            var Properties = this.TypeEntity().GetProperties();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                String reference = dataRow[nameof(BaseEntity.Reference)].ToString();

                if (string.IsNullOrEmpty(reference))
                {
                    int index = dataTable.Rows.IndexOf(dataRow);
                    msg += " * " +  string.Format(msgBLO.The_reference_of_the_entity_can_not_be_empty, index +1) + "<br>";
                    continue;
                }

                // Add new if the entity not exist
                Trainee entity = this.FindBaseEntityByReference(reference);
                if (entity == null)
                {
                    entity = new Trainee();

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
                            Type navigationMemberType = propertyInfo.PropertyType;
							DAL.TrainingISModel trainingISModel = DAL.TrainingISModel.CreateContext();
                            var navigationProperty_set = trainingISModel.Set(propertyInfo.PropertyType);
                            var vlaue = navigationProperty_set.Local.OfType<BaseEntity>().Where(e => e.Reference == navigationMemberReference).FirstOrDefault();
                            propertyInfo.SetValue(entity, vlaue);

                            // if ManyToMany
                        }



                    }

                    this.Save(entity);
                    number_of_saved++;
                    msg += " + " +  string.Format(msgBLO.Inserting_the_entity, entity) + "<br>";

                }
                else
                {
                    msg += " - " + string.Format(msgBLO.the_entity_already_exists, entity) + "<br>";
                }
               
            }

            msg += "<hr>";
            msg += string.Format(msgBLO.In_total_there_is_the_insertion_of, number_of_saved) + " " + @msg_Trainee.PluralName;
            return msg;
        }


 
	}
}
