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

namespace  TrainingIS.BLL
{
	public partial class FormerBLO : BaseBLO<Former>{
	    
		public FormerBLO(DbContext context) : base()
        {
            this.entityDAO = new FormerDAO(context);
        }
		 
		public FormerBLO() : base()
        {
           this.entityDAO = new FormerDAO(TrainingISModel.CreateContext());
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

            var foreignKeys = getForeignKeys(typeof(Former));
            var Keys = getKeys(typeof(Former));

            var navigationPropertiesNames = this.NavigationPropertiesNames();

            // Create DataColumn Names
            var Properties = typeof(Former).GetProperties();
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
        public void Import(DataTable dataTable)
        {

            var Properties = this.TypeEntity().GetProperties();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                String reference = dataRow[nameof(BaseEntity.Reference)].ToString();

                // Add new if the entity not exist
                if (this.FindBaseEntityByReference(reference) == null)
                {
                    Former entity = new Former();

                    // Fill primitive value from DataRow
                    GApp.Core.Utils.ConversionUtil.FillBeanFieldsByDataRow_PrimitiveValue(entity, dataRow);

                    // Fill none primitive value
                    var navigationPropertiesNames = this.NavigationPropertiesNames();
                    foreach (PropertyInfo propertyInfo in Properties)
                    {
                        if (navigationPropertiesNames.Contains(propertyInfo.Name))
                        {
                            // Generic Algo

                            //// if One to One or OneToMany
                            string navigationMemberReference = dataRow[propertyInfo.Name].ToString();
                            Type navigationMemberType = propertyInfo.PropertyType;

                            // if One to One or OneToMany
                            if (propertyInfo.Name == "Group")
                            {
                                GroupBLO groupBLO = new GroupBLO();
                                var navigatationMemberValue = groupBLO.FindBaseEntityByReference(navigationMemberReference);
                                propertyInfo.SetValue(entity, navigatationMemberValue);
                            }
                            // if ManyToMany
                        }



                    }
                    this.Save(entity);
                }

            }
        }
 
	}
}
