using System.Web;

namespace Misuka.Domain.Security
{
  public interface IAuthenticationCookieManager
  {
    /// <summary>
    /// Saves specified username as authentication cookie
    /// </summary>
    /// <param name="username"></param>
    /// <param name="context"></param>
    void Save(string username, HttpContextBase context);

    /// <summary>
    /// Loads username from authentication cookie. Returns null if the cookie is not available or expired 
    /// </summary>
    /// <returns></returns>
    string Load(HttpContextBase context);

    /// <summary>
    /// Removes the authentication cookie
    /// </summary>
    /// <param name="context"></param>
    void Remove(HttpContextBase context);
  }
}
