using System.Web;

namespace Misuka.Infrastructure.Utilities
{
  public static class HttpContextUtilities
  {
    public static HttpContextBase GetHttpContext()
    {
      if (HttpContext.Current != null)
      {
        return new HttpContextWrapper(HttpContext.Current);
      }
      return null;
    }
  }
}
