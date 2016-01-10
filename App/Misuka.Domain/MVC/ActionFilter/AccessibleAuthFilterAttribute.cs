using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Configuration;

namespace Misuka.Domain.MVC.ActionFilter
{
  /// <summary>
  /// Limits action to members of the Administrator Role
  /// TODO: should be in Infrastructure project but it depends on UserSession. 
  /// </summary>
  public class AccessibleAuthFilterAttribute : AuthorizeAttribute
  {
    public string PermissionName { get; set; }

    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
      if (!base.AuthorizeCore(httpContext))
        return false;
   //   var permissionId = typeof(PermissionObjects).GetField(PermissionName).GetValue(typeof(PermissionObjects));
  //    return permissionId != null && new UserSession().HasPermissionOnPermission((int)permissionId);
      return true;
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
      filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "Unauthorized" }));
    }
  }
}
