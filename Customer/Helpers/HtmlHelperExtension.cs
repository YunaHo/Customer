using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Customer.Helpers
{
    public static class HtmlHelperExtension
    {

        //public static MvcHtmlString SortLink<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object HtmlAttributes = null)
        //{
        //    //透過AnonymousObjectToHtmlAttributes將HtmlAttribute讀取出來
        //    var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(HtmlAttributes);
        //    //將expression的值抓出來放到data-content這個屬性中
        //    attrs.Add("data-content", ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model);
        //    //var txtBoxFor = htmlHelper.TextBoxFor(expression, attrs);
        //    return MvcHtmlString.Create(txtBoxFor.ToString());
        //}
    }
}