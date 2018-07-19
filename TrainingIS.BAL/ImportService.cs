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
    /// Import DataTable to DataBase
    /// </summary>
    public class ImportService
    {
        private TrainingISModel Context;
        private Type TypeEntity;
         

        public ImportService(Type TypeEntity, TrainingISModel Context)
        {
            this.Context = Context;
            this.TypeEntity = TypeEntity;
        }
 
        public void Fill_PrimitiveValue(Object bean,
            List<string> ForeignKeys,
            DataRow dataRow)
        {
            PropertyInfo[] props = this.TypeEntity.GetProperties();

            foreach (PropertyInfo propertyInfo in props)
            {

                if (propertyInfo.PropertyType.IsPrimitive || propertyInfo.PropertyType == typeof(string) || propertyInfo.PropertyType == typeof(decimal) || propertyInfo.PropertyType == typeof(DateTime)
                    || propertyInfo.PropertyType == typeof(int?) || propertyInfo.PropertyType == typeof(decimal?) || propertyInfo.PropertyType == typeof(DateTime?) || propertyInfo.PropertyType == typeof(Guid) || propertyInfo.PropertyType == typeof(Guid?))
                {

                    // ForeignKeys not exist in DataTable ans it well confused with 
                    // NavigationPropeorty
                    if (ForeignKeys.Contains(propertyInfo.Name)) continue;

                    string name_of_property = propertyInfo.Name;
                    string local_name_of_property = this.getLocalNameOfProperty(propertyInfo);
                     
                    // Set Value
                    if (!dataRow.Table.Columns.Contains(local_name_of_property)) continue;

                    if (dataRow.IsNull(local_name_of_property))
                    {
                        if (propertyInfo.PropertyType == typeof(string))
                        {
                            this.TypeEntity.GetProperty(name_of_property).SetValue(bean, "", null);
                        }
                        continue;
                    }

                    object value = dataRow[local_name_of_property];
                    this.TypeEntity.GetProperty(name_of_property).SetValue(bean, HackType(value, propertyInfo.PropertyType), null);
                }
            }
        }

       

       
        public string Fill_NonPrimitiveValue(Trainee entity,
            List<string> navigationPropertiesNames, 
            DataRow dataRow )
        {
            string msg = "";
            var Properties = this.TypeEntity.GetProperties();

            foreach (PropertyInfo propertyInfo in Properties)
            {
                if (navigationPropertiesNames.Contains(propertyInfo.Name))
                {

                    string name_of_property = propertyInfo.Name;
                    string local_name_of_property = this.getLocalNameOfProperty(propertyInfo);

                    // continue if the property not exist in DataRow
                    if (!dataRow.Table.Columns.Contains(local_name_of_property)) continue;


                    // Dynamic type Algo

                    //// if One to One or OneToMany
                    string navigationMemberReference = dataRow[local_name_of_property].ToString();
                    if (string.IsNullOrEmpty(navigationMemberReference))
                    {
                        propertyInfo.SetValue(entity, null);
                    }
                    else
                    {
                        Type navigationMemberType = propertyInfo.PropertyType;
                        var navigationProperty_set = this.Context.Set(propertyInfo.PropertyType);
                        navigationProperty_set.Load();
                        var vlaue = navigationProperty_set.Local.OfType<BaseEntity>().Where(e => e.Reference == navigationMemberReference).FirstOrDefault();
                        if (vlaue == null)
                        {
                            msg += string.Format(" ! erreur à la ligne {0} : la référence {1} de l'objet {2} n'exist pas dans la base de données", dataRow.Table.Rows.IndexOf(dataRow) + 1, navigationMemberReference, local_name_of_property) + "<br>";
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

            return msg;
        }

        public string Fill_Value(Trainee entity, 
            List<string> navigationPropertiesNames,
            List<string> ForeignKeys,
            DataRow dataRow)
        {
            string msg = "";
            // Fill primitive value from DataRow
            this.Fill_PrimitiveValue(entity, ForeignKeys, dataRow);

            // Fill none primitive value
            msg = this.Fill_NonPrimitiveValue(entity, navigationPropertiesNames, dataRow);
            return msg;
        }

        private  object HackType(object value, Type conversionType)
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
