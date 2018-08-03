﻿using GApp.Core.MetaDatas.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GApp.Dev.Generator
{
    public enum Operations {
        Show,
        Edit
    }
    public class CodeStringCsharp
    {
        public Operations Operation { get; }

        public CodeStringCsharp(Operations operation = Operations.Show)
        {
            this.Operation = operation;
        }


        public string Property(PropertyInfo propertyInfo , List<string> namesSpaces)
        {
            string code_result = "";
            string attributes_codes = "";
            string property_code = "";
            string simple_property_format = "public {0} {1}  {{set; get;}}";
            string genetic_property_format = "public List<{0}> {1}  {{set; get;}}";



            // Find a property Type
            bool isNullable = false;
            Type underlyingType = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
            if (underlyingType != null) isNullable = true;
            Type property_type = isNullable ? underlyingType : propertyInfo.PropertyType;

            // if Generic List
            if(propertyInfo.PropertyType.IsGenericType && property_type.GenericTypeArguments.Count() > 0)
            {
                Type ParametetType = property_type.GenericTypeArguments.First();

                // Many Attribute
                string code_Many_attribure = this.Code_ManyAttribute(propertyInfo, namesSpaces, ParametetType);
                if (this.Operation == Operations.Edit)
                {
                    if (string.IsNullOrEmpty(code_Many_attribure))
                    {
                        // id [Many] not exist, we add one to ModelView
                        string code_Many_attribure_format = "[Many (TypeOfEntity = typeof({0}))]";
                        code_Many_attribure = string.Format(code_Many_attribure_format, ParametetType.Name);
                        attributes_codes = this.Add_Line(attributes_codes, code_Many_attribure);
                    }
                    else
                    {
                        attributes_codes = this.Add_Line(attributes_codes, code_Many_attribure);
                    }

                }


                if (this.Operation == Operations.Show)
                {
                    property_code = string.Format(genetic_property_format, ParametetType.Name, propertyInfo.Name);
                }
                if (this.Operation == Operations.Edit)
                {
                    string selected_vlaues_format = "public List<String> Selected_{0} {{set; get;}}";
                    string all_values_format = "public List<{0}> All_{1}  {{set; get;}}";

                    string selected_vlues = string.Format(selected_vlaues_format, propertyInfo.Name);

                    // All Values
                    string all_values = "";
                    string all_values_display = "[Display(AutoGenerateField = false)]"  ;
                    string all_values_property = string.Format(all_values_format, "System.Web.Mvc.SelectListItem", propertyInfo.Name);
                    all_values = this.Add_Line(all_values, all_values_display);
                    all_values = this.Add_Line(all_values, all_values_property);
                    property_code = this.Add_Line(property_code, selected_vlues);
                    property_code = this.Add_Line(property_code, all_values);
                }
            }
            else
            {
                property_code = string.Format(simple_property_format, property_type.Name, propertyInfo.Name);
            }

            // Required Attribute
            Attribute required_attribute = propertyInfo.GetCustomAttribute(typeof(RequiredAttribute));
            if (required_attribute != null)
            {
                string code_required_attribute = "[Required]";
                attributes_codes = this.Add_Line(attributes_codes, code_required_attribute);
            }

            // Unique Attribute
            Attribute uniqueAttribute = propertyInfo.GetCustomAttribute(typeof(UniqueAttribute));
            if (uniqueAttribute != null)
            {
                string code_uniqueAttribute = "[Unique]";
                attributes_codes = this.Add_Line(attributes_codes, code_uniqueAttribute);
            }

            // Dispaly Attribute
            string code_display_attribure = this.Code_DispalyAttribute(propertyInfo, namesSpaces);
            attributes_codes = this.Add_Line(attributes_codes, code_display_attribure);



            // Attributes code
            code_result = this.Add_Line(code_result, attributes_codes);

            // Property code
            code_result = this.Add_Line(code_result, property_code);

            return code_result;

        }

        private string Code_ManyAttribute(PropertyInfo propertyInfo, List<string> namesSpaces , Type typeofEntity)
        {
            string code = "";
            Attribute attribute = propertyInfo.GetCustomAttribute(typeof(ManyAttribute));
            if (attribute == null) return code;
            ManyAttribute manyAttribute = attribute as ManyAttribute;

            if(manyAttribute.userInterfaces == default(UserInterfaces))
            {
                string code_format = "[Many]";
                code = string.Format(code_format);
                return code;
            }
            else
            {
                string code_format = "[Many(userInterfaces = UserInterfaces.{0} , TypeOfEntity = typeof({1}))]";
                code = string.Format(code_format, manyAttribute.userInterfaces.ToString(), typeofEntity.Name);
                return code;
            }
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
