using GApp.Core.Localization;
using GApp.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TrainingIS.BLL.Services.Import
{
    /// <summary>
    /// Export Business Objext
    /// </summary>
    public class ExportService : BaseImportExportService
    {

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="EntityType">Type of exported entity</param>
        public ExportService(Type EntityType):base(EntityType) {}
        public ExportService(Type EntityType, Type ModelType) : base(EntityType, ModelType) { }




        /// <summary>
        /// Get DataTable Columns for the exporte files
        /// </summary>
        /// <returns></returns>
        public DataColumn[] getDataTableColumns()
        {
            List<DataColumn> dataColumns = new List<DataColumn>();

            // Add Columns to DataTable instance
            var Properties = this.GetExportedProperties();
            foreach (PropertyInfo item in Properties)
            {
                string local_name_of_property = item.getLocalName();
                DataColumn column = new DataColumn();
                column.ColumnName = local_name_of_property;
                dataColumns.Add(column);
            }

            return dataColumns.ToArray();
        }

        public void Fill(DataTable entityDataTable, List<object> entities)
        {
            foreach (var entity in entities)
            {
                DataRow dataRow = entityDataTable.NewRow();
                foreach (PropertyInfo property in this.GetExportedProperties())
                {
                    string local_name_of_property = property.getLocalName();
                    // if OneToOne or ManyToOne Relationship
                    if (this.ForeignKeiesNames.Contains(property.Name))
                    {
                        var value = property.GetValue(entity) as BaseEntity;
                        if (value != null)
                            dataRow[local_name_of_property] = value.Reference;
                        else
                        {
                            var owner = property.GetValue(entity) as TrainingIS.Entitie_excludes.ApplicationUser;
                            if (owner != null)
                                dataRow[local_name_of_property] = owner.UserName;
                        }
                        continue;
                    }
                    // if ManyToMany Relationship
                    if (this.ManyToManyKeiesNames.Contains(property.Name))
                    {
                        IList list_value = property.GetValue(entity) as IList;
                        var value = list_value.Cast<BaseEntity>();

                        if (value != null)
                            dataRow[local_name_of_property] = string.Join(",",value.Select(e=>e.Reference).ToList())  ;
                        continue;
                    }
                    // if Enum
                    if (property.PropertyType.IsEnum)
                    {
                        string value = GAppEnumLocalization.GetLocalValue(property.PropertyType, property.GetValue(entity).ToString());
                        dataRow[local_name_of_property] = value;
                        continue;
                    }
                    // by default
                    dataRow[local_name_of_property] = property.GetValue(entity);
                }
                entityDataTable.Rows.Add(dataRow);
            }
        }

        public DataTable CreateDataTable(string DataTableName)
        {
            // Worksheet names cannot be more than 31 characters
            DataTable entityDataTable = new DataTable(DataTableName.Truncate(30));
            entityDataTable.Columns.AddRange(this.getDataTableColumns());
            return entityDataTable;
        }
    }
}
