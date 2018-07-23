using GApp.Core.MetaDatas.Attributes;
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
using TrainingIS.BLL.Resources;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.Entities.Base;

namespace TrainingIS.BLL
{
    /// <summary>
    /// Convert DataTable to Entities and Save
    /// with report generation
    /// </summary>
    public class ImportService
    {
      

        // Entry
        protected Type _TypeEntity;
        protected List<string> _NavigationPropertiesNames;
        protected List<string> _ForeignKeys;

        // output
        public ImportReport Report { set; get; }



        public ImportService(DataTable DataTable, Type TypeEntity, List<string> navigationPropertiesNames, List<string> foreignKeys)
        {
            this._TypeEntity = TypeEntity;
            this._NavigationPropertiesNames = navigationPropertiesNames;
            this._ForeignKeys = foreignKeys;
            Report = new ImportReport(DataTable);
        }

        #region Fill DatRow
        public void Fill_Value(object entity, DataRow dataRow, UnitOfWork unitOfWork)
        {
            EntityPropertyShortcutBLO entityPropertyShortcutBLO = new EntityPropertyShortcutBLO(unitOfWork);

            List<EntityPropertyShortcut> propertiesShortcuts = entityPropertyShortcutBLO
                .getPropertiesShortcuts(entity.GetType());


            // Fill primitive value from DataRow
            this.Fill_PrimitiveValue(entity, propertiesShortcuts, dataRow, unitOfWork);

            // Fill none primitive value
            this.Fill_NonPrimitiveValue(entity, propertiesShortcuts, dataRow, unitOfWork);
        }
        public void Fill_PrimitiveValue(Object bean,
            List<EntityPropertyShortcut> propertiesShortcuts,
            DataRow dataRow, UnitOfWork unitOfWork)
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
                    string local_name_of_property = propertyInfo.getLocalName();

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
            foreach (DataColumn column in dataRow.Table.Columns)
            {

                // Case 1
                if (column.ColumnName == name_of_property) return index;

                // Case 2
                if (column.ColumnName == local_name_of_property) return index;

                // Case 3
                foreach (var shortcutName in ShortcutsNames)
                {
                    if (column.ColumnName == shortcutName) return index;
                }
                index++;
            }

            return -1;
        }

        public void Fill_NonPrimitiveValue(Object entity,
            List<EntityPropertyShortcut> propertiesShortcuts,
            DataRow dataRow, UnitOfWork unitOfWork)
        {
            var Properties = this._TypeEntity.GetProperties();

            foreach (PropertyInfo propertyInfo in Properties)
            {
                if (!this._NavigationPropertiesNames.Contains(propertyInfo.Name)) continue;

                string name_of_property = propertyInfo.Name;
                string local_name_of_property = propertyInfo.getLocalName();

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
                    // Find the navigation Entity Value by it Reference
                    Type navigationMemberType = propertyInfo.PropertyType;
                    var navigationProperty_set = unitOfWork.context.Set(propertyInfo.PropertyType);
                    navigationProperty_set.Load();


                    var vlaue = navigationProperty_set.Local.OfType<BaseEntity>().Where(e => e.Reference == navigationMemberReference).FirstOrDefault();

                    // if the NavigationMemeber not exist in dataBase
                    if (vlaue == null)
                    {
                        // if AddAutomatically
                        var importAttribute = navigationMemberType.GetCustomAttribute(typeof(ImportAttribute));
                        if (importAttribute != null && (importAttribute as ImportAttribute).AddAutomatically)
                        {

                            // Creare Entity instance
                            AutoAddedEntity navigate_entity_instance = Activator.CreateInstance(navigationMemberType) as AutoAddedEntity;
                            navigate_entity_instance.Reference = navigationMemberReference;
                            navigate_entity_instance.Code = navigationMemberReference;
                            navigate_entity_instance.Name = navigationMemberReference;

                            // Save Entity
                            BLO_Manager BLO_Manager = new BLO_Manager(unitOfWork);
                            Type navigate_TypeBLO = BLO_Manager.getBLOType(navigationMemberType);
                            object[] param_blo = { unitOfWork };

                            var BLOIntance = Convert.ChangeType(Activator.CreateInstance(navigate_TypeBLO, param_blo), navigate_TypeBLO);
                            object[] param = { navigate_entity_instance };
                            try
                            {
                                navigate_TypeBLO.GetMethod("Save").Invoke(BLOIntance, param);
                                // Save to entity
                                vlaue = navigate_entity_instance;
                            }
                            catch (GApp.DAL.Exceptions.DataBaseEntityValidationException e)
                            {
                                string msg = "Insertion automatique : " + e.Message;
                                this.Report.AddMessage(msg, MessagesService.MessageTypes.Add_Error);
                                throw e;
                            }
                        }
                        // If the entity support NotComplet refereence by TrainingYear
                        if (importAttribute != null && (importAttribute as ImportAttribute).NotCompleteReference)
                        {
                            if (unitOfWork.CurrentTrainingYear == null)
                            {
                                string msg = "You mut chose a training year";
                                throw new ImportException(msg);

                            }
                            string new_referene = navigationMemberReference + "-" + unitOfWork.CurrentTrainingYear.Reference;
                            vlaue = navigationProperty_set.Local.OfType<BaseEntity>().Where(e => e.Reference == new_referene).FirstOrDefault();
                        }
                        if (vlaue == null)
                        {
                            // ImportException 
                            string msg = string.Format(msg_ImportService.error_reference_of_object_not_exist_in_database,
                            dataRow.Table.Rows.IndexOf(dataRow) + 1,
                            navigationMemberReference, local_name_of_property);
                            this.Report.AddMessage(msg, MessagesService.MessageTypes.Error);
                            //  throw new ImportException(msg);
                        }
                    }
                    propertyInfo.SetValue(entity, vlaue);
                }
                // if ManyToMany


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

    }
}
