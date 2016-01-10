using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Misuka.Infrastructure.Resources;

namespace Misuka.Infrastructure.MVC
{
  public static class HtmlHelperExtensions
  {
    private static IDictionary<String, Object> GetHtmlAttributesIncludeDisabledReadOnly(Object htmlAttributes, Boolean isDisabled, Boolean isReadOnly)
    {
      var dic = htmlAttributes == null
                       ? new Dictionary<String, Object>(StringComparer.OrdinalIgnoreCase)
                       : htmlAttributes is IDictionary<String, Object> ? (IDictionary<String, Object>)htmlAttributes : new RouteValueDictionary(htmlAttributes);
      if (isReadOnly) dic["readonly"] = "readonly";
      if (isDisabled) dic["disabled"] = "disabled";
      return dic;
    }

    /// <summary>
    /// Convert model property name to element id
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="html"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static string IdFor<T, TResult>(this HtmlHelper<T> html, Expression<Func<T, TResult>> expression)
    {
      var id = html.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
      // because "[" and "]" aren't replaced with "_" in GetFullHtmlFieldId
      return id.Replace('[', '_').Replace(']', '_');
    }

    /// <summary>
    /// Convert model property name to element name
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="html"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static string NameFor<T, TResult>(this HtmlHelper<T> html, Expression<Func<T, TResult>> expression)
    {
      return html.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
    }

    /// <summary>
    /// Generate unique id for html. Unique id is storaged in ViewData, this means in current view, same key = same id
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="helper"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static String Id<TModel>(this HtmlHelper<TModel> helper, String key)
    {
      var idBag = helper.ViewData["id-bag"] as IDictionary<String, String>;
      if (idBag == null) helper.ViewData["id-bag"] = idBag = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
      String id;
      if (!idBag.TryGetValue(key, out id)) idBag[key] = id = "id_" + Guid.NewGuid().ToString("n");
      return id;
    }

    #region Asterisk

    public static MvcHtmlString RequiredSymbol(this HtmlHelper htmlHelper)
    {
      return RequiredSymbol(htmlHelper, null);
    }

    public static MvcHtmlString RequiredSymbol(this HtmlHelper htmlHelper, Object attributes)
    {
      return RequiredSymbol(htmlHelper, true, attributes);
    }

    public static MvcHtmlString RequiredSymbol(this HtmlHelper htmlHelper, Boolean isRequired, Object attributes)
    {
      return RequiredSymbol(htmlHelper, isRequired, "*", attributes);
    }

    public static MvcHtmlString RequiredSymbol(this HtmlHelper htmlHelper, Boolean isRequired, String symbol)
    {
      return RequiredSymbol(htmlHelper, isRequired, symbol, null);
    }

    public static MvcHtmlString RequiredSymbol(this HtmlHelper htmlHelper, Boolean isRequired, String symbol, Object attributes)
    {
      if (isRequired)
      {
        var tag = new TagBuilder("span");
        tag.AddCssClass("required");
        tag.MergeAttributes(attributes == null
                                ? new Dictionary<String, Object>(StringComparer.OrdinalIgnoreCase)
                                : attributes is IDictionary<String, Object> ? (IDictionary<String, Object>)attributes : new RouteValueDictionary(attributes));
        tag.SetInnerText(symbol);
        return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
      }
      return MvcHtmlString.Empty;
    }

    #endregion

    #region Read only textbox
    public static MvcHtmlString ReadOnlyTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
    {
      return ReadOnlyTextBoxFor(htmlHelper, expression, null);
    }

    public static MvcHtmlString ReadOnlyTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Object htmlAttributes)
    {
      return ReadOnlyTextBoxFor(htmlHelper, true, expression, htmlAttributes);
    }

