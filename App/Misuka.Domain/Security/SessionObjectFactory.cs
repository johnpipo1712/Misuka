using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Misuka.Domain.Entity;
using Misuka.Infrastructure.Configuration;
using Misuka.Infrastructure.Utilities;

namespace Misuka.Domain.Security
{
  public class SessionObjectFactory
  {
    private const string SESSION_NAME = "Time_Session";

    private static BaseSessionObject _sessionObject;

    internal static void SetSessionObject(BaseSessionObject sessionObject)
    {
      _sessionObject = sessionObject;
    }

    public static void ResetSessionObject()
    {
      _sessionObject = null;
    }

    /// <summary>
    /// Create a session object by configuration
    /// </summary>
    /// <returns></returns>
    public static BaseSessionObject GetSessionObject()
    {
      var context = HttpContextUtilities.GetHttpContext();

      var sessionObject = GetSessionObjectFromHttpContext(context);
      if (sessionObject != null) return sessionObject;

      sessionObject = CreateSessionObject();
      sessionObject.Init();

      if (!sessionObject.UserId.Equals(Guid.Empty))
      {
        SaveSessionObjectToHttpContext(sessionObject, context);
        return sessionObject;
      }

      return sessionObject;
    }

    /// <summary>
    /// Create a session object for specific user account
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    internal static BaseSessionObject GetSessionObject(string username, string password)
    {
      BaseSessionObject sessionObject = CreateSessionObject();
      sessionObject.Init(username, password);
      var context = HttpContextUtilities.GetHttpContext();
      SaveSessionObjectToHttpContext(sessionObject, context);
      return sessionObject;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    internal static BaseSessionObject GetSessionObject(User user)
    {
      BaseSessionObject sessionObject = CreateSessionObject();
      sessionObject.Init(user);
      var context = HttpContextUtilities.GetHttpContext();
      SaveSessionObjectToHttpContext(sessionObject, context);
      return sessionObject;

    }

    #region Private methods


    private static BaseSessionObject CreateSessionObject()
    {
      return _sessionObject ?? CreateSessionObjectFromConfiguration();
    }


    private static BaseSessionObject CreateSessionObjectFromConfiguration()
    {
      BaseSessionObject sessionObject = null;
      Type sessionType = Type.GetType("Misuka.Domain.Providers.SessionObject");
      try
      {
        if (sessionType != null) sessionObject = (BaseSessionObject)Activator.CreateInstance(sessionType);
      }
     
      catch (Exception ex)
      {
        throw new ConfigurationErrorsException("Could not create an instance of DashboardSession", ex);
      }
      return sessionObject;
    }


    /// <summary>
    /// Try to get session object from http context
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private static BaseSessionObject GetSessionObjectFromHttpContext(HttpContextBase context)
    {
      if (context == null) return null;
      if (context.Items.Contains(SESSION_NAME))
      {
        var sessionObject = (BaseSessionObject)context.Items[SESSION_NAME];
        if (!sessionObject.UserId.Equals(Guid.Empty)) return sessionObject;
      }

      return null;
    }

    /// <summary>
    /// Save session object to http context so that other http modules and handlers can use it without initializing again
    /// </summary>
    /// <param name="sessionObject"></param>
    /// <param name="context"></param>
    private static void SaveSessionObjectToHttpContext(BaseSessionObject sessionObject, HttpContextBase context)
    {
      if (context == null) return;

      if (context.Items.Contains(SESSION_NAME)) context.Items[SESSION_NAME] = sessionObject;
      else context.Items.Add(SESSION_NAME, sessionObject);
    }

    #endregion
  }
}