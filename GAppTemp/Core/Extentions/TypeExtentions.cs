using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class TypeExtentions
    {
        /// <summary>
        /// Get a list of PropertyInfo that has a specific attribute
        /// </summary>
        /// <param name="AttributeType"></param>
        /// <returns>Null if the attribute not exist in all properties</returns>
        public static List<PropertyInfo> GetProperties(this Type type, Type attributeType)
        {
            List<PropertyInfo> properties = new List<PropertyInfo>();

            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                Attribute attribute = propertyInfo.GetCustomAttribute(attributeType);
                if (attribute != null)
                    properties.Add(propertyInfo);
            }

            return properties;

        }

    }
}