    public static MvcHtmlString ReadOnlyTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Boolean readOnlyStatus, Expression<Func<TModel, TProperty>> expression)
    {
      return ReadOnlyTextBoxFor(htmlHelper, readOnlyStatus, expression, null);
    }

    public static MvcHtmlString ReadOnlyTextBox(this HtmlHelper htmlHelper, String name, Object value)
    {
      return ReadOnlyTextBox(htmlHelper, name, value, null);
    }

    public static MvcHtmlString ReadOnlyTextBox(this HtmlHelper htmlHelper, String name, Object value, Object htmlAttributes)
    {
      return ReadOnlyTextBox(htmlHelper, true, name, value, htmlAttributes);
    }

    public static MvcHtmlString ReadOnlyTextBox(this HtmlHelper htmlHelper, Boolean readOnlyStatus, String name, Object value)
    {
      return ReadOnlyTextBox(htmlHelper, readOnlyStatus, name, value, null);
    }

    public static MvcHtmlString ReadOnlyTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Boolean readOnlyStatus, Expression<Func<TModel, TProperty>> expression, Object htmlAttributes)
    {
      return htmlHelper.TextBoxFor(expression, GetHtmlAttributesIncludeDisabledReadOnly(htmlAttributes, false, readOnlyStatus));
    }

    public static MvcHtmlString ReadOnlyTextBox(this HtmlHelper htmlHelper, Boolean readOnlyStatus, String name, Object value, Object htmlAttributes)
    {
      return htmlHelper.TextBox(name, value, GetHtmlAttributesIncludeDisabledReadOnly(htmlAttributes, false, readOnlyStatus));
    }
    #endregion

    #region Disabled textbox
    public static MvcHtmlString DisabledTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
    {
      return DisabledTextBoxFor(htmlHelper, expression, null);
    }

    public static MvcHtmlString DisabledTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Object htmlAttributes)
    {
      return DisabledTextBoxFor(htmlHelper, true, expression, htmlAttributes);
    }

    public static MvcHtmlString DisabledTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Boolean disabledStatus, Expression<Func<TModel, TProperty>> expression)
    {
      return DisabledTextBoxFor(htmlHelper, disabledStatus, expression, null);
    }

    public static MvcHtmlString DisabledTextBox(this HtmlHelper htmlHelper, String name, Object value)
    {
      return DisabledTextBox(htmlHelper, name, value, null);
    }

    public static MvcHtmlString DisabledTextBox(this HtmlHelper htmlHelper, String name, Object value, Object htmlAttributes)
    {
      return DisabledTextBox(htmlHelper, true, name, value, htmlAttributes);
    }

    public static MvcHtmlString DisabledTextBox(this HtmlHelper htmlHelper, Boolean disabledStatus, String name, Object value)
    {
      return DisabledTextBox(htmlHelper, disabledStatus, name, value, null);
    }

    public static MvcHtmlString DisabledTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Boolean disabledStatus, Expression<Func<TModel, TProperty>> expression, Object htmlAttributes)
    {
      return htmlHelper.TextBoxFor(expression, GetHtmlAttributesIncludeDisabledReadOnly(htmlAttributes, disabledStatus, false));
    }

    public static MvcHtmlString DisabledTextBox(this HtmlHelper htmlHelper, Boolean disabledStatus, String name, Object value, Object htmlAttributes)
    {
      return htmlHelper.TextBox(name, value, GetHtmlAttributesIncludeDisabledReadOnly(htmlAttributes, disabledStatus, false));
    }
    #endregion

    #region Read only TextArea
    public static MvcHtmlString ReadOnlyTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
    {
      return ReadOnlyTextAreaFor(htmlHelper, expression, null);
    }

    public static MvcHtmlString ReadOnlyTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Object htmlAttributes)
    {
      return ReadOnlyTextAreaFor(htmlHelper, true, expression, htmlAttributes);
    }

    public static MvcHtmlString ReadOnlyTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Boolean readOnlyStatus, Expression<Func<TModel, TProperty>> expression)
    {
      return ReadOnlyTextAreaFor(htmlHelper, readOnlyStatus, expression, null);
    }

    public static MvcHtmlString ReadOnlyTextArea(this HtmlHelper htmlHelper, String name, Object value)
    {
      return ReadOnlyTextArea(htmlHelper, name, value, null);
    }

    public static MvcHtmlString ReadOnlyTextArea(this HtmlHelper htmlHelper, String name, Object value, Object htmlAttributes)
    {
      return ReadOnlyTextArea(htmlHelper, true, name, value, htmlAttributes);
    }

    public static MvcHtmlString ReadOnlyTextArea(this HtmlHelper htmlHelper, Boolean readOnlyStatus, String name, Object value)
    {
      return ReadOnlyTextArea(htmlHelper, readOnlyStatus, name, value, null);
    }

    public static MvcHtmlString ReadOnlyTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Boolean readOnlyStatus, Expression<Func<TModel, TProperty>> expression, Object htmlAttributes)
    {
      return htmlHelper.TextAreaFor(expression, GetHtmlAttributesIncludeDisabledReadOnly(htmlAttributes, false, readOnlyStatus));
    }

    public static MvcHtmlString ReadOnlyTextArea(this HtmlHelper htmlHelper, Boolean readOnlyStatus, String name, Object value, Object htmlAttributes)
    {
      return htmlHelper.TextArea(name, (value ?? String.Empty).ToString(), GetHtmlAttributesIncludeDisabledReadOnly(htmlAttributes, false, readOnlyStatus));
    }
    #endregion

    #region Disabled TextArea
    public static MvcHtmlString DisabledTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
    {
      return DisabledTextAreaFor(htmlHelper, expression, null);
    }

    public static MvcHtmlString DisabledTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, Object htmlAttributes)
    {
      return DisabledTextAreaFor(htmlHelper, true, expression, htmlAttributes);
    }

    public static MvcHtmlString DisabledTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Boolean disabledStatus, Expression<Func<TModel, TProperty>> expression)
    {
      return DisabledTextAreaFor(htmlHelper, disabledStatus, expression, null);
    }

    public static MvcHtmlString DisabledTextArea(this HtmlHelper htmlHelper, String name, Object value)
    {
      return DisabledTextArea(htmlHelper, name, value, null);
    }

    public static MvcHtmlString DisabledTextArea(this HtmlHelper htmlHelper, String name, Object value, Object htmlAttributes)
    {
      return DisabledTextArea(htmlHelper, true, name, value, htmlAttributes);
    }

    public static MvcHtmlString DisabledTextArea(this HtmlHelper htmlHelper, Boolean disabledStatus, String name, Object value)
    {
      return DisabledTextArea(htmlHelper, disabledStatus, name, value, null);
    }

    public static MvcHtmlString DisabledTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Boolean disabledStatus, Expression<Func<TModel, TProperty>> expression, Object htmlAttributes)
    {
      return htmlHelper.TextAreaFor(expression, GetHtmlAttributesIncludeDisabledReadOnly(htmlAttributes, disabledStatus, false));
    }

    public static MvcHtmlString DisabledTextArea(this HtmlHelper htmlHelper, Boolean disabledStatus, String name, Object value, Object htmlAttributes)
    {
      return htmlHelper.TextArea(name, (value ?? String.Empty).ToString(), GetHtmlAttributesIncludeDisabledReadOnly(htmlAttributes, disabledStatus, false));
    }
    #endregion
    

    #region Criteria Input Elements
   /* internal static string GetCriteriaString(CriteriaOperator criteriaOperator)
    {
      switch (criteriaOperator)
      {
        case CriteriaOperator.Equal:
          return string.Format("criteria {0}", "eq");

        case CriteriaOperator.GreaterThan:
          return string.Format("criteria {0}", "gt");

        case CriteriaOperator.GreaterThanOrEqual:
          return string.Format("criteria {0}", "ge");

        case CriteriaOperator.In:
          return string.Format("criteria {0}", "in");

        case CriteriaOperator.LessThan:
          return string.Format("criteria {0}", "lt");

        case CriteriaOperator.LessThanOrEqual:
          return string.Format("criteria {0}", "le");

        case CriteriaOperator.NotEqual:
          return string.Format("criteria {0}", "ne");

        default:
          return string.Empty;
      }
    }

    public static MvcHtmlString CriteriaTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
      Expression<Func<TModel, TValue>> expression, object htmlAttributes, CriteriaOperator criteriaOperator)
    {
      var routeValues = new RouteValueDictionary(htmlAttributes);
      string classAttValues = string.Empty;
      if (routeValues.ContainsKey("class"))
      {
        classAttValues = routeValues["class"].ToString();
        routeValues.Remove("class");
      }

      classAttValues = string.Format("{0} {1}", classAttValues, GetCriteriaString(criteriaOperator)).Trim();
      if (!string.IsNullOrEmpty(classAttValues))
        routeValues.Add("class", classAttValues);
      return htmlHelper.TextBoxFor(expression, routeValues);
    }

    public static MvcHtmlString CriteriaRadioButtonFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
     Expression<Func<TModel, TValue>> expression, object value, object htmlAttributes, CriteriaOperator criteriaOperator)
    {
      var routeValues = new RouteValueDictionary(htmlAttributes);
      string classAttValues = string.Empty;
      if (routeValues.ContainsKey("class"))
      {
        classAttValues = routeValues["class"].ToString();
        routeValues.Remove("class");
      }

      classAttValues = string.Format("{0} {1}", classAttValues, GetCriteriaString(criteriaOperator)).Trim();
      if (!string.IsNullOrEmpty(classAttValues))
        routeValues.Add("class", classAttValues);
      return htmlHelper.RadioButtonFor(expression, value, routeValues);
    }

    public static MvcHtmlString CriteriaDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList,
        object htmlAttributes, CriteriaOperator criteriaOperator)
    {
      var routeValues = new RouteValueDictionary(htmlAttributes);
      string classAttValues = string.Empty;
      if (routeValues.ContainsKey("class"))
      {
        classAttValues = routeValues["class"].ToString();
        routeValues.Remove("class");
      }

      classAttValues = string.Format("{0} {1}", classAttValues, GetCriteriaString(criteriaOperator)).Trim();
      if (!string.IsNullOrEmpty(classAttValues))
        routeValues.Add("class", classAttValues);
      return htmlHelper.DropDownListFor(expression, selectList, routeValues);
    }

    public static MvcHtmlString CriteriaEditorFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
     Expression<Func<TModel, TValue>> expression, string templateName, object htmlAttributes, CriteriaOperator criteriaOperator)
    {
      var routeValues = new RouteValueDictionary(htmlAttributes);
      string classAttValues = string.Empty;
      if (routeValues.ContainsKey("class"))
      {
        classAttValues = routeValues["class"].ToString();
        routeValues.Remove("class");
      }

      classAttValues = string.Format("{0} {1}", classAttValues, GetCriteriaString(criteriaOperator)).Trim();
      if (!string.IsNullOrEmpty(classAttValues))
        routeValues.Add("class", classAttValues);
      return htmlHelper.EditorFor(expression, templateName, routeValues);
    }*/
    #endregion

    #region Auto Complete Search Box

    public static AutoCompleteBuilder<TModel, TValue> AutoCompleteSearchBox<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
    {
      return new AutoCompleteBuilder<TModel, TValue>(htmlHelper, expression);
    }
    #endregion

    #region Date, time pickers
    public static MvcHtmlString File(this HtmlHelper html, string name, bool multiple)
    {
      var tb = new TagBuilder("input");
      tb.Attributes.Add("type", "file");
      tb.Attributes.Add("name", name);
      tb.GenerateId(name);

      if (multiple)
        tb.Attributes.Add("multiple", "multiple");

      return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
    }

    public static MvcHtmlString File(this HtmlHelper html, string name)
    {
      return html.File(name, false);
    }

    public static MvcHtmlString MultipleFileFor<TModel, TProperty>(this HtmlHelper<TModel> html,
           Expression<Func<TModel, TProperty>> expression)
    {
      string name = GetFullPropertyName(expression);
      return html.File(name, true);
    }

    public static MvcHtmlString FileFor<TModel, TProperty>(this HtmlHelper<TModel> html,
                  Expression<Func<TModel, TProperty>> expression)
    {
      string name = GetFullPropertyName(expression);
      return html.File(name);
    }

    public static MvcHtmlString ActionImage(this HtmlHelper html, string imageUrl,
           string action, string controller, object routeValues, object htmlAttributes)
    {
      var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
      var link = new TagBuilder("a");
      link.Attributes.Add("href", urlHelper.Action(action, controller, routeValues));
      var img = new TagBuilder("img");
      img.Attributes.Add("src", imageUrl);
      img.Attributes.Add("alt", action);
      link.InnerHtml = img.ToString(TagRenderMode.SelfClosing);
      return MvcHtmlString.Create(link.ToString(TagRenderMode.Normal));
    }

    public static MvcHtmlString Email(this HtmlHelper html, string name)
    {
      return html.Email(name, string.Empty);
    }

    public static MvcHtmlString Email(this HtmlHelper html, string name, string value)
    {
      var tb = new TagBuilder("input");
      tb.Attributes.Add("type", "email");
      tb.Attributes.Add("name", name);
      tb.Attributes.Add("value", value);
      tb.GenerateId(name);
      return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
    }

    public static MvcHtmlString EmailFor<TModel, TProperty>(this HtmlHelper<TModel> html,
           Expression<Func<TModel, TProperty>> expression)
    {
      var name = GetFullPropertyName(expression);
      var value = string.Empty;

      if (html.ViewContext.ViewData.Model != null)
        value = expression.Compile()((TModel)html.ViewContext.ViewData.Model).ToString();

      return html.Email(name, value);
    }

    static string GetFullPropertyName<T, TProperty>(Expression<Func<T, TProperty>> exp)
    {
      MemberExpression memberExp;

      if (!TryFindMemberExpression(exp.Body, out memberExp))
        return string.Empty;

      var memberNames = new Stack<string>();

      do
      {
        memberNames.Push(memberExp.Member.Name);
      }
      while (TryFindMemberExpression(memberExp.Expression, out memberExp));

      return string.Join(".", memberNames.ToArray());
    }

    static bool TryFindMemberExpression(Expression exp, out MemberExpression memberExp)
    {
      memberExp = exp as MemberExpression;

      if (memberExp != null)
        return true;

      if (IsConversion(exp) && exp is UnaryExpression)
      {
        memberExp = ((UnaryExpression)exp).Operand as MemberExpression;

        if (memberExp != null)
          return true;
      }

      return false;
    }

    static bool IsConversion(Expression exp)
    {
      return (exp.NodeType == ExpressionType.Convert || exp.NodeType == ExpressionType.ConvertChecked);
    }

    public static MvcHtmlString DatePicker(this HtmlHelper htmlHelper, string name, object date, DatePickerMode dateType, object htmlAttributes, int stepMinute = 1)
    {
      string displayValue = string.Empty, cssClass = string.Empty;
      string maskSeperator = ".";
      if (CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.Contains("/"))
        maskSeperator = "/";
      DateTime? dateValue = Convert.ToDateTime(date);
      string funcName = "datepicker";
      var mask = string.Empty;
      switch (dateType)
      {
        case DatePickerMode.DateAndTime:
          cssClass = "date-time-picker";
          if (dateValue.Value > DateTime.MinValue)
            displayValue = string.Format("{0} {1}", dateValue.Value.ToString(HtmlHelpersResource.DatePickerDatePattern), dateValue.Value.ToString(HtmlHelpersResource.DatePickerTimePattern));

          funcName = "datetimepicker";
          mask = string.Format("99{0}99{0}9999 99:99", maskSeperator);
          break;

        case DatePickerMode.Date:
          cssClass = "date-picker";
          if (dateValue.Value > DateTime.MinValue)
            displayValue = dateValue.Value.ToString(HtmlHelpersResource.DatePickerDatePattern);
          mask = string.Format("99{0}99{0}9999", maskSeperator);
          break;

        case DatePickerMode.Time:
          cssClass = "time-picker";
          if (dateValue.Value > DateTime.MinValue)
            displayValue = dateValue.Value.ToString(HtmlHelpersResource.DatePickerTimePattern);
          funcName = "timepicker";
          mask = "99:99";
          break;

        case DatePickerMode.MonthYearOnly:
          cssClass = "date-picker";
          if (dateValue.Value > DateTime.MinValue)
            displayValue = dateValue.Value.ToString(HtmlHelpersResource.DatePickerMonthYearOnlyPattern);
          mask = string.Format("99{0}9999", maskSeperator);
          break;
      }

      #region Init javascript
      var sb = new StringBuilder();
      sb.AppendFormat("<script language='javascript'>");
      sb.AppendFormat("$().ready(function() {{");
      sb.AppendLine();
      sb.AppendFormat("$('#{0}').{1}({{", name, funcName);
      sb.Append("autoFocusNextInput: true, ");
      sb.AppendFormat("dateFormat: '{0}', ",dateType== DatePickerMode.MonthYearOnly?HtmlHelpersResource.DatePickerJavascriptMonthYearOnlyPattern: HtmlHelpersResource.DatePickerJavascriptDatePattern);
      sb.AppendFormat("timeFormat: '{0}', ", HtmlHelpersResource.DatePickerJavascriptTimePattern);
      sb.Append("buttonImage: '/Content/img/common/calendar.png', ");
      sb.Append("showButtonPanel: true, ");
      sb.Append("changeYear: true, ");
      sb.AppendFormat("stepMinute: {0}, ", stepMinute);
      sb.Append("showOn: 'button', ");
      sb.Append("firstDay: 1");
      sb.AppendFormat("}});");
      sb.AppendLine();
      sb.AppendFormat("$('#{0}').mask('{1}');", name, mask);
      sb.AppendLine();
      sb.AppendFormat("$('#{0}').bind('change', function () {{", name);
      sb.AppendFormat("$(this).validateDate({{type: {0}, ", (int)dateType);
      sb.AppendFormat("dateformat: '{0}'}});", HtmlHelpersResource.DatePickerJavascriptDatePattern);
      sb.AppendFormat("  }});");
      sb.AppendLine();
      sb.AppendFormat("  }});");
      sb.AppendFormat("</script>");
      #endregion

      return MvcHtmlString.Create(string.Format("<div class='{0}'>{1}{2}</div>", cssClass, htmlHelper.TextBox(name, displayValue, htmlAttributes), sb));
    }


    public static MvcHtmlString DatePickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, DatePickerMode dateType, int stepMinute = 1)
    {
      Func<TModel, TProperty> methodCall = expression.Compile();
      TProperty value = methodCall(htmlHelper.ViewData.Model);
      var inputName = ExpressionHelper.GetExpressionText(expression);

      return htmlHelper.DatePicker(inputName, value, dateType, null, stepMinute);
    }

    public static MvcHtmlString DatePickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, DatePickerMode dateType, object htmlAttributes, int stepMinute = 1)
    {
      Func<TModel, TProperty> methodCall = expression.Compile();
      TProperty value = methodCall(htmlHelper.ViewData.Model);
      var inputName = ExpressionHelper.GetExpressionText(expression);

      return htmlHelper.DatePicker(inputName, value, dateType, htmlAttributes, stepMinute);
    }

    #endregion
  }

  public enum DatePickerMode
  {
    DateAndTime = 1,
    Date,
    Time,
    MonthYearOnly
  }
}
