using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Misuka.Infrastructure.Utilities
{
  public class ValueUtilities
  {
    public static bool GetBoolean(string value, bool defaultValue)
    {
      try
      {
        return bool.Parse(value);
      }
      catch
      {
        return defaultValue;
      }
    }

    public static long GetInt64(string value, long defaultValue)
    {
      if (value == null)
        return defaultValue;
      if (!new Regex("^[0123456789]+$|^-[0123456789]+$").Match(value.Trim()).Success)
        return defaultValue;
      try
      {
        return long.Parse(value.Trim());
      }
      catch
      {
        return defaultValue;
      }
    }

    public static int GetInt32(string value, int defaultValue)
    {
      if (value == null)
        return defaultValue;
      if (!new Regex("^[0123456789]+$|^-[0123456789]+$").Match(value.Trim()).Success)
        return defaultValue;
      try
      {
        return int.Parse(value.Trim());
      }
      catch
      {
        return defaultValue;
      }
    }

    public static Decimal GetDecimal(string value, Decimal defaultValue)
    {
      if (value == null)
        return defaultValue;
      string newValue = new Decimal(5, 0, 0, false, (byte)1).ToString().Substring(1, 1);
      value = value.Replace(".", newValue);
      value = value.Replace(",", newValue);
      if (!new Regex("^[0123456789.,]+$|^-[0123456789.,]+$").Match(value).Success)
        return defaultValue;
      try
      {
        return Decimal.Parse(value);
      }
      catch
      {
        return defaultValue;
      }
    }

    public static Guid GetGuid(string value, Guid defaultValue)
    {
      if (value == null)
        return defaultValue;
      try
      {
        return new Guid(value);
      }
      catch
      {
        return defaultValue;
      }
    }

    public static T GetEnumerationValue<T>(string flags)
    {
      var obj1 = (object)0;
      Type enumType = typeof(T);
      if (!enumType.IsEnum)
        throw new InvalidOperationException("This function can only be called with a enum type.");
      Type underlyingType = Enum.GetUnderlyingType(enumType);
      if (underlyingType != typeof(short) && underlyingType != typeof(int) && (underlyingType != typeof(long) && underlyingType != typeof(byte)))
        throw new InvalidOperationException("The enum type must be one of the following types: Int16, Int32, Int64, Byte");
      T obj2 = (T)obj1;
      if (flags != null && !flags.Equals(string.Empty))
      {
        string[] strArray = flags.Split(new char[1]
        {
          char.Parse("|")
        });
        if (strArray.Length > 0)
        {
          foreach (string str in strArray)
          {
            var obj3 = (object)obj2;
            var obj4 = (object)GetEnumerationValueFromString<T>(str);
            var obj5 = (object)0;
            if (underlyingType == typeof(short))
              obj5 = (object)((int)(short)obj3 | (int)(short)obj4);
            if (underlyingType == typeof(int))
              obj5 = (object)((int)obj3 | (int)obj4);
            if (underlyingType == typeof(long))
              obj5 = (object)((long)obj3 | (long)obj4);
            if (underlyingType == typeof(byte))
              obj5 = (object)((int)(byte)obj3 | (int)(byte)obj4);
            obj2 = (T)obj5;
          }
        }
      }
      return obj2;
    }

    private static T GetEnumerationValueFromString<T>(string value)
    {
      return (T)Enum.Parse(typeof(T), value);
    }

    public static bool GuidTryParse(string s, out Guid result)
    {
      result = Guid.Empty;
      if (s == null)
        return false;
      var format = new Regex(
          "^[A-Fa-f0-9]{32}$|" +
          "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
          "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$");
      var match = format.Match(s);
      if (match.Success)
      {
        result = new Guid(s);
        return true;
      }
      result = Guid.Empty;
      return false;
    }

    public static DateTime? GetNullableDateTime(string value, CultureInfo culture)
    {
      if (value == null)
        return null;

      const string format = "d";
      if (culture == null) culture = CultureInfo.InvariantCulture;
      try
      {
        DateTime result = DateTime.ParseExact(value, format, culture);
        return result;
      }
      catch (FormatException)
      {
        return null;
      }
    }

    public static DateTime? GetNullableDateTime(string value, string dateTimeFormat)
    {
      if (value == null)
        return null;

      try
      {
        DateTime result = DateTime.Parse(value, CultureInfo.CurrentUICulture);
        return result;
      }
      catch (FormatException ex)
      {
        return null;
      }
    }

    public static Guid? GetNullableGuid(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
        return null;

      try
      {
        Guid result = Guid.Empty;
        var isValid = Guid.TryParse(value, out result);
        if (isValid) return result;
        return null;
      }
      catch (FormatException)
      {
        return null;
      }
    }

    public static decimal? GetNullableDecimal(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
        return null;

      try
      {
        decimal result = 0;
        var isValid = decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
        if (isValid) return result;
        return null;
      }
      catch (FormatException)
      {
        return null;
      }
    }

    public static int? GetNullableInt32(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
        return null;

      try
      {
        int result = 0;
        var isValid = int.TryParse(value, out result);
        if (isValid) return result;
        return null;
      }
      catch (FormatException)
      {
        return null;
      }
    }

    public static DateTime GetFirstDateOfWeek(DateTime dayInWeek, DayOfWeek firstDay)
    {
      DateTime firstDayInWeek = dayInWeek.Date;
      while (firstDayInWeek.DayOfWeek != firstDay)
        firstDayInWeek = firstDayInWeek.AddDays(-1);

      return firstDayInWeek;
    }
    public static DateTime GetLastDateOfWeek(DateTime dayInWeek, DayOfWeek firstDay)
    {
      DateTime lastDayInWeek = dayInWeek.Date;
      while (lastDayInWeek.DayOfWeek != firstDay)
        lastDayInWeek = lastDayInWeek.AddDays(1);

      return lastDayInWeek;
    }
  }
}
