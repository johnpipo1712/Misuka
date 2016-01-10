using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Misuka.Domain.Security;
using Misuka.Domain.Utilities;
using Misuka.Infrastructure.Configuration;
using Misuka.Infrastructure.Utilities;


namespace Misuka.Domain.HttpModules
{
  public class PostAuthModule : BaseAuthenticationModule
  {

    private ISecurityUtility _securityUtility;
    protected ISecurityUtility SecurityUtility
    {
      get { return _securityUtility ?? (_securityUtility = new SecurityUtility()); }
    }

    #region Overrides of BaseAuthenticationModule

    public override void OnBeginRequest(HttpContextBase context)
    {
    }

    public override void OnFormsAuthenticate(HttpContextBase context)
    {
      //// Ignore resource files
      if (UrlUtilities.IsCommonResource(context.Request.RawUrl)) return;

      var session = new UserSession();

      // Forms authentication
      if (!SystemConfiguration.Instance.SecuritySettings.UseWindowAuthentication)
      {
        //Need to explicitly set User on this context again although we already did it in line: DashboardSession session = new DashboardSession();
        //Dont really know why but it seems FormsAuthenticationEventArgs.Context and HttpContext.Current are different to each other in this case
        context.User = session.Principal;
        return;
      }


      //**********************************************************************
      //** Use windows authentication instead of forms authentication

      // Has user allready been verified for this session?

      string username = context.Request.ServerVariables["LOGON_USER"];


      if (string.IsNullOrEmpty(username))
      {
        //Anonymous user
        //Need to explicitly set User on this context again although we already did it in line: DashboardSession session = new DashboardSession();
        //Dont really know why but it seems FormsAuthenticationEventArgs.Context and HttpContext.Current are different to each other in this case
        context.User = new WindowsPrincipal(WindowsIdentity.GetAnonymous());
        return;
      }

      if (!session.IsAuthenticated())
      {
        var user = SecurityUtility.GetUserByUsername(username);
        if (user == null)
        {
          context.User = new WindowsPrincipal(WindowsIdentity.GetAnonymous());
          return;
        }

        //attach the user to current Dashboard session
        session = new UserSession(user);
        context.User = session.Principal;
      }
    }

    public override void OnEndRequest(HttpContextBase context)
    {
    }

    public override void OnError(HttpContextBase context)
    {
    }

    #endregion


    #region Private methods



    #endregion
  }
}