using GApp.Core.MetaDatas.Attributes;
using GApp.Exceptions;
using GApp.WebApp.Manager.Views.Attributes;
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
    public enum ViewName {
        None,
        Create,
        Edit,
        Index
    }
    public class Tags<T> where T : DbContext, new()
    {
        private RelationShip_CodeGenerator<T> _RelationShip_CodeGenerator;
        public Tags(Type EntityType)
        {
            _RelationShip_CodeGenerator = new RelationShip_CodeGenerator<T>(EntityType);
        }

      

        /// <summary>
        /// Editor for ModelView or Entity
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public string EditorFor(PropertyInfo propertyInfo)
        {
            string EditorFor_Value = String.Empty;
            List<string> foreignKeiesIds = this._RelationShip_CodeGenerator.ForeignKeiesIds;

            // Default Editor
            EditorFor_Value = "@Html.EditorFor(model => model." + propertyInfo.Name + ", new { htmlAttributes = new { @class = \"form-control\" } })";
            string htmlAttributes = "new { @class = \"form-control\" }";

            // if ForeignKey
            if (foreignKeiesIds.Contains(propertyInfo.Name))
            {
                EditorFor_Value = string.Format("@Html.DropDownList(\"{0}\", null,\"\", htmlAttributes: {1} )",
                    propertyInfo.Name, htmlAttributes);
                return EditorFor_Value;
            }

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
                EditorFor_Value = string.Format(frm, propertyInfo.PropertyType.Name, propertyInfo.Name, htmlAttributes);
                return EditorFor_Value;
            }

           

            // if Meny Realtion
            if (propertyInfo.IsDefined(typeof(ManyAttribute)))
            {
                ManyAttribute manyAttribute = propertyInfo.GetCustomAttribute(typeof(ManyAttribute)) as ManyAttribute;

                if (propertyInfo.IsDefined(typeof(SelectFilterAttribute)))
                {
                    SelectFilterAttribute selectFilterAttribute = propertyInfo.GetCustomAttributes(typeof(SelectFilterAttribute)).LastOrDefault() as SelectFilterAttribute;
                    string frm = "@Html.EditFor_Select_With_Filter(model => model.{0}, Model.{1},true);";
                    string All_Data_Property = "All_" + manyAttribute.TypeOfEntity.Name.Pluralize();
                    EditorFor_Value = string.Format(frm, propertyInfo.Name, All_Data_Property);
                    return EditorFor_Value;
                }

               

                string list_box_format = "@Html.ListBoxFor(model => model.{0}, Model.All_{1})";
                string check_box_format = "";
                EditorFor_Value = string.Format(list_box_format, propertyInfo.Name, manyAttribute.TypeOfEntity.Name.Pluralize(), htmlAttributes);
                return EditorFor_Value;

            }
            else
            {
                // if SelectFilter Defined
                if (propertyInfo.IsDefined(typeof(SelectFilterAttribute)))
                {
                    SelectFilterAttribute selectFilterAttribute = propertyInfo.GetCustomAttributes(typeof(SelectFilterAttribute)).LastOrDefault() as SelectFilterAttribute;
                    string frm = "@Html.EditFor_Select_With_Filter(model => model.{0}, Model.{1},false);";
                    string All_Data_Property = "Data_" + selectFilterAttribute.Filter_HTML_Id;
                    EditorFor_Value = string.Format(frm, propertyInfo.Name, All_Data_Property);
                    return EditorFor_Value;
                }
            }

          


            // return default EditorFor
            return EditorFor_Value;
        }

        /// <summary>
        /// Editor for ModelView or Entity
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public string DisplayFor(string ModelVarName, PropertyInfo propertyInfo)
        {


            string Display_For_Value = String.Empty;
            List<string> foreignKeyNames= this._RelationShip_CodeGenerator.ForeignKeyNames;


            string model_format = "@Html.GAppDisplayFor(model => model.{0})";
            string foreach_format = "@Html.GAppDisplayFor(modelItem => {0}.{1})";

            // Default 
            if (string.IsNullOrEmpty(ModelVarName))
            {
                Display_For_Value = string.Format(model_format, propertyInfo.Name);
            }
            else
            {
                Display_For_Value = string.Format(foreach_format, ModelVarName, propertyInfo.Name);
            }
               
            
           
            // if ForeignKey
            if (foreignKeyNames.Contains(propertyInfo.Name))
            {
                if (string.IsNullOrEmpty(ModelVarName))
                {
                    Display_For_Value = string.Format("@{0}.{1}.{2}", "Model", propertyInfo.Name, "ToString()");
                }
                else
                {
                    Display_For_Value = string.Format("@{0}.{1}.{2}", ModelVarName, propertyInfo.Name, "ToString()");
                }
            }

            // return default EditorFor
            return Display_For_Value;
        }

        //public string LabelFor(PropertyInfo propertyInfo, ViewName ViewName)
        //{
        //    string format = "@Html.LabelFor(model => model.{0}, htmlAttributes: new { @class = \"control-label col-md-2\" })";
        //    string property_full_name = "";

        //    Type createModelView_Type = this.EntityGeneratorWork.getCreateModelView_Type();

        //    switch (ViewName)
        //    {
        //        case ViewName.Create:
        //            if(createModelView_Type != null)

        //            property_full_name = "";
        //            break;
        //        case ViewName.Edit:
        //            break;
        //        case ViewName.Index:
        //            break;
        //        default:
        //            property_full_name = propertyInfo.Name;
        //            break;
        //    }
        //    string labelValue = string.Format(format, property_full_name);
        //    return labelValue;


        //}
        //public static string EditorFor(string ViewDataTypeName, string PropertyName)
        //{
        //    // GetTypeModel
        //    Type typeModel = Type.GetType(ViewDataTypeName + ",TrainingIS.Entities");
        //    if (typeModel == null)
        //    {
        //        string msg = string.Format("Can't louad the Type {0} by Name", ViewDataTypeName);
        //        throw new GAppException(msg);
        //    }
        //    else
        //    {
        //        PropertyInfo propertyInfo = typeModel.GetProperty(PropertyName);

        //        return EditorFor(propertyInfo);
        //    }

        //}

    }
}
