using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Security;
using Misuka.Domain.Security;

namespace Misuka.Domain.Providers
{
  public class SessionObject : BaseSessionObject
  {
    public override bool IsAuthenticated()
    {
      if (Context == null) return false;
      string username = AuthenticationCookieManager.Load(Context);
      return !string.IsNullOrEmpty(username);
    }

    internal override void Init()
    {
      var loadingResult = SessionObjectStorageStrategy.Load();
      if (loadingResult.Status == SessionDataLoadingStatus.Succeeded)
      {
        SessionData = loadingResult.Data;
        SaveAuthenticatedIdentity();
        return;
      }

      //Either internet user is disabled or invalid/expired data stored in cookie. Sign out and redirect to login page
      if (loadingResult.Status == SessionDataLoadingStatus.Invalid)
      {
        RemoveAuthenticatedIdentity();
        if (Context.Request.Url != null && Context.Request.Url.AbsolutePath.IndexOf(FormsAuthentication.LoginUrl, StringComparison.OrdinalIgnoreCase) < 0)
        {
          FormsAuthentication.RedirectToLoginPage();
        }
        return;
      }

      SessionData = new SessionData();
    }

    protected override IPrincipal CreatePrincipal()
    {
      return SessionData == null ? null : new ClaimsPrincipal(new GenericIdentity(SessionData.Username));
    }
  }
}