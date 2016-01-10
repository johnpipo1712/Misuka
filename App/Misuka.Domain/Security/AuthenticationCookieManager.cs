using System;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;

namespace Misuka.Domain.Security
{
  class AuthenticationCookieManager : IAuthenticationCookieManager
  {
    
    #region Implementation of IAuthenticationCookieManager

    public void Save(string username, HttpContextBase context)
    {
      FormsAuthentication.SetAuthCookie(username, false);
    }

    public string Load(HttpContextBase context)
    {
      HttpCookie authCookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
      if (authCookie == null || authCookie.Value.Equals(string.Empty)) return null;

      FormsAuthenticationTicket ticket = null;
      try
      {
        ticket = FormsAuthentication.Decrypt(authCookie.Value);
      }
      catch (CryptographicException)
      {
        //Deal with the "Padding is invalid and cannot be removed" exception
        ExpireAuthenticationCookie(context);
      }
      catch (HttpException)
      {
        /*Årsaken til feilen er høyst sannsynligvis patchen som ble installert på web serverne for å adressere dette problemet http://weblogs.asp.net/scottgu/archive/2010/09/18/important-asp-net-security-vulnerability.aspx
        Vi kan adressere dette slik som det er beskrevet her: http://forums.asp.net/p/1608654/4107803.aspx . */
        ExpireAuthenticationCookie(context);
      }
      if (ticket == null || ticket.Expired) return null;

      // If user was logged in allready (it could be with an actual user, not anonymous internet user) make sure
      // the auth cookie is refreshed with the correct login name
      string username = ticket.Name;

      return username;
    }

    public void Remove(HttpContextBase context)
    {
      FormsAuthentication.SignOut();
    }

    #endregion

    #region Private methods

    private void ExpireAuthenticationCookie(HttpContextBase context)
    {
      var cookie = new HttpCookie(FormsAuthentication.FormsCookieName) { Expires = DateTime.Now.AddDays(-1) };
      context.Response.Cookies.Add(cookie);
    }

    #endregion
  }
}