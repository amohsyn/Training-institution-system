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

        public string Property(PropertyInfo propertyInfo , List<string> namesSpaces)
        {
            string code = "";
            string attribute_code = "";

            bool isNullable = false;
            Type underlyingType = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
            if (underlyingType != null) isNullable = true;
            Type property_type = isNullable ? underlyingType : propertyInfo.PropertyType;


            // Required Attribute
            Attribute required_attribute = propertyInfo.GetCustomAttribute(typeof(RequiredAttribute));
            if (required_attribute != null)
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

            // Dispaly Attribute
            string code_display_attribure = this.Code_DispalyAttribute(propertyInfo, namesSpaces);
            attribute_code = this.Add_Line(attribute_code, code_display_attribure);



            // Attributes code
            code = this.Add_Line(code, attribute_code);

            // Property code
            string property_format = "public {0} {1}  {{set; get;}}";
            string property_code = string.Format(property_format, property_type.Name, propertyInfo.Name);
            code = this.Add_Line(code, property_code);


            return code;

        }


        #region Attributes
        public string Code_DispalyAttribute(PropertyInfo propertyInfo, List<string> namesSpaces)
        {
            string code = "";
            Attribute attribute = propertyInfo.GetCustomAttribute(typeof(DisplayAttribute));
            if (attribute == null) return code;
            DisplayAttribute displayAttribute = attribute as DisplayAttribute;
            if (displayAttribute.ResourceType == null) return code;

            string code_format = "[Display(Name = \"{0}\", ResourceType = typeof({1}))]";
            code = string.Format(code_format, displayAttribute.Name,displayAttribute.ResourceType?.Name);

            if (!namesSpaces.Contains(displayAttribute.ResourceType.Namespace))
                namesSpaces.Add(displayAttribute.ResourceType.Namespace);

            return code;
        }
        #endregion

        /// <summary>
        /// Generate code string Properties
        /// </summary>
        /// <param name="propertyInfos">Properties to generate</param>
        /// <param name="NamesSpaces">out put Namespace</param>
        /// <param name="Code_properties">out put code string propertoes</param>
        public void GenerateCodeProperties(List<PropertyInfo> propertyInfos, List<string> NamesSpaces, List<string> Code_properties)
        {
            foreach (var item in propertyInfos)
            {
                Code_properties.Add(this.Property(item, NamesSpaces));
            }
        }




        private string Add_Line(string Code, string CodeLine)
        {
            if (string.IsNullOrEmpty(CodeLine)) return Code;

            if (!string.IsNullOrEmpty(Code))
                Code += "\n\t\t";

            Code += CodeLine;
            return Code;
        }
    }
}
