using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GApp.Entities
{
    public static class BaseEntityExtentions
    {


        /// <summary>
        /// Update Entity from a Object according to BindAttribute
        /// </summary>
        public static void UpdateEntity(this BaseEntity baseEntity, object ObjectValue,BindAttribute bindAttribute)
        {
            var destinationProperties = baseEntity.GetType().GetProperties();
            foreach (PropertyInfo ValuePropertyInfo in ObjectValue.GetType().GetProperties())
            {
                PropertyInfo destinationPropertyInfo = destinationProperties
                    .Where(p => p.Name == ValuePropertyInfo.Name && p.PropertyType == ValuePropertyInfo.PropertyType)
                    .FirstOrDefault();

                // If ValueProperty exsit in destinationProperty
                if (destinationPropertyInfo != null)
                {
                    if (bindAttribute.IsPropertyAllowed(ValuePropertyInfo.Name))
                    {
                        var Value = ValuePropertyInfo.GetValue(ObjectValue);
                        destinationPropertyInfo.SetValue(baseEntity, Value);
                    }

                }

            }
    
         
        }
    }
}
