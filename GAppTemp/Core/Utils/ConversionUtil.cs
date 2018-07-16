using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace GApp.Core.Utils
{
    public class ConversionUtil
    {
        public static T To<T>(object value)
        {
            if (Convert.IsDBNull(value))
            {
                return default(T);
            }

            if (typeof(T) == typeof(char) || typeof(T) == typeof(char?))
            {
                value = ((string)value)[0];
            }
            else if (typeof(T) == typeof(bool))
            {
                if (value != null && ("Y".Equals(value.ToString(), StringComparison.OrdinalIgnoreCase) || "TRUE".Equals(value.ToString(), StringComparison.OrdinalIgnoreCase)))
                    value = true;
                else
                    value = false;
            }
            else if (typeof(T) == typeof(string))
            {
                if (value == null)
                {
                    value = string.Empty;
                }
            }
            try
            {
                return (T)value;
            }
            catch
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }


        public static void FillBeanFieldsByDataRow_PrimitiveValue(Object bean, DataRow dataRow)
        {
            Type t = bean.GetType();
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                string name = prop.Name;
                if (prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(string) || prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(DateTime)
                    || prop.PropertyType == typeof(int?) || prop.PropertyType == typeof(decimal?) || prop.PropertyType == typeof(DateTime?) || prop.PropertyType == typeof(Guid) || prop.PropertyType == typeof(Guid?))
                {
                    if (!dataRow.Table.Columns.Contains(name)) continue;

                    if (dataRow.IsNull(name))
                    {
                        if (prop.PropertyType == typeof(string))
                        {
                            t.GetProperty(name).SetValue(bean, "", null);
                        }
                        continue;
                    }
                    t.GetProperty(name).SetValue(bean, HackType(dataRow[name], prop.PropertyType), null);
                }
            }
        }

        public static List<T> TableToList<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T model = Activator.CreateInstance<T>();
                FillBeanFieldsByDataRow_PrimitiveValue(model, dr);
                list.Add(model);
            }
            return list;
        }


        // Cette classe juge la conversion du type nullable, sinon elle signale une erreur.
        private static object HackType(object value, Type conversionType)
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
    }
}