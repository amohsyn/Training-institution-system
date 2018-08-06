using GApp.WebApp.Manager.Views.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace GApp.WebApp.Helpers
{
    public static class EditorFor_SelectFilter_Extention
    {
      

        public static IHtmlString EditFor_Select_With_Filter<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
          
            var member = expression.Body as MemberExpression;
            var SelectFilterAttribute = member.Member.GetCustomAttributes(typeof(SelectFilterAttribute), false).FirstOrDefault() as SelectFilterAttribute;

            TagBuilder tagSelect = new TagBuilder("select");

            TagBuilder tagOption = new TagBuilder("option");

            tagSelect.InnerHtml += tagOption.ToString();


            MvcHtmlString mvcHtmlString = new MvcHtmlString("");
            return mvcHtmlString;
        }
    }
}