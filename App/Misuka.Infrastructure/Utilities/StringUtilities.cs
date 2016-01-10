using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Misuka.Infrastructure.Extensions;

namespace Misuka.Infrastructure.Utilities
{
  public static class StringUtilities
  {
    public static IList<T> ParseCommaSeparatedStringToList<T>(string text)
    {
      IList<T> result = new List<T>();
      if (string.IsNullOrEmpty(text)) return result;

      string[] arrayOfElements = text.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
      foreach (string element in arrayOfElements)
      {
        //result.Add((T)Convert.ChangeType(element, typeof(T))); // This method not support convert to GUID so we need below way to work around
        result.Add(element.ToType<T>());
      }
      return result;
    }

    public static String NormalizeFileName(String value)
    {
      if (value == null) return string.Empty;
      return new Regex(string.Format("[{0}]", Regex.Escape(@"<>:""/\|?*#"))).Replace(value, string.Empty).Trim();
    }
    public static string GetNameStringFile(string namefile)
    {
      string name = "";
      name = namefile.Substring(0, namefile.IndexOf('.'));
      return name;
    }
  }
}
