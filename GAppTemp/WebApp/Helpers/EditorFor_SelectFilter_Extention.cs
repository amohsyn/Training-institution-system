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


        public static MvcHtmlString EditFor_Select_With_Filter<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            List<BaseEntity> Entities,
            bool isMultiple

            )
        {
            // Template
            //< select id = "Selected_ActionControllerApps" name = "Selected_ActionControllerApps" multiple >
            //            @foreach(var actionControllerApp in Model.All_ActionControllerApps)
            //    {
            //        < option value = "@actionControllerApp.Id" data-controllerappid = "@actionControllerApp.ControllerApp.Id" > @actionControllerApp.Code </ option >
            //    }
            //</ select >

            // Property
            var member = expression.Body as MemberExpression;
            PropertyInfo property = member.Member as PropertyInfo;


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
            


            // get SelectFilterAttribute
            SelectFilterAttribute SelectFilterAttribute = member.Member.GetCustomAttributes(typeof(SelectFilterAttribute), false).FirstOrDefault() as SelectFilterAttribute;
            if (SelectFilterAttribute == null)
            {
                // [Bug] Localization
                string msg = string.Format("The property {0} does not have an attribute {1} in the helper {2} ",
                    member.Member.Name, nameof(SelectFilterAttribute), nameof(EditFor_Select_With_Filter));
                throw new GAppException(msg);
            }

            // Get MetaData from SelectFilterAttribute
            string SelectTagId = property.Name;
            string FilteredBy_Id = SelectFilterAttribute.Filter_HTML_Id;
            string data_FilteredBy_name = string.Format("data-{0}", FilteredBy_Id.ToLower());



            TagBuilder tagSelect = new TagBuilder("select");
            tagSelect.Attributes.Add("id", SelectTagId);
            tagSelect.Attributes.Add("name", SelectTagId);
            tagSelect.Attributes.Add("class", "form-control");
            if (isMultiple)
                tagSelect.Attributes.Add("multiple", "true");

            if (Entities != null)
                foreach (BaseEntity baseEntity in Entities)
                {
                    TagBuilder tagOption = new TagBuilder("option");

                    tagOption.Attributes.Add("value", baseEntity.Id.ToString());
                    tagOption.InnerHtml += baseEntity.ToString();

                    if (Selected.Contains(baseEntity.Id.ToString()))
                    {
                        tagOption.Attributes.Add("selected", "true");
                    }


                    // data-filtered_id_value
                    string Filtered_Property_Name = SelectFilterAttribute.Filter_HTML_Id;
                    PropertyInfo Filtered_Property = baseEntity.GetType().GetProperty(Filtered_Property_Name);
                    if (Filtered_Property != null)
                    {
                        string Find_Filtred_Id_Value = Filtered_Property.GetValue(baseEntity).ToString();
                        tagOption.Attributes.Add(data_FilteredBy_name, Find_Filtred_Id_Value);
                    }

                    tagSelect.InnerHtml += tagOption.ToString();
                }


            MvcHtmlString mvcHtmlString = new MvcHtmlString(tagSelect.ToString());
            return mvcHtmlString;
        }
    }
}