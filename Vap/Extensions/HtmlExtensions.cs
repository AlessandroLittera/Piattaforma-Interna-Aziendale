using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Vap.Extensions
{
    
    public static class HtmlExtensions
    {
        public static IHtmlContent DisabledIf(this IHtmlHelper htmlHelper,
                                                bool condition)
            => new HtmlString(condition ? "disabled=\"disabled\"" : "");


        public static IHtmlContent OnlyFor(this IHtmlHelper htmlHelper,
                                                string role)
        {
            var isInRole = htmlHelper.ViewContext.HttpContext.User.IsInRole(role);
            if (isInRole)
            {
                return new HtmlString("qualified");
            }
            return new HtmlString("isDisabled");
        }
        public static IHtmlContent SampleFor(this IHtmlHelper htmlHelper,
                                                string role)
        {
            var isInRole = htmlHelper.ViewContext.HttpContext.User.IsInRole(role);
            if (!isInRole)
            {
                return new HtmlString("qualified");
            }
            return new HtmlString("isDisabled");
        }

        public static IHtmlContent OnlyFor(this IHtmlHelper htmlHelper, ICollection<string> roles)
        {
            var isUsable = false;
            foreach(var role in roles)
            {
                var isInRole = htmlHelper.ViewContext.HttpContext.User.IsInRole(role);
                if (isInRole)
                {
                    isUsable = true;
                }
            }
            if (!isUsable)
            {
                return new HtmlString("");
            }
            return new HtmlString("isDisabled");
        }

        //public static IHtmlContent GetList(this IHtmlHelper helper)
        // {

        //   System.Web.Mvc.TagBuilder li = new System.Web.Mvc.TagBuilder("li");


        //     var listHtml = new HtmlContentBuilder();
        //     listHtml.AppendHtml("<li class='dropdown'>");
        //     listHtml.AppendHtml("<a class='fa fa - user' data-toggle='dropdown'");
        //     listHtml.AppendHtml("<span class='caret'>");
        //     listHtml.AppendHtml("</span>");
        //     listHtml.AppendHtml("</a>");
        //     listHtml.AppendHtml("<ul class='dropdown-menu'>");
        //     listHtml.AppendHtml("<li>");
        //     listHtml.AppendHtml("<a>");
        //     listHtml.AppendHtml(helper.ActionLink("foo", "bar", "example"));
        //     listHtml.AppendHtml("</a>");
        //     listHtml.AppendHtml("</li>");
        //     listHtml.AppendHtml("</ul>");
        //     listHtml.AppendHtml("</li>");

        //     return listHtml;
        // }

        /*public static IHtmlContent GenerateInput(this IHtmlHelper<TModel> helper)
        {
            var tb = new TagBuilder("input");

            tb.TagRenderMode = TagRenderMode.SelfClosing;
            tb.MergeAttribute("name", this.GetIdentity());
            tb.MergeAttribute("type", "hidden");
            tb.MergeAttribute("value", this.GetSelectedItem()?.Value);
            return tb;
        }*/

        //           <div class="form-group">
        //    <label class="col-sm-2 control-label"><span class="input-group-text" id="inputGroup-sizing-default">@Html.LabelFor(x => x.Name)</span></label>
        //    <div class="col-sm-10">
        //        @Html.TextBoxFor(x => x.Name, new { @class = "form-control" })
        //        <span asp-validation-for="Name" />
        //    </div>
        //</div>


        public static IHtmlContent LabeledTextBoxFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {

            var builder = new System.Web.Mvc.TagBuilder("div");
            builder.AddCssClass("form-group");

            var label = new System.Web.Mvc.TagBuilder("label");
            label.AddCssClass("col-sm-2 control-label");
            label.Attributes.Add("id", "");

          //  builder.InnerHtml += label.toHtml

            var textBoxFor = htmlHelper.TextBoxFor(expression);
            var labelFor = htmlHelper.LabelFor(expression);

            builder.InnerHtml = "polof";

            
            return new HtmlString(builder.ToString());
        }
        
        public static IHtmlContent File<TModel, TResult>(this IHtmlHelper<IModel> htmlHelper, Expression<Func<TModel, TResult>> expression, string id)
        {
            var div = new System.Web.Mvc.TagBuilder("div");
            div.AddCssClass("form-control");

            var label = new System.Web.Mvc.TagBuilder("label");
            label.AddCssClass("col-sm-2 control-label");

            var span = new System.Web.Mvc.TagBuilder("span");
            span.AddCssClass("input-group-text");
            span.Attributes.Add("id", "inputGroup-sizing-default");

           // var textBoxFor = htmlHelper.TextBoxFor(expression);
           // var labelFor = htmlHelper.LabelFor(expression);
            
            System.Web.Mvc.TagBuilder tb = new System.Web.Mvc.TagBuilder("input");
            tb.Attributes.Add("type", "file");
            tb.Attributes.Add("id", id);
            return new HtmlString(tb.ToString());
        }
       

         //<div class="form-group">
         //                   <label class="col-sm-2 control-label"><span class="input-group-text" id="inputGroup-sizing-default">@Html.LabelFor(x => x.Image)</span></label>
         //                   <div class="col-sm-10">
         //                       @Html.TextBoxFor(x => x.Image, htmlAttributes: new { @type = "file" })
         //                       <span asp-validation-for="Image" />
         //                   </div>
         //               </div>

    }
}
