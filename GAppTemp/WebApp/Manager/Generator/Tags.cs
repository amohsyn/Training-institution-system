using GApp.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GApp.WebApp.Manager.Generator
{

    public class Tags<T> where T : DbContext, new()
    {

        public static string EditorFor(string ViewDataTypeName, string PropertyName)
        {
            // GetTypeModel
            Type typeModel = Type.GetType(ViewDataTypeName + ",TrainingIS.Entities");
            if (typeModel == null)
            {
                string msg = string.Format("Can't louad the Type {0} by Name", ViewDataTypeName);
                throw new GAppException(msg);
            }
            else
            {
                PropertyInfo propertyInfo = typeModel.GetProperty(PropertyName);
                
                return EditorFor(propertyInfo);
            }
               
        }
        public static string EditorFor(PropertyInfo propertyInfo)
        {
            string EditorFor_Value = String.Empty;
            List<string> foreignKeies = new EntityService<T>().GetForeignKeiesIds(propertyInfo.ReflectedType);

            // Default Editor
            EditorFor_Value = "@Html.EditorFor(model => model." + propertyInfo.Name + ", new { htmlAttributes = new { @class = \"form-control\" } })";
            string htmlAttributes = "new { @class = \"form-control\" }";

            // Read DataTypeAttribute
            DataTypeAttribute dataTypeAttribute = null;
            var dataTypeAttribute_obj = propertyInfo.GetCustomAttribute(typeof(DataTypeAttribute));
            if (dataTypeAttribute_obj != null) dataTypeAttribute = (DataTypeAttribute)dataTypeAttribute_obj;
            
            // If DataTime
            if (propertyInfo.PropertyType.Name == typeof(DateTime).Name 
                || propertyInfo.PropertyType.Name == typeof(DateTime?).Name)
            {
                if (dataTypeAttribute != null && dataTypeAttribute.DataType == DataType.Time)
                { // time => default
                }
                else
                {
                    EditorFor_Value = "@Html.TextBoxFor(model => model." + propertyInfo.Name + ", new { @class = \"form-control has-feedback-left datetimepicker\" })";
                    EditorFor_Value += "\n <span class=\"fa fa-calendar-o form-control-feedback left\" aria-hidden=\"true\"></span>";
                    return EditorFor_Value;
                }
            }

            // if Enum
            if (propertyInfo.PropertyType.IsEnum)
            {
                string frm = "@(Html.EnumDropDownList<{0}>(\"{1}\", {2}))";
                EditorFor_Value = string.Format(frm, propertyInfo.PropertyType.Name,propertyInfo.Name , htmlAttributes);
                return EditorFor_Value;
            }

            // if ForeignKey
            if (foreignKeies.Contains(propertyInfo.Name))
            {
                EditorFor_Value =  string.Format("@Html.DropDownList(\"{0}\", null, htmlAttributes: {1} )", 
                    propertyInfo.Name, htmlAttributes);
                return EditorFor_Value;
            }

            // return default EditorFor
            return EditorFor_Value;
        }
    }
}
