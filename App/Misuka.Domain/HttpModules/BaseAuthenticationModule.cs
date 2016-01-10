using System.Web;
using System.Web.Security;

namespace Misuka.Domain.HttpModules
{
  public abstract class BaseAuthenticationModule : IHttpModule
  {
    public void Init(HttpApplication httpApplication)
    {
      httpApplication.BeginRequest += (sender, eventArgs) => OnBeginRequest(new HttpContextWrapper(((HttpApplication)sender).Context));
      httpApplication.EndRequest += (sender, e) => OnEndRequest(new HttpContextWrapper(((HttpApplication)sender).Context));
      httpApplication.Error += (sender, e) => OnError(new HttpContextWrapper(((HttpApplication)sender).Context));
      ((FormsAuthenticationModule)httpApplication.Modules["FormsAuthentication"]).Authenticate += (sender, formsAuthenticationEventArgs) => OnFormsAuthenticate(new HttpContextWrapper(formsAuthenticationEventArgs.Context));
    }

    public void Dispose()
    {
    }

    public abstract void OnBeginRequest(HttpContextBase context);
    public abstract void OnFormsAuthenticate(HttpContextBase context);
    public abstract void OnEndRequest(HttpContextBase context);
    public abstract void OnError(HttpContextBase context);
  }

}