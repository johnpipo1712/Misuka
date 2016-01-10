using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System.Web
{
  public static class ContextExtensions
  {
    /// <summary>
    /// Returns the application root url of current request. Note that there's always a slash (/) at the end of returned url.
    /// </summary>
    /// <returns></returns>
    public static string GetApplicationRootUrl(this HttpContext context)
    {
      return new HttpContextWrapper(context).GetApplicationRootUrl();
    }

    /// <summary>
    /// Returns the application root url of current request. Note that there's always a slash (/) at the end of returned url.
    /// </summary>
    /// <returns></returns>
    public static string GetApplicationRootUrl(this HttpContextBase context)
    {
      return RemoveStandardPortFromURL(string.Concat(context.Request.Url.GetLeftPart(UriPartial.Authority), context.Request.ApplicationPath.TrimEnd('/'), "/"));
    }
    


    /// <summary>
    /// Safely adds a cookie to client browser. The existing cookie (by name) will be overwritten
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cookieName"></param>
    /// <param name="cookieValue"></param>
    /// <param name="expiredInDay"></param>
    public static void AddCookie(this HttpContext context, string cookieName, string cookieValue, int expiredInDay = 90)
    {
      new HttpContextWrapper(context).AddCookie(cookieName, cookieValue, expiredInDay);
    }

    /// <summary>
    /// Safely adds a cookie to client browser. The existing cookie (by name) will be overwritten
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cookieName"></param>
    /// <param name="cookieValue"></param>
    /// <param name="expiredInDay"></param>
    public static void AddCookie(this HttpContextBase context, string cookieName, string cookieValue, int expiredInDay = 90)
    {
      HttpCookie cookie = new HttpCookie(cookieName);
      cookie.Value = cookieValue;
      cookie.Expires = DateTime.Now.AddDays(expiredInDay);
      context.AddCookie(cookie);
    }

    /// <summary>
    /// Safely adds a cookie to client browser. The existing cookie (by name) will be overwritten
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cookie"></param>
    public static void AddCookie(this HttpContext context, HttpCookie cookie)
    {
      new HttpContextWrapper(context).AddCookie(cookie);
    }

    /// <summary>
    /// Safely adds a cookie to client browser. The existing cookie (by name) will be overwritten
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cookie"></param>
    public static void AddCookie(this HttpContextBase context, HttpCookie cookie)
    {
      context.Response.Cookies.Set(cookie);
    }

    /// <summary>
    /// Deletes a cookie specified by <c>cookieName</c>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cookieName"></param>
    public static void DeleteCookie(this HttpContext context, string cookieName)
    {
      new HttpContextWrapper(context).DeleteCookie(cookieName);
    }

    /// <summary>
    /// Delete a cookie specified by <c>cookieName</c>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cookieName"></param>
    public static void DeleteCookie(this HttpContextBase context, string cookieName)
    {
      if (context.Request.Cookies[cookieName] != null)
      {
        HttpCookie cookie = new HttpCookie(cookieName) {Expires = DateTime.Now.AddDays(-1d)};
        context.AddCookie(cookie);
      }
    }

    private static readonly Regex RemoveStandardPortRegex = new Regex("(http[s]*://.*):(\\d+)(/.*)?", RegexOptions.IgnoreCase);
    private static string RemoveStandardPortFromURL(string url)
    {
      Match match = RemoveStandardPortRegex.Match(url);
      if (match.Success)
      {
        int port = int.Parse(match.Groups[2].Value);
        if (port == 80 || port == 443)
        {
          url = string.Format("{0}{1}", match.Groups[1].Value, match.Groups[3].Value);
        }
      }
      return url;
    }
  }
}