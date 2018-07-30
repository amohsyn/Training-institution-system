using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Reflection
{
    public static class PropertyInfoExtantions
    {
        /// <summary>
        /// Get localName of the property
        /// the localName is configured by DisplayAttribute
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static string getLocalName(this PropertyInfo propertyInfo)
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
                if (local_name_of_property == null)
                    local_name_of_property = propertyInfo.Name;
            }

            return local_name_of_property;
        }
    }
}
