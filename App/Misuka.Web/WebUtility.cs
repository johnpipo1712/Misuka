using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web;
using Misuka.Infrastructure.Extensions;
using WebControl.Grid.Util;

namespace Misuka.Web
{
  public class WebUtility
  {
    public static HttpPostedFileBase TryGetFileFromRequest(string name, HttpRequestBase request)
    {
      var file = request.Files[name];
      return (file != null && file.ContentLength > 0) ? file : null;
    }

    public static HttpPostedFileBase TryGetFileFromRequest(string name, HttpRequestBase request, out bool isEmptyFileInput)
    {
      var file = request.Files[name];
      isEmptyFileInput = file == null || (file.ContentLength == 0 && !string.IsNullOrEmpty(file.FileName));
      return (file != null && file.ContentLength > 0) ? file : null;
    }

    public static string GetFileName(HttpPostedFileBase postedFile)
    {
      return postedFile == null ? string.Empty : postedFile.FileName;
    }

    public static Stream GetFileStream(HttpPostedFileBase postedFile)
    {
      return postedFile == null ? null : postedFile.InputStream;
    }

    public static object GetSearchCriteriaValue(PropertyInfo propertyInfo, GridRequestArgs.PageCriteriaItem item)
    {
      object obj = null;
      if (propertyInfo.PropertyType == typeof(bool))
        obj = Convert.ToBoolean(item.FieldValue);

      else if (propertyInfo.PropertyType == typeof(long?))
        obj = string.IsNullOrEmpty(item.FieldValue) ? null : item.FieldValue.ToNullable<long>();

      else if (propertyInfo.PropertyType == typeof(int?))
        obj = string.IsNullOrEmpty(item.FieldValue) ? null : item.FieldValue.ToNullable<int>();

      else if (propertyInfo.PropertyType == typeof(Guid?))
        obj = string.IsNullOrEmpty(item.FieldValue) ? null : item.FieldValue.ToNullable<Guid>();

      else if (propertyInfo.PropertyType == typeof(DateTime?))
        obj = string.IsNullOrEmpty(item.FieldValue)
          ? null
          : (item.FieldValue.ToNullableDateTime("dd.MM.yyyy") ??
             item.FieldValue.ToNullableDateTime(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern));
      else
      {
        if (item.FieldValue != null && item.FieldValue != "null") obj = Convert.ChangeType(item.FieldValue, propertyInfo.PropertyType);
      }

      return obj;
    }
  }
}