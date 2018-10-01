using GApp.BLL.Services;
using GApp.Core.Context;
using GApp.Core.Localization;
using GApp.DAL;
using GApp.Entities;
using GApp.Exceptions;
using GApp.Models.DataAnnotations;
using System;
using System.Collections;
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
using TrainingIS.BLL.Services;
using TrainingIS.BLL.Services.Import;
using TrainingIS.DAL;
using TrainingIS.Entitie_excludes;
using TrainingIS.Entities;
using TrainingIS.Entities.Base;

namespace TrainingIS.BLL
{
    /// <summary>
    /// Convert DataTable to Entities and Save
    /// with report generation
    /// </summary>
    public class ImportService : BaseImportExportService
    {
        public static string IMPORT_PROCESS_KEY = "IMPORT_PROCESS_KEY";

        public GAppContext GAppContext { set; get; }
        public ImportReport Report { set; get; }

        public ImportService(DataTable DataTable, Type TypeEntity, GAppContext GAppContext):base(TypeEntity)
        {
            this.GAppContext = GAppContext;
            this.GAppContext.Session.Add(IMPORT_PROCESS_KEY, true);
            Report = new ImportReport(this.EntityType, DataTable);
        }

        #region Fill DatRow
        public void Fill_Value(object entity, DataRow dataRow, UnitOfWork<TrainingISModel> unitOfWork)
        {

            // Delete propertiesShortcuts traitements
            List<EntityPropertyShortcut> propertiesShortcuts = new List<EntityPropertyShortcut>();


            // Fill primitive value from DataRow
            this.Fill_PrimitiveValue(entity, propertiesShortcuts, dataRow, unitOfWork);

            // Fill none primitive value
            this.Fill_NonPrimitiveValue(entity, propertiesShortcuts, dataRow, unitOfWork);
        }
        public void Fill_PrimitiveValue(Object bean,
            List<EntityPropertyShortcut> propertiesShortcuts,
            DataRow dataRow, UnitOfWork<TrainingISModel> unitOfWork)
        {
            PropertyInfo[] props = this.EntityType.GetProperties();

            foreach (PropertyInfo propertyInfo in props)
            {
                if (
                    propertyInfo.PropertyType.IsPrimitive
                    || propertyInfo.PropertyType == typeof(string)
                    || propertyInfo.PropertyType == typeof(decimal)
                    || propertyInfo.PropertyType == typeof(DateTime)
                    || propertyInfo.PropertyType == typeof(int?)
                    || propertyInfo.PropertyType == typeof(decimal?)
                    || propertyInfo.PropertyType == typeof(DateTime?)
                    || propertyInfo.PropertyType == typeof(Guid)
                    || propertyInfo.PropertyType == typeof(Guid?))
                {
                    // ForeignKeys not exist in DataTable ans it well confused with 
                    // NavigationPropeorty
                    if (this.ForeignKeiesIds.Contains(propertyInfo.Name)) continue;

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
                            this.EntityType.GetProperty(name_of_property).SetValue(bean, "", null);
                        }
                        continue;
                    }

                    object value = dataRow[column_index];
                    this.EntityType.GetProperty(name_of_property).SetValue(bean, HackType(value, propertyInfo.PropertyType), null);
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
            DataRow dataRow, UnitOfWork<TrainingISModel> unitOfWork)
        {
            var Properties = this.EntityType.GetProperties();

            foreach (PropertyInfo propertyInfo in Properties)
            {

                if (
                    !this.ForeignKeiesNames.Contains(propertyInfo.Name)
                    && !this.ManyKeiesNames.Contains(propertyInfo.Name)
                    && !propertyInfo.PropertyType.IsEnum) continue;


                string name_of_property = propertyInfo.Name;
                string local_name_of_property = propertyInfo.getLocalName();

                // Delete ShortcutsNames traitement
                List<string> ShortcutsNames = new List<string>();

                int column_index = this.FindColumnIndex(dataRow,
                    name_of_property, local_name_of_property, ShortcutsNames);

                // continue if the property not exist in the dataRow
                if (column_index == -1) continue;

                // if the propertu is Enumeration
                if (propertyInfo.PropertyType.IsEnum)
                {
                    string localValue = dataRow[column_index].ToString();
                    try
                    {
                        string value_string = GAppEnumLocalization.GetValue_From_LocalValue(propertyInfo.PropertyType, localValue);

                        object value = Enum.Parse(propertyInfo.PropertyType, value_string);

                        propertyInfo.SetValue(entity, value);
                    }
                    catch (GAppException e)
                    {
                        //[Bug] Localization
                        string msg = "Valeur n'exist pas : " + e.Message;
                        this.Report.AddMessage(msg, MessagesService.MessageTypes.Add_Error);
                        throw e;
                    }


                    continue;
                }


                //// if One to One or OneToMany
                if (this.ForeignKeiesNames.Contains(propertyInfo.Name))
                {
                    string navigationMemberReference = dataRow[column_index].ToString();
                    if (string.IsNullOrEmpty(navigationMemberReference))
                    {
                        propertyInfo.SetValue(entity, null);
                        continue;
                    }
                    else
                    {
                        if(propertyInfo.PropertyType.Name == typeof(ApplicationUser).Name)
                        {
                            var Owner = new UserBLO(unitOfWork, this.GAppContext).FindByLogin(navigationMemberReference);
                            propertyInfo.SetValue(entity, Owner);
                            continue;
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
                                    BLO_Context BLO_Context = new BLO_Context();
                                    Type navigate_TypeBLO = BLO_Context.Get_BLO_Type(navigationMemberType);
                                    object[] param_blo = { unitOfWork, this.GAppContext };

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
                                if (vlaue == null)
                                {
                                    // ImportException 
                                    string msg = string.Format(msg_ImportService.error_reference_of_object_not_exist_in_database,
                                    dataRow.Table.Rows.IndexOf(dataRow) + 1,
                                    navigationMemberReference, local_name_of_property);
                                    this.Report.AddMessage(msg, MessagesService.MessageTypes.Error, dataRow);
                                    //  throw new ImportException(msg);
                                }
                            }

                            propertyInfo.SetValue(entity, vlaue);
                            continue;
                        }
                     


                       
                    }
                }

                // if ManyToMany
                if (this.ManyToManyKeiesNames.Contains(propertyInfo.Name))
                {
                    Type MenyEntity_Type = propertyInfo.PropertyType.GenericTypeArguments.First();

                    string navigationMemberReference = dataRow[column_index].ToString();
                    List<string> references_values = navigationMemberReference.Split(",".ToArray()).ToList();


                    // Delete existante values
                    IList List_Values = propertyInfo.GetValue(entity) as IList;
                    if (List_Values == null)
                    {
                        // Create List_Values instance
                        List<Type> param = new List<Type>();
                        param.Add(MenyEntity_Type);
                        var list_value_type = typeof(List<>).MakeGenericType(param.ToArray());
                        var list_value = Activator.CreateInstance(list_value_type);
                        propertyInfo.SetValue(entity, list_value);
                        List_Values = list_value as IList;
                    }
                    List_Values.Clear();

                    // Load data
                    var MenyEntity_set = unitOfWork.context.Set(MenyEntity_Type);
                    MenyEntity_set.Load();

                    foreach (var reference in references_values)
                    {
                        if (string.IsNullOrEmpty(reference)) continue;

                        var item_value = MenyEntity_set.Local.OfType<BaseEntity>().Where(e => e.Reference == reference).FirstOrDefault();
                        // if the item_value not exist in dataBase
                        if (item_value == null)
                        {
                            // ImportException 
                            string msg = string.Format(msg_ImportService.error_reference_of_object_not_exist_in_database,
                            dataRow.Table.Rows.IndexOf(dataRow) + 1,
                            item_value, local_name_of_property);
                            this.Report.AddMessage(msg, MessagesService.MessageTypes.Error, dataRow);
                        }
                        else
                        {
                            List_Values.Add(item_value);
                        }
                    }

                    continue;




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

    }
}
