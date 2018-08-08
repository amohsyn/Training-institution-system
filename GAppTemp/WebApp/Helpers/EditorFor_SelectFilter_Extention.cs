using GApp.Core.Entities.ModelsViews;
using GApp.Entities;
using GApp.Exceptions;
using GApp.WebApp.Manager.Views.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace GApp.WebApp.Helpers
{
    public static class EditorFor_SelectFilter_Extention
    {


        public static MvcHtmlString Select_Tag<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            List<BaseEntity> Data,
            bool isMultiple,
            bool AddBlank = true
            )
        {
            // Template
            //< select id = "Selected_ActionControllerApps" name = "Selected_ActionControllerApps" multiple >
            //            @foreach(var actionControllerApp in Model.All_ActionControllerApps)
            //    {
            //        < option value = "@actionControllerApp.Id" data-controllerappid = "@actionControllerApp.ControllerApp.Id" > @actionControllerApp.Code </ option >
            //    }
            //</ select >

            // Get the Property from Expression
            var member = expression.Body as MemberExpression;
            PropertyInfo property = member.Member as PropertyInfo;

            // Get Selected Values from ViewData
            List<String> Selected;
            var view_data_value = ModelMetadata.FromLambdaExpression(
               expression, htmlHelper.ViewData
            ).Model ;
            if(view_data_value is  List<String>)
            {
                Selected = view_data_value as List<String>;
            }
            else
            {
                Selected = new List<String>();
                Selected.Add(view_data_value.ToString());  
            }
            
            // Get SelectFilter from Attribute
            SelectFilterAttribute SelectFilterAttribute = member.Member.GetCustomAttributes(typeof(SelectFilterAttribute), false).FirstOrDefault() as SelectFilterAttribute;
            string filteredBy_Id = "";
            string data_FilteredBy_name = "";
            if (SelectFilterAttribute != null)
            {
                filteredBy_Id = SelectFilterAttribute.Filter_HTML_Id;
                data_FilteredBy_name  = string.Format("data-{0}", filteredBy_Id.ToLower());
            }


            // Create Select Tag
            string SelectTagId = property.Name;
            TagBuilder tagSelect = new TagBuilder("select");
            tagSelect.Attributes.Add("id", SelectTagId);
            tagSelect.Attributes.Add("name", SelectTagId);
            tagSelect.Attributes.Add("class", "form-control");
            if (isMultiple)
                tagSelect.Attributes.Add("multiple", "true");

            if (AddBlank)
            {
                // Creatop, option tag
                TagBuilder tagOption = new TagBuilder("option");
                tagOption.Attributes.Add("value", "");
                tagSelect.InnerHtml += tagOption.ToString();
            }
            // Create Option Tags
            if (Data != null)
                foreach (BaseEntity baseEntity in Data)
                {
                    // Creatop, option tag
                    TagBuilder tagOption = new TagBuilder("option");
                    tagOption.Attributes.Add("value", baseEntity.Id.ToString());
                    tagOption.InnerHtml += baseEntity.ToString();

                    // isOptopnTag is Selected
                    if (Selected.Contains(baseEntity.Id.ToString()))
                    {
                        tagOption.Attributes.Add("selected", "true");
                    }


                    // add data-filteredbyid = value
                    if(SelectFilterAttribute != null)
                    {
                        string Filtered_Property_Name = SelectFilterAttribute.Filter_HTML_Id;
                        PropertyInfo Filtered_Property = baseEntity.GetType().GetProperty(Filtered_Property_Name);
                        if (Filtered_Property != null)
                        {
                            string Find_Filtred_Id_Value = Filtered_Property.GetValue(baseEntity).ToString();
                            tagOption.Attributes.Add(data_FilteredBy_name, Find_Filtred_Id_Value);
                        }
                    }
                   

                    tagSelect.InnerHtml += tagOption.ToString();
                }


            MvcHtmlString mvcHtmlString = new MvcHtmlString(tagSelect.ToString());
            return mvcHtmlString;
        }
    }
}