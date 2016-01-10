using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Misuka.Infrastructure.Utilities
{
  public static class EnumExtensions
  {
    public static string ToDescriptionString(this Enum value)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      Type type = value.GetType();
      DescriptionAttribute descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(type.GetField(Enum.GetName(type, (object)value)), typeof(DescriptionAttribute));
      if (descriptionAttribute != null)
        return descriptionAttribute.Description;
      return ((object)value).ToString();
    }
  }

  public static class EnumUtilities
  {
    public static IList<SelectListItem> BuildSelectList<T>(T? selectedValue) where T : struct
    {
      IList<SelectListItem> items = new List<SelectListItem>();
      Array values = Enum.GetValues(typeof(T));
      foreach (object value in values)
      {
        string description = (value as Enum).ToDescriptionString();
        SelectListItem item = new SelectListItem
                                  {
                                    Value = Convert.ToInt32(value).ToString(),
                                    Text = string.IsNullOrEmpty(description) ? Enum.GetName(typeof(T), value) : description
                                  };
        if (selectedValue.HasValue && selectedValue.Value.Equals(value)) item.Selected = true;
        items.Add(item);
      }
      return items;
    }

    public static IList<SelectListItem> BuildSelectList<T>(T? selectedValue, bool hasEmptyValue) where T : struct
    {
      var items = BuildSelectList<T>(selectedValue);
      if (hasEmptyValue)
      {
        var emptyValue = new SelectListItem
                             {
                               Text = "",
                               Value = "",
                               Selected = selectedValue == null ? true : false
                             };
        items.Insert(0, emptyValue);
      }
      return items;
    }

    public static string GetTextOfSelectedItem<T>(int selectedValue) where T : struct
    {
      foreach (object obj in Enum.GetValues(typeof(T)))
      {
        string str = (obj as Enum).ToDescriptionString();
        if (selectedValue.Equals(obj.GetHashCode()))
          return string.IsNullOrEmpty(str) ? Enum.GetName(typeof(T), obj) : str;
      }
      return string.Empty;
    }
  }
}
