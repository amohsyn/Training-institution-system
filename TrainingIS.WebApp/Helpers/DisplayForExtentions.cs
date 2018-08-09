using GApp.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TrainingIS.WebApp.Helpers
{
    public static class DisplayForExtentions
    {
        public static MvcHtmlString GAppDisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            Type typeProperty = expression.Body.Type;
            if (typeProperty.IsEnum)
            {
                Type typeEnum = typeProperty;
                string value = html.DisplayFor(expression).ToString();
                 
                string LocalValue = GAppEnumLocalization.GetLocalValue(typeEnum, value);
                return new MvcHtmlString(LocalValue);
            }

            return html.DisplayFor(expression);
        }
    }
}