using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace GApp.Web.Manager.Scaffold
{
    public class ScaffoldFunctions
    {
        public static string function1()
        {
            return "bonjour scaffold function";
        }

        public static string EditorFor(string ViewDataTypeName, string PropertyName)
        {
            string returnValue = String.Empty;
            returnValue = "@Html.EditorFor(model => model." + PropertyName + ")";

            // DataType Attribute

            DataTypeAttribute dataTypeAttribute = null;
            Type typeModel = Type.GetType(ViewDataTypeName + ",TrainingIS.Entities");
            if (typeModel != null)
            {
                dataTypeAttribute = (DataTypeAttribute)Attribute.GetCustomAttribute(typeModel.GetProperty(PropertyName), typeof(DataTypeAttribute));
                if (dataTypeAttribute != null)
                {
                    if(dataTypeAttribute.DataType == DataType.Time)
                    {
                        returnValue = "@Html.TextBoxFor(model => model." + PropertyName + ", new { @class = \"form-control has-feedback-left datetimepicker\" })";
                        returnValue += "\n <span class=\"fa fa-calendar-o form-control-feedback left\" aria-hidden=\"true\"></span>";
                    }
                }
            }
            return returnValue;
        }

          
      


    }
}
