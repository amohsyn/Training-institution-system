﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TrainingIS.WebApp.Helpers
{
    public class ImportHelper
    {
        public static void FillBeanFieldsByDataRow(Object bean, DataRow dataRow)
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
                FillBeanFieldsByDataRow(model, dr);
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