﻿using GApp.Core.Localization;
using GApp.Entities;
using System;
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
    public class ExportService
    {
        private Type EntityType;
        private TrainingISModel Context;

        private List<string> ForeignKeiesIds { get; }
        private List<string> ForeignKeiesNames { get; }
        private List<string> ManyKeiesNames { get; }

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="EntityType">Type of exported entity</param>
        public ExportService(Type EntityType)
        {
            this.EntityType = EntityType;
            this.Context = new TrainingISModel();

            this.ForeignKeiesIds = this.Context.GetForeignKeysIds(this.EntityType);
            this.ForeignKeiesNames = this.Context.GetKeyNames(this.EntityType).ToList();
            this.ManyKeiesNames = this.Context.Get_Many_ForeignKeyNames(this.EntityType).ToList();
        }


        public List<PropertyInfo> GetExportedProperties()
        {
            return this.EntityType.GetProperties()
                        .Where(property => !property.IsDefined(typeof(NotMappedAttribute)))
                        .Where(property => !this.ForeignKeiesIds.Contains(property.Name))
                        .ToList();


        }

        /// <summary>
        /// Get DataTable Columns
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

        public void Fill(DataTable entityDataTable, List<Former> entities)
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
                        continue;
                    }
                    // if OneToOne or ManyToOne Relationship
                    if (this.ManyKeiesNames.Contains(property.Name))
                    {
                        var value = property.GetValue(entity) as List<BaseEntity>;
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
            DataTable entityDataTable = new DataTable(DataTableName);
            entityDataTable.Columns.AddRange(this.getDataTableColumns());
            return entityDataTable;
        }
    }
}
