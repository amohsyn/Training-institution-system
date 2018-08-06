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


        public static MvcHtmlString EditFor_Select_With_Filter<TModel, TValue>(this HtmlHelper<TModel> htmlHelper
            , Expression<Func<TModel, TValue>> expression, List<BaseEntity> Entities, List<String> Selected
            )
        {


            //< select id = "Selected_ActionControllerApps" name = "Selected_ActionControllerApps" multiple >
            //            @foreach(var actionControllerApp in Model.All_ActionControllerApps)
            //    {
            //        < option value = "@actionControllerApp.Id" data-controllerappid = "@actionControllerApp.ControllerApp.Id" > @actionControllerApp.Code </ option >
            //    }
            //</ select >

            var member = expression.Body as MemberExpression;
            PropertyInfo property = member.Member as PropertyInfo;

            SelectFilterAttribute SelectFilterAttribute = member.Member.GetCustomAttributes(typeof(SelectFilterAttribute), false).FirstOrDefault() as SelectFilterAttribute;
            if (SelectFilterAttribute == null)
            {
                // [Bug] Localization
                string msg = string.Format("The property {0} does not have an attribute {1} in the helper {2} ",
                    member.Member.Name, nameof(SelectFilterAttribute), nameof(EditFor_Select_With_Filter));
                throw new GAppException(msg);
            }

            string SelectTagId = SelectFilterAttribute.FilteredBy.Name.Pluralize();
            string FilteredBy_Id = SelectFilterAttribute.FilteredBy.Name + "Id";
            string data_FilteredBy_name = string.Format("data-{0}", FilteredBy_Id.ToLower());

            TagBuilder tagSelect = new TagBuilder("select");
            tagSelect.Attributes.Add("id", SelectTagId);
            tagSelect.Attributes.Add("name", SelectTagId);
            tagSelect.Attributes.Add("multiple", "true");

            foreach (BaseEntity baseEntity in Entities)
            {
                TagBuilder tagOption = new TagBuilder("option");
                tagOption.SetValue(baseEntity.Id.ToString(), baseEntity.ToString());

                // data-filtered_id_value
                string Filtered_Property_Name = SelectFilterAttribute.FilteredBy.Name + "Id";
                PropertyInfo Filtered_Property = baseEntity.GetType().GetProperty(Filtered_Property_Name);
                if(Filtered_Property != null)
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