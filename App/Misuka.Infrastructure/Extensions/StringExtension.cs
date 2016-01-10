using System;
using System.ComponentModel;
using System.Globalization;

namespace Misuka.Infrastructure.Extensions
{
  public static class StringExtension
  {
    public static T? ToNullable<T>(this string s) where T : struct
    {
      T? result = null;
      string value = s.Trim();
      if (!string.IsNullOrEmpty(value))
      {
        TypeConverter converter = TypeDescriptor.GetConverter(typeof(T?));
        result = (T?)converter.ConvertFrom(value);
      }
      return result;
    }

    public static T ToType<T>(this string s)
    {
      T result = default(T);
      string value = s.Trim();
      if (!string.IsNullOrEmpty(value))
      {
        TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
        var convertFrom = converter.ConvertFromInvariantString(value);
        if (convertFrom != null) result = (T)convertFrom;
      }
      return result;
    }

    public static DateTime? ToNullableDateTime(this string s, string dateFormat)
    {
      return s.ToNullableDateTime(CultureInfo.InvariantCulture, dateFormat);
    }

    public static DateTime? ToNullableDateTime(this string s, IFormatProvider formatProvider, string dateFormat)
    {
      DateTime? result = null;
      string value = s.Trim();
      if (!string.IsNullOrEmpty(value))
      {
        DateTime dt;
        if (DateTime.TryParseExact(value, dateFormat, formatProvider, DateTimeStyles.None, out dt)) result = dt;
      }
      return result;
    }

    public static Guid ToGuid(this string s)
    {
      Guid result = Guid.Empty;
      if (!string.IsNullOrEmpty(s))
      {
        try
        {
          result = new Guid(s);
        }
        catch
        {
          result = Guid.Empty;
        }
      }
      return result;
    }

    public static T ToEnum<T>(this string s)
    {
      return (T)Enum.Parse(typeof(T), s.Trim(), true);
    }

    public static T? ToNullableEnum<T>(this string s) where T : struct
    {
      T? result = null;
      string value = s.Trim();
      if (!string.IsNullOrEmpty(value))
      {
        result = (T?)Enum.Parse(typeof(T), value, true);
      }
      return result;
    }

  }
}