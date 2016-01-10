using System;
using System.Net;
using System.Web;
using System.Web.Security;
using Misuka.Infrastructure.Configuration;
using Misuka.Infrastructure.Utilities;


namespace Misuka.Domain.HttpModules
{
  public class PreAuthModule : BaseAuthenticationModule
  {

    #region Overrides of BaseAuthenticationModule

    public override void OnBeginRequest(HttpContextBase context)
    {


    }

    public override void OnFormsAuthenticate(HttpContextBase context)
    {
    }

    public override void OnEndRequest(HttpContextBase context)
    {

      if (UrlUtilities.IsCommonResource(context.Request.RawUrl)) return;

       // Check auth type
      if (SystemConfiguration.Instance.SecuritySettings.UseWindowAuthentication)
      {
        // we are using windows authentication, not forms authentication
        if (context.Response.StatusCode == (int) HttpStatusCode.Redirect && context.Response.RedirectLocation != null &&
            context.Response.RedirectLocation.IndexOf(FormsAuthentication.LoginUrl, StringComparison.OrdinalIgnoreCase) >=
            0)
        {
          // Hide 401 status so formsauthentication dont perform a redirect
          context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
        }
      }
    }

    public override void OnError(HttpContextBase context)
    {
    }

    #endregion

    #region Private methods


    #endregion
  }
}