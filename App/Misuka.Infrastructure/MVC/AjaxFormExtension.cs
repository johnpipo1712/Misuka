using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace System.Web.Mvc.Ajax
{
  public static class AjaxFormExtension
  {
    public static MvcForm BeginFormWithCloseDialog(this AjaxHelper ajaxHelper, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
    {
      return ajaxHelper.BeginFormWithCloseDialog(actionName, controllerName, routeValues, ajaxOptions, string.Empty, htmlAttributes);
    }

    public static MvcForm BeginFormWithCloseDialog(this AjaxHelper ajaxHelper, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, string errorPanel, object htmlAttributes)
    {
      ProcessOnComplete(ajaxOptions);
      return ajaxHelper.BeginForm(actionName, controllerName, routeValues, ajaxOptions, errorPanel, htmlAttributes);
    }

    public static MvcForm BeginForm(this AjaxHelper ajaxHelper, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, string errorPanel, object htmlAttributes)
    {
      if (!string.IsNullOrEmpty(errorPanel))
      {
        ProcessOnSuccess(ajaxOptions, htmlAttributes, errorPanel);
      }
      ProcessOnFailure(ajaxOptions, htmlAttributes);

      var newhtmlAttribute = htmlAttributes.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(htmlAttributes, null));
      if (newhtmlAttribute.ContainsKey("class"))
        newhtmlAttribute["class"] = newhtmlAttribute["class"] + " ajax-form";
      else
        newhtmlAttribute.Add("class", "ajax-form");

      var newRouteCollection = new RouteValueDictionary();
      if (routeValues != null)
      {
        foreach (var prop in routeValues.GetType().GetProperties())
        {
          newRouteCollection.Add(prop.Name, prop.GetValue(routeValues, null));
        }
      }
      return ajaxHelper.BeginForm(actionName, controllerName, newRouteCollection, ajaxOptions, newhtmlAttribute);
    }

    public static MvcForm AjaxUploadFormWithCloseDialog(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, string errorPanel, object htmlAttributes)
    {
      ProcessOnComplete(ajaxOptions);
      return htmlHelper.AjaxUploadForm(actionName, controllerName, routeValues, ajaxOptions, errorPanel, htmlAttributes);
    }

    public static MvcForm AjaxUploadForm(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, string errorPanel, object htmlAttributes)
    {
      ProcessOnSuccess(ajaxOptions, htmlAttributes, errorPanel, true);
      var newhtmlAttribute = htmlAttributes.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(htmlAttributes, null));
      ProcessAttributeHtmlForm(ajaxOptions, newhtmlAttribute);
      newhtmlAttribute.Add("enctype", "multipart/form-data");
      var newRouteCollection = new RouteValueDictionary();
      foreach (var prop in routeValues.GetType().GetProperties())
      {
        newRouteCollection.Add(prop.Name, prop.GetValue(routeValues, null));
      }
      return htmlHelper.BeginForm(actionName, controllerName, newRouteCollection, FormMethod.Post, newhtmlAttribute);
    }


    private static void ProcessAttributeHtmlForm(AjaxOptions ajaxOptions, IDictionary<string, object> newhtmlAttribute)
    {
      const string emptyFunction = "function(){}";
      var onsuccess = string.IsNullOrEmpty(ajaxOptions.OnSuccess) ? emptyFunction : ajaxOptions.OnSuccess;
      var oncomplete = string.IsNullOrEmpty(ajaxOptions.OnComplete) ? emptyFunction : ajaxOptions.OnComplete;
//      newhtmlAttribute.Add("onsubmit", string.Format("uploadHandler(this, new Sys.UI.DomEvent(event), {{onsuccess:{0}, oncomplete:{1} }})", onsuccess, oncomplete));
      newhtmlAttribute.Add("onsubmit", string.Format("uploadHandler(this, {{onsuccess:{0}, oncomplete:{1} }})", onsuccess, oncomplete));

    }

    private static void ProcessOnComplete(AjaxOptions ajaxOptions)
    {
      if (string.IsNullOrEmpty(ajaxOptions.OnComplete))
      {
        ajaxOptions.OnComplete = "defaultResponse.completeHandler";
      }
      ajaxOptions.OnComplete = string.Format("defaultResponse.completeHandler(data,{0});", ajaxOptions.OnComplete);
    }

    private static void ProcessOnSuccess(AjaxOptions ajaxOptions, object htmlAttributes, string errorPanel, bool uploadForm)
    {
      if (!uploadForm)
      {
        ajaxOptions.OnSuccess = string.IsNullOrEmpty(ajaxOptions.OnSuccess)
          ? string.Format("defaultResponse.successHandler(data, '{0}', '{1}')", GetAttributeValue(htmlAttributes, "id"),
            errorPanel)
          : string.Format("defaultResponse.successHandler(data, '{0}', '{1}', {2})",
            GetAttributeValue(htmlAttributes, "id"), errorPanel, ajaxOptions.OnSuccess);
      }
      else
      {
        ajaxOptions.OnSuccess = string.IsNullOrEmpty(ajaxOptions.OnSuccess) ? string.Format("function onSuccess(ajaxContext){{alert('ddd');defaultResponse.successHandler(ajaxContext, '{0}', '{1}')}}", GetAttributeValue(htmlAttributes, "id"), errorPanel) : string.Format("function onSuccess(ajaxContext){{defaultResponse.successHandler(ajaxContext, '{0}', '{1}', {2})}}", GetAttributeValue(htmlAttributes, "id"), errorPanel, ajaxOptions.OnSuccess);
      }
    }

    private static void ProcessOnSuccess(AjaxOptions ajaxOptions, object htmlAttributes, string errorPanel)
    {
      ProcessOnSuccess(ajaxOptions, htmlAttributes, errorPanel, false);
    }

    private static void ProcessOnFailure(AjaxOptions ajaxOptions, object htmlAttributes)
    {
      if (string.IsNullOrEmpty(ajaxOptions.OnFailure))
      {
        ajaxOptions.OnFailure = string.Format("defaultResponse.failureHandler(data, '{0}')", GetAttributeValue(htmlAttributes, "id"));
      }
      else
      {
        ajaxOptions.OnFailure = string.Format("defaultResponse.failureHandler(data, '{0}', {1})", GetAttributeValue(htmlAttributes, "id"), ajaxOptions.OnFailure);
      }
    }

    private static string GetAttributeValue(object htmlAttributes, string attribute)
    {
      var pi = htmlAttributes.GetType().GetProperties().SingleOrDefault(x => x.Name.Equals(attribute, StringComparison.OrdinalIgnoreCase));
      if (pi == null)
      {
        return string.Format("f{0}", new Random().Next());
      }
      return (string)pi.GetValue(htmlAttributes, null);
    }
  }
}