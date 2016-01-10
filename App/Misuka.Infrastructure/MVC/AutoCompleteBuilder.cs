using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace Misuka.Infrastructure.MVC
{
  public enum AutoCompleteAddMode
  {
    Inline = 1,
    Dialog
  }

  public enum AutoCompleteMode
  {
    Search = 1,
    DropDown
  }

  public enum DropdownDirection
  {
    Down,
    Up
  }

  public class AutoCompleteBuilder<TModel, TValue>
  {
    private HtmlHelper<TModel> _helper;
    private string _inputName;
    private bool _prefetch;
    private string _ajaxUrl;
    private DialogSetting _advancedSearchSetting;
    private DialogSetting _addSetting;
    private int _addHintCharacters = 4;
    private AutoCompleteAddMode _addMode = AutoCompleteAddMode.Inline;
    private AutoCompleteMode _mode = AutoCompleteMode.DropDown;
    private Expression<Func<TModel, TValue>> _expression;
    private IEnumerable<SelectListItem> _localData;
    private Attributes _attributes;
    private string _onChanged;
    private string _onSelect;
    private string _onItemMouseMove;
    private string _onItemMouseOut;
    private string _onDropdown;
    private bool _ignoreMark;
    private IList<SelectListItem> _initData;
    private int _containerMaxHeight;
    private int _inputWidth;
    private DropdownDirection _dropdownDirection;
    private bool _enableCaching;
    private bool _enablePaging;
    private int _pageSize = 10;
    private string _loadMoreText = "More";// AutoCompleteResource.LoadMoreResults;

    public AutoCompleteBuilder(HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> property)
    {
      _helper = helper;
      _expression = property;
      _attributes = new Attributes();
    }

    public AutoCompleteBuilder<TModel, TValue> AutocompleteName(string name)
    {
      _inputName = name;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> LocalData(IEnumerable<SelectListItem> data)
    {
      _localData = data;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> InitData(string text, string value)
    {
      _initData = new List<SelectListItem>
                            {
                                new SelectListItem
                                    {
                                        Text = (text ?? string.Empty),
                                        Value = value,
                                        Selected = true
                                    }
                            };
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> InitData(IList<SelectListItem> data)
    {
      _initData = data;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> AdvancedSearchSetting(DialogSetting advancedSearch)
    {
      _advancedSearchSetting = advancedSearch;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> AddSetting(DialogSetting addSetting)
    {
      _addSetting = addSetting;
      return this;
    }

    /// <summary>
    /// Load all data while initializing
    /// It is invalid when EnablePaging is true or Mode is Search
    /// </summary>
    /// <param name="prefetch"></param>
    /// <returns></returns>
    public AutoCompleteBuilder<TModel, TValue> Prefetch(bool prefetch)
    {
      _prefetch = prefetch;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> AddMode(AutoCompleteAddMode addMode)
    {
      _addMode = addMode;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> Mode(AutoCompleteMode mode)
    {
      _mode = mode;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> AjaxUrl(string url)
    {
      _ajaxUrl = url;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> ContainerMaxheight(int maxHeight)
    {
      _containerMaxHeight = maxHeight;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> EnableCaching(bool enable)
    {
      _enableCaching = enable;
      return this;
    }

    ///<summary>
    /// Allow list of items which not match with searchkey in highlight mode.
    ///</summary>
    ///<param name="value"></param>
    ///<returns></returns>
    public AutoCompleteBuilder<TModel, TValue> IgnoreMark(bool value)
    {
      _ignoreMark = value;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> EnablePaging(bool enable)
    {
      _enablePaging = enable;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> PageSize(int pageSize)
    {
      if (pageSize > 0)
        _pageSize = pageSize;

      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> LoadMoreText(string loadMoreText)
    {
      if (!string.IsNullOrEmpty(loadMoreText))
        _loadMoreText = loadMoreText;

      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> InputWidth(int width)
    {
      _inputWidth = width;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> AddHintCharacters(int characters)
    {
      _addHintCharacters = characters;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> HtmlAttributes(Attributes attributes)
    {
      _attributes = attributes;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> AddHtmlAttributes(string key, string value)
    {
      _attributes.Add(key, value);
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> HtmlAttributes(object htmlAttributes)
    {
      var dic = htmlAttributes == null
                     ? new Dictionary<String, Object>(StringComparer.OrdinalIgnoreCase)
                     : htmlAttributes is IDictionary<String, Object> ? (IDictionary<String, Object>)htmlAttributes : new RouteValueDictionary(htmlAttributes);
      foreach (var o in dic)
      {
        _attributes.Add(o.Key, o.Value);
      }
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> DropdownDirection(DropdownDirection direction)
    {
      _dropdownDirection = direction;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> OnChanged(string onchanged)
    {
      _onChanged = onchanged;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> OnDropdown(string onDropdown)
    {
      _onDropdown = onDropdown;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> OnSelected(string onselected)
    {
      _onSelect = onselected;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> OnItemMouseMove(string onItemMouseMove)
    {
      _onItemMouseMove = onItemMouseMove;
      return this;
    }

    public AutoCompleteBuilder<TModel, TValue> OnItemMouseOut(string onItemMouseOut)
    {
      _onItemMouseOut = onItemMouseOut;
      return this;
    }

    static object GetDefaultDataValue(Type dataType)
    {
      if (!dataType.IsValueType)
        return null;
      return Activator.CreateInstance(dataType);
    }

    public MvcHtmlString Render()
    {
      string id = ExpressionHelper.GetExpressionText(_expression);

      if (_prefetch)
      {
        if (_mode == AutoCompleteMode.Search)
          throw new ArgumentException("Cannot use Prefetch in Search mode");

        if (_enablePaging)
          throw new ArgumentException("Cannot use Prefetch with Paging");
      }

      #region Init javascript
      var sb = new StringBuilder();
      sb.AppendFormat("<script language='javascript'>");
      sb.AppendFormat("$().ready(function() {{");
      sb.AppendLine();
      sb.AppendFormat("$('#{0}').dashboardAutoComplete({{", string.IsNullOrEmpty(_inputName) ? id + "-autocomplete" : _inputName);
      sb.AppendFormat("mode : {0}, ", (int)_mode);
      sb.AppendFormat("dropdownDirection: '{0}', ", _dropdownDirection);

      if (_advancedSearchSetting != null && _advancedSearchSetting.Enable)
      {
        sb.AppendFormat("advancedSearchOptions: '{0}', ", new JavaScriptSerializer().Serialize(_advancedSearchSetting));
      }

      if (_addSetting != null && _addSetting.Enable)
      {
        sb.AppendFormat("addOptions: '{0}', ", new JavaScriptSerializer().Serialize(_addSetting));
        sb.AppendFormat("addMode: {0}, ", (int)_addMode);
        sb.AppendFormat("addHintChars: {0}, ", _addHintCharacters);
      }

      if (_localData != null)
      {
        sb.AppendFormat("source: {0},", new JavaScriptSerializer().Serialize(_localData));
        _prefetch = true;
      }
      else
      {
        sb.AppendFormat("url: '{0}', ", _ajaxUrl);
        if (_initData != null)
          sb.AppendFormat("initData: {0},", new JavaScriptSerializer().Serialize(_initData));
      }

      sb.AppendFormat("prefetch: {0}, ", _prefetch ? "true" : "false");

      if (!string.IsNullOrEmpty(_onChanged))
        sb.AppendFormat("onChanged: '{0}', ", _onChanged);

      if (!string.IsNullOrEmpty(_onSelect))
        sb.AppendFormat("onSelected: '{0}', ", _onSelect);

      if (!string.IsNullOrEmpty(_onItemMouseMove))
        sb.AppendFormat("onItemMouseMove: '{0}', ", _onItemMouseMove);

      if (!string.IsNullOrEmpty(_onItemMouseOut))
        sb.AppendFormat("onItemMouseOut: '{0}', ", _onItemMouseOut);

      if (!string.IsNullOrEmpty(_onDropdown))
        sb.AppendFormat("onDropdown: '{0}', ", _onDropdown);

      if (_containerMaxHeight > 0)
      {
        sb.AppendFormat("maxHeight: '{0}', ", _containerMaxHeight);
      }

      sb.AppendFormat("enableCaching: {0}, ", _enableCaching ? "true" : "false");
      if (_inputWidth > 0)
      {
        sb.AppendFormat("inputWidth: {0}, ", _inputWidth);
      }
      sb.AppendFormat("ignoreMark: {0}, ", _ignoreMark ? "true" : "false");
      sb.AppendFormat("enablePaging: {0}, ", _enablePaging ? "true" : "false");
      sb.AppendFormat("pageSize: {0}, ", _enablePaging ? _pageSize : 20);
      sb.AppendFormat("loadMoreText: '{0}', ", _loadMoreText);
      sb.AppendFormat("hiddenId: '{0}' ", id);
      sb.AppendFormat("}});");
      sb.AppendLine();
      sb.AppendFormat("  }});");
      sb.AppendFormat("</script>");
      #endregion

      string classAttValues = string.Empty;
      if (_attributes.ContainsKey("class") && _attributes["class"] != null)
      {
        classAttValues = _attributes["class"].ToString();
        _attributes.Remove("class");
      }

      classAttValues = string.Format("{0} {1}", classAttValues, "t-autocomplete");
      if (!string.IsNullOrEmpty(classAttValues))
        _attributes.Add("class", classAttValues);
      _attributes.Add("data-init-value", GetDefaultDataValue(_expression.Body.Type));

      var input = _helper.TextBox(string.IsNullOrEmpty(_inputName) ? string.Format("{0}-autocomplete", id) : _inputName, string.Empty, _attributes);
      return MvcHtmlString.Create(string.Format("{0}{1}{2}", input, _helper.HiddenFor(_expression, new { @class = classAttValues }), sb));
    }
  }
}

