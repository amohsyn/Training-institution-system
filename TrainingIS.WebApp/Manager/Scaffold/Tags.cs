using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS.WebApp.Manager.Scaffold
{
    
    public class Tags
    {
        public static string EditorFor(string ViewDataTypeName, string PropertyName)
        {
            string returnValue = String.Empty;
            // Default Editor
            returnValue = "@Html.EditorFor(model => model." + PropertyName + ", new { htmlAttributes = new { @class = \"form-control\" } })";

            // GetTypeModel
            Type typeModel = Type.GetType(ViewDataTypeName + ",TrainingIS.Entities");
            if (typeModel == null) return returnValue;

            // GetPropertyInfo
            PropertyInfo propertyInfo = typeModel.GetProperty(PropertyName);


            // Read Attributes MetaData
            DataTypeAttribute dataTypeAttribute = (DataTypeAttribute)Attribute.GetCustomAttribute(typeModel.GetProperty(PropertyName), typeof(DataTypeAttribute));


            if (propertyInfo.PropertyType.Name == typeof(DateTime).Name)
            {
                if (dataTypeAttribute != null && dataTypeAttribute.DataType == DataType.Time)
                {
                     // time => default
                }
                else
                {
                    returnValue = "@Html.TextBoxFor(model => model." + PropertyName + ", new { @class = \"form-control has-feedback-left datetimepicker\" })";
                    returnValue += "\n <span class=\"fa fa-calendar-o form-control-feedback left\" aria-hidden=\"true\"></span>";
                }
            }


            return returnValue;
        }
    }
}
