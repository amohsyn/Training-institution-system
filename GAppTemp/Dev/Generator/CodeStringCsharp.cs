using GApp.Core.MetaDatas.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GApp.Dev.Generator
{
    public class CodeStringCsharp
    {

        public string Property(PropertyInfo propertyInfo)
        {
            string code = "";
            string attribute_code = "";

            bool isNullable = false;
            Type underlyingType = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
            if (underlyingType != null) isNullable = true;
            Type property_type = isNullable ? underlyingType : propertyInfo.PropertyType;


            // Required Attribute
            Attribute required_attribute = propertyInfo.GetCustomAttribute(typeof(RequiredAttribute));
            if(required_attribute != null)
            {
                string code_required_attribute = "[Required]";
                attribute_code = this.Add_Line(attribute_code, code_required_attribute);
            }

            // Unique Attribute
            Attribute uniqueAttribute = propertyInfo.GetCustomAttribute(typeof(UniqueAttribute));
            if (uniqueAttribute != null)
            {
                string code_uniqueAttribute = "[Unique]";
                attribute_code = this.Add_Line(attribute_code, code_uniqueAttribute);
            }

            // Attributes code
            code = this.Add_Line(code, attribute_code);

            // Property code
            string property_format = "public {0} {1}  {{set; get;}}";
            string property_code = string.Format(property_format, property_type.Name, propertyInfo.Name);
            code = this.Add_Line(code, property_code);

        
            return code;

        }



        private string Add_Line(string Code, string CodeLine)
        {
            if (!string.IsNullOrEmpty(Code))
                Code += "\n\t\t";
            Code += CodeLine;
          return Code;
        }
    }
}
