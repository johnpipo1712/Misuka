using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web.UI.WebControls;
using Misuka.Domain.Entity;
using Misuka.Infrastructure.Configuration;

namespace Misuka.Domain.Security
{
  public class UserSession : IUserSession
  {
    private readonly BaseSessionObject _sessionObject;

    #region Constructors
    public UserSession()
    {
      _sessionObject = SessionObjectFactory.GetSessionObject();
    }

    internal UserSession(BaseSessionObject sessionObject)
    {
      _sessionObject = sessionObject;
    }

    internal UserSession(User loggingUser)
    {
      _sessionObject = SessionObjectFactory.GetSessionObject(loggingUser);
    }

    #endregion

    #region Static methods

    public static UserSession Login(string username, string password)
    {
      var sessionObject = SessionObjectFactory.GetSessionObject(username, password);
      return new UserSession(sessionObject);
    }
    
    public static UserSession Logout()
    {
      var session = new UserSession();
      session.DoLogout();
      return session;
    }

    #endregion

    #region Properties

    public string FirstName
    {
      get { return _sessionObject.FirstName; }
    }
    public string MiddleName
    {
      get { return _sessionObject.MiddleName; }
    }
    public string LastName
    {
      get { return _sessionObject.LastName; }
    }
    
    public string Email
    {
      get { return _sessionObject.Email; }
    }

    public  string ImageUrl
    {
      get { return _sessionObject.ImageUrl; }
    }

    public  string FileVirtualPath
    {
      get { return _sessionObject.FileVirtualPath; }
    }
    public string ImageUrlCover
    {
      get { return _sessionObject.ImageUrlCover; }
    }

    public string FileCoverVirtualPath
    {
      get { return _sessionObject.FileCoverVirtualPath; }
    }
    public string FullName
    {
      get
      {
        var name = string.Format("{0} {1} {2}", FirstName ?? string.Empty, MiddleName ?? string.Empty, LastName ?? string.Empty);
        name = name.Replace("  ", " ");
        return name;
      }
    }
    public Guid UserId
    {
      get { return _sessionObject.UserId; }
    }

    public string Username
    {
      get { return _sessionObject.Username; }
    }

    public IPrincipal Principal
    {
      get { return _sessionObject.Principal; }
    }
    #endregion
    
    /// <summary>
    /// This function returns true if current user is authenticated 
    /// </summary>
    /// <returns></returns>
    public bool IsAuthenticated()
    {
      return _sessionObject.IsAuthenticated();
    }

    private void DoLogout()
    {
      _sessionObject.RemoveAuthenticatedIdentity();
    }

    #region User's permission
    /// <returns></returns>
    public bool IsAdministrator()
    {
      return IsAuthenticated() && _sessionObject.IsAdministrator();
    }

    public bool HasPermission(Guid roleId)
    {
      if (_sessionObject.IsAdministrator()) return true;
      return _sessionObject.RoleIds != null && _sessionObject.RoleIds.Contains(roleId);
    }
    public IList<Guid> GetRoles(Guid userId)
    {
      return _sessionObject.RoleIds;
    }
    public IList<int> GetPermission()
    {
      return _sessionObject.AccessibleFunctions;
    }
    public bool CheckPermission(string permissions)
    {
      var result = false;
      var permissionsDefault = _sessionObject.AccessibleFunctions;
      string[] ArrayPermissions = permissions.Split(',');
      for (int i = 0; i < ArrayPermissions.Length; i++)
      {
        var index = permissionsDefault.IndexOf(Convert.ToInt32(ArrayPermissions[i]));
        if (index != -1 )
        {
          result = true;
          break;
        }
      }
      return result;
    }
    public bool HasPermissionOnPermission(int permissionId)
    {
      if (_sessionObject.IsAdministrator()) return true;

      if (_sessionObject.AccessibleFunctions == null || !_sessionObject.AccessibleFunctions.Contains(permissionId)) return false;

      return true;
    }

    #endregion
  }
}
