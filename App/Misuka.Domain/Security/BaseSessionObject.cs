using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using Misuka.Domain.Context;
using Misuka.Domain.Entity;
using Misuka.Domain.Utilities;
using Misuka.Infrastructure.Configuration;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;

namespace Misuka.Domain.Security
{
  public abstract class BaseSessionObject
  {
    protected SessionData SessionData { get; set; }

    #region Properties

    public virtual Guid UserId
    {
      get { return SessionData.UserId; }
    }

    public virtual string Username
    {
      get { return SessionData.Username; }
    }

    public virtual string FirstName
    {
      get { return SessionData.FirstName; }
    }

    public virtual string MiddleName
    {
      get { return SessionData.MiddleName; }
    }

    public virtual string LastName
    {
      get { return SessionData.LastName; }
    }

    public virtual string Email
    {
      get { return SessionData.Email; }
    }

    public virtual string ImageUrl
    {
      get { return SessionData.ImageUrl; }
    }
    public virtual string FileVirtualPath
    {
      get { return SessionData.FileVirtualPath; }
    }
    public virtual string ImageUrlCover
    {
      get { return SessionData.ImageCoverUrl; }
    }
    public virtual string FileCoverVirtualPath
    {
      get { return SessionData.FileCoverVirtualPath; }
    }
    public virtual IPrincipal Principal { get; protected set; }

    public IList<Guid> RoleIds
    {
      get { return SessionData.RoleIds; }
    }

    #endregion
    
    #region Constructors

    protected BaseSessionObject()
    {
      SessionData = new SessionData();
    }

    #endregion

    #region Methods
    public virtual bool IsAuthenticated()
    {
      return SessionData.IsAuthenticated;
    }

    public virtual bool IsAdministrator()
    {
      return SessionData.IsAdministrator;
    }

    internal virtual void Init()
    {
      var loadingResult = SessionObjectStorageStrategy.Load();
      if (loadingResult.Status != SessionDataLoadingStatus.Succeeded)
      {
        SessionData = new SessionData();
        return;
      }

      SessionData = loadingResult.Data;
      SaveAuthenticatedIdentity();
    }

    internal virtual void Init(User loggingUser)
    {
      SessionData = new SessionData(loggingUser);
      SaveAuthenticatedIdentity();
    }

    internal virtual void Init(string username, string password)
    {
      ISecurityUtility securityUtility = new SecurityUtility();
      var membershipProvider = securityUtility.GetMembershipProvider();
      if (!membershipProvider.ValidateUser(username, password)) 
        throw new ApplicationException(string.Format("Failed to init Session Object. Invalid username or password. Username: {0}. Password {1}", username, password));

      var user = SecurityUtility.GetUserByUsername(username);
      if (user == null) throw new ApplicationException(string.Format("Cannot retrieve user for username {0}.", username));

      Init(user);
    }
    
    internal virtual void SaveAuthenticatedIdentity()
    {
      if (SessionData == null) return;

      Principal = CreatePrincipal();
      if (Context != null) Context.User = Principal;

      if (SessionData.Saved) return;

      SessionData.Saved = true;
      if (Context != null) AuthenticationCookieManager.Save(SessionData.Username, Context);

      SessionData.RoleIds = SecurityUtility.GetRoleIds(SessionData.UserId);
      SessionData.IsAdministrator = SessionData.RoleIds != null && SessionData.RoleIds.Contains(SystemConfiguration.Instance.SecuritySettings.AdministratorRole);

      SessionObjectStorageStrategy.Save(SessionData);
    }

    internal virtual void RemoveAuthenticatedIdentity()
    {
      SessionData = new SessionData();
      Principal = null;
      if (Context != null)
      {
        Context.User = null;
        AuthenticationCookieManager.Remove(Context);
      }
      SessionObjectStorageStrategy.Remove();
    }

    protected virtual IPrincipal CreatePrincipal()
    {
      return SessionData == null ? null : new ClaimsPrincipal(new GenericIdentity(SessionData.Username));
    }
    #endregion

    #region Plugable components

    private HttpContextBase _context;
    protected HttpContextBase Context
    {
      get { return _context ?? (_context =new HttpContextWrapper(HttpContext.Current)); }
    }

    private IAuthenticationCookieManager _authenticationCookieManager;
    protected IAuthenticationCookieManager AuthenticationCookieManager
    {
      get { return _authenticationCookieManager ?? (_authenticationCookieManager = new AuthenticationCookieManager()); }
    }

    private ISecurityUtility _securityUtility;
    protected ISecurityUtility SecurityUtility
    {
      get { return _securityUtility ?? (_securityUtility = new SecurityUtility()); }
    }

    private ISessionObjectStorageStrategy _sessionObjectStorageStrategy;
    protected ISessionObjectStorageStrategy SessionObjectStorageStrategy
    {
      get { return _sessionObjectStorageStrategy ?? (_sessionObjectStorageStrategy = new SessionObjectStorageStrategy()); }
    }

    #endregion
   
    public IList<int> AccessibleFunctions
    {
      get
      {
        if (Context.Session["AccessibleFunctions"] == null)
          Context.Session["AccessibleFunctions"] = SecurityUtility.GetPermissions(UserId);

        SessionData.AccessiblePermissions = (IList<int>)Context.Session["AccessibleFunctions"];
        return SessionData.AccessiblePermissions;
      }
    }
  }



  public enum SessionDataLoadingStatus
  {
    /// <summary>
    /// Successfully load session data from isolated storage 
    /// </summary>
    Succeeded = 0,
    /// <summary>
    /// Can't load sesssion data because it's not existed in isolated storage
    /// </summary>
    NotExisted = 1,
    /// <summary>
    /// The data is in isolated storage but it's already expired or invalid
    /// </summary>
    Invalid = 2
  }

  public class SessionDataLoadingResult
  {
    public SessionData Data { get; private set; }
    public SessionDataLoadingStatus Status { get; private set; }

    public SessionDataLoadingResult(SessionDataLoadingStatus status, SessionData data = null)
    {
      Data = data;
      Status = status;
    }

    public static readonly SessionDataLoadingResult NotExistedResult = new SessionDataLoadingResult(SessionDataLoadingStatus.NotExisted);
    public static readonly SessionDataLoadingResult InvalidResult = new SessionDataLoadingResult(SessionDataLoadingStatus.Invalid);
  }
}