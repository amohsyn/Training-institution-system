using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    /// <summary>
    /// Convert DataTable to Entities and Save
    /// with report generation
    /// </summary>
    public class ImportService
    {
        public ImportReportService Report { set; get; }
        protected TrainingISModel _Context;
        protected Type _TypeEntity;
        protected UnitOfWork _UnitOfWork;

        protected List<string> _NavigationPropertiesNames;
        protected List<string> _ForeignKeys;

        public ImportService(Type TypeEntity, UnitOfWork UnitOfWork)
        {
            this._UnitOfWork = UnitOfWork;
            this._Context = UnitOfWork.context;
            this._TypeEntity = TypeEntity;
            this._NavigationPropertiesNames = this._Context.GetForeignKeyNames(this._TypeEntity).ToList<string>();
            this._ForeignKeys = this._Context.GetForeignKeysIds(this._TypeEntity).ToList<string>();
            Report = new ImportReportService();
        }

        #region Fill DatRow
        public void Fill_Value(object entity, DataRow dataRow)
        {
            EntityPropertyShortcutBLO entityPropertyShortcutBLO = new EntityPropertyShortcutBLO(_UnitOfWork);

            List<EntityPropertyShortcut> propertiesShortcuts = entityPropertyShortcutBLO
                .getPropertiesShortcuts(entity.GetType());


            // Fill primitive value from DataRow
            this.Fill_PrimitiveValue(entity, propertiesShortcuts, dataRow);

            // Fill none primitive value
            this.Fill_NonPrimitiveValue(entity, propertiesShortcuts, dataRow);
        }
        public void Fill_PrimitiveValue(Object bean,
            List<EntityPropertyShortcut> propertiesShortcuts,
            DataRow dataRow)
        {
            PropertyInfo[] props = this._TypeEntity.GetProperties();

            foreach (PropertyInfo propertyInfo in props)
            {

                if (propertyInfo.PropertyType.IsPrimitive || propertyInfo.PropertyType == typeof(string) || propertyInfo.PropertyType == typeof(decimal) || propertyInfo.PropertyType == typeof(DateTime)
                    || propertyInfo.PropertyType == typeof(int?) || propertyInfo.PropertyType == typeof(decimal?) || propertyInfo.PropertyType == typeof(DateTime?) || propertyInfo.PropertyType == typeof(Guid) || propertyInfo.PropertyType == typeof(Guid?))
                {

                    // ForeignKeys not exist in DataTable ans it well confused with 
                    // NavigationPropeorty
                    if (this._ForeignKeys.Contains(propertyInfo.Name)) continue;

                    string name_of_property = propertyInfo.Name;
                    string local_name_of_property = this.getLocalNameOfProperty(propertyInfo);

                    List<string> ShortcutsNames = propertiesShortcuts
                        .Where(p => p.PropertyName == name_of_property)
                        .Select(p => p.PropertyShortcutName)
                        .ToList<string>();


                    int column_index = this.FindColumnIndex(dataRow,
                        name_of_property, local_name_of_property, ShortcutsNames);

                    // if the property not exist in the dataRow
                    if (column_index == -1) continue;
                   
                    if (dataRow.IsNull(column_index))
                    {
                        if (propertyInfo.PropertyType == typeof(string))
                        {
                            this._TypeEntity.GetProperty(name_of_property).SetValue(bean, "", null);
                        }
                        continue;
                    }

                    object value = dataRow[column_index];
                    this._TypeEntity.GetProperty(name_of_property).SetValue(bean, HackType(value, propertyInfo.PropertyType), null);
                }
            }
        }

        /// <summary>
        /// // Find column index according to thne name_of_property or ShortcutsNames
        /// </summary>
        private int FindColumnIndex(DataRow dataRow,
            string name_of_property,
            string local_name_of_property,
            List<string> ShortcutsNames)
        {
            int index = 0;
            foreach (var column in dataRow.Table.Columns)
            {
                DataColumn dataColumn = null;
                // Case 1
                dataColumn = dataRow.Table.Columns[name_of_property];
                if (dataColumn != null) return  index;
                // Case 2
                dataColumn = dataRow.Table.Columns[local_name_of_property];
                if (dataColumn == null) return index;
                // Case 3
                foreach (var shortcutsNames in ShortcutsNames){
                    dataColumn = dataRow.Table.Columns[shortcutsNames];
                    if (dataColumn == null) return index;
                }
                index++;
            }

            return -1;
        }

        public void Fill_NonPrimitiveValue(Object entity,
            List<EntityPropertyShortcut> propertiesShortcuts,
            DataRow dataRow)
        {
            var Properties = this._TypeEntity.GetProperties();

            foreach (PropertyInfo propertyInfo in Properties)
            {
                if (this._NavigationPropertiesNames.Contains(propertyInfo.Name))
                {

                    string name_of_property = propertyInfo.Name;
                    string local_name_of_property = this.getLocalNameOfProperty(propertyInfo);

                    List<string> ShortcutsNames = propertiesShortcuts
                        .Where(p => p.PropertyName == name_of_property)
                        .Select(p => p.PropertyShortcutName)
                        .ToList<string>();

                    int column_index = this.FindColumnIndex(dataRow,
                        name_of_property, local_name_of_property, ShortcutsNames);

                    // continue if the property not exist in the dataRow
                    if (column_index == -1) continue;


                    // Dynamic type Algo

                    //// if One to One or OneToMany
                    string navigationMemberReference = dataRow[column_index].ToString();
                    if (string.IsNullOrEmpty(navigationMemberReference))
                    {
                        propertyInfo.SetValue(entity, null);
                    }
                    else
                    {
                        Type navigationMemberType = propertyInfo.PropertyType;
                        var navigationProperty_set = this._Context.Set(propertyInfo.PropertyType);
                        navigationProperty_set.Load();
                        var vlaue = navigationProperty_set.Local.OfType<BaseEntity>().Where(e => e.Reference == navigationMemberReference).FirstOrDefault();
                        if (vlaue == null)
                        {
                            string msg = string.Format(" ! erreur à la ligne {0} : la référence {1} de l'objet {2} n'exist pas dans la base de données",
                                dataRow.Table.Rows.IndexOf(dataRow) + 1,
                                navigationMemberReference, local_name_of_property);
                            this.Report.AddMessage(msg, MessagesService.MessageTypes.Error);

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
        }




        private object HackType(object value, Type conversionType)
        {
            if (value == null)
                return null;
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {

                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }
        #endregion

        public string getLocalNameOfProperty(PropertyInfo propertyInfo)
        {
            /// get local_name_of_property
            string local_name_of_property = "";
            var displayAttribute = propertyInfo
                .GetCustomAttributes(typeof(DisplayAttribute), true)
                .Cast<DisplayAttribute>()
                .FirstOrDefault();
            if (displayAttribute == null)
            {
                local_name_of_property = propertyInfo.Name;
            }
            else
            {
                local_name_of_property = displayAttribute.GetName();
            }

            return local_name_of_property;
        }

    }
}
