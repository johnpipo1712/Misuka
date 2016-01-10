using System;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Web.Security;
using Misuka.Domain.Utilities;
using Misuka.Infrastructure.Configuration;
using Misuka.Infrastructure.Utilities;

namespace Misuka.Domain.Security
{
  class SessionObjectStorageStrategy : ISessionObjectStorageStrategy
  {
    private ISecurityUtility _securityUtility;
    private readonly string _sessionDataCookieName;

    public SessionObjectStorageStrategy()
    {
      _sessionDataCookieName = SecurityUtility.SessionDataCookieName;
    }

    private ISecurityUtility SecurityUtility
    {
      get { return _securityUtility ?? (_securityUtility = new SecurityUtility()); }
    }
    public void Save(SessionData data)
    {
      var context = HttpContextUtilities.GetHttpContext();
      if (context != null)
      {
        SaveSessionObjectDataToCookie(data, context);
        return;
      }

      SaveSessionObjectDataToAssemblyIsolatedStorage(data);
    }


    public SessionDataLoadingResult Load()
    {
      var context = HttpContextUtilities.GetHttpContext();
      if (context != null)
      {
        return LoadSessionObjectDataFromCookie(context);
      }
      return LoadSessionObjectDataFromAssemblyIsolatedStorage();
    }

    public void Remove()
    {
      var context = HttpContextUtilities.GetHttpContext();
      if (context != null)
      {
        RemoveSessionObjectDataFromCookie(context);
        return;
      }
      RemoveSessionObjectDataFromAssemblyIsolatedStorage();
    }

    #region Private methods

    private void RemoveSessionObjectDataFromCookie(HttpContextBase context)
    {
      context.DeleteCookie(_sessionDataCookieName);
    }

    private void RemoveSessionObjectDataFromAssemblyIsolatedStorage()
    {
      string filePath = GetIsolatedStorageFilePath();
      try
      {
        IsolatedStorageFile s = IsolatedStorageFile.GetUserStoreForAssembly();
        s.DeleteFile(filePath);
      }
      catch (Exception ex)
      {
        //Log.Debug(this, string.Format("Failed to remove session object data from isolated storage. Details: {0}", ex));
      }
    }

    private SessionDataLoadingResult LoadSessionObjectDataFromCookie(HttpContextBase context)
    {
      //for performance reason, try to load session object data from http context first
      if (HttpContext.Current.Items.Contains(_sessionDataCookieName))
      {
        SessionData data = (SessionData)HttpContext.Current.Items[_sessionDataCookieName];
        return new SessionDataLoadingResult(SessionDataLoadingStatus.Succeeded, data);
      }

      try
      {
        string authenticationCookieName = FormsAuthentication.FormsCookieName;
        HttpCookie authenticationCookie = context.Request.Cookies.Get(authenticationCookieName);
        if (authenticationCookie == null || string.IsNullOrEmpty(authenticationCookie.Value)) return SessionDataLoadingResult.NotExistedResult;


        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authenticationCookie.Value);

        HttpCookie sessionDataCookie = context.Request.Cookies.Get(_sessionDataCookieName);
        if (sessionDataCookie == null || string.IsNullOrEmpty(sessionDataCookie.Value)) return SessionDataLoadingResult.InvalidResult;

        string dataString = sessionDataCookie.Value;
        //Extract ticket, checksum and datastring from cookie
        //Regex regEx = new Regex("(.*)?\\*{5}(.*)?\\*{5}(.*)");
        //Match match = regEx.Match(dataString);
        //string ticket = match.Groups[2].Value;
        //string checksumFromCookie = match.Groups[3].Value;
        //dataString = match.Groups[1].Value;

        string[] l = dataString.Split(new[] { "*****" }, StringSplitOptions.None);
        string ticket = l[1];
        string checksumFromCookie = l[2];
        dataString = l[0];

        // Get the authenticated user name as part of the key, this is to make sure we have the correct 
        // Dashboard Session Data coupled with the right authentication cookie.

        if (authTicket != null)
        {
          string sessionId = authTicket.Name;
          //if (_securityUtility.UsernameContainsDomain(sessionId))
          //{
          //  // If we are running windows authentication, remove domain from username.
          //  sessionId = SecurityUtility.ExtractUsername(sessionId);
          //}

          #region Check checksum. Logout and go to login page if not valid.
          string generatedChecksum = CreateChecksum(dataString, ticket, sessionId);
          if (generatedChecksum != checksumFromCookie)
          {
            return SessionDataLoadingResult.InvalidResult;
          }
        }

        #endregion

        var stream = new MemoryStream(Convert.FromBase64String(dataString));
        var bin = new BinaryFormatter();
        var data = (SessionData)bin.Deserialize(stream);
        stream.Close();

        //Save for current request so we don't have to do this work for every requests during the same session
        SaveSessionObjectDataToHttpContext(data, context);
        return new SessionDataLoadingResult(SessionDataLoadingStatus.Succeeded, data);
      }
      catch (Exception ex)
      {
        //Log.Error(this, ex.ToString());
      }
      return SessionDataLoadingResult.NotExistedResult;
    }

    private SessionDataLoadingResult LoadSessionObjectDataFromAssemblyIsolatedStorage()
    {
      string filePath = GetIsolatedStorageFilePath();

      try
      {
        IsolatedStorageFile s = IsolatedStorageFile.GetUserStoreForAssembly();
        if (!s.FileExists(filePath)) return SessionDataLoadingResult.NotExistedResult;

        IsolatedStorageFileStream storage = new IsolatedStorageFileStream(filePath, FileMode.Open, s);

        byte[] buffer = new byte[storage.Length];
        storage.Read(buffer, 0, (int)storage.Length);
        storage.Close();

        string dataString = ASCIIEncoding.ASCII.GetString(buffer);
        MemoryStream stream = new MemoryStream(Convert.FromBase64String(dataString));

        BinaryFormatter bin = new BinaryFormatter();
        SessionData data = (SessionData)bin.Deserialize(stream);
        stream.Close();

        return new SessionDataLoadingResult(SessionDataLoadingStatus.Succeeded, data);
      }
      catch (Exception ex)
      {
        RemoveSessionObjectDataFromAssemblyIsolatedStorage();
      }
      return SessionDataLoadingResult.InvalidResult;
    }


    private static void SaveSessionObjectDataToAssemblyIsolatedStorage(SessionData data)
    {
      string filePath = GetIsolatedStorageFilePath();

      BinaryFormatter bin = new BinaryFormatter();
      MemoryStream stream = new MemoryStream();
      bin.Serialize(stream, data);

      string dataString = Convert.ToBase64String(stream.ToArray());
      stream.Close();

      IsolatedStorageFile s = IsolatedStorageFile.GetUserStoreForAssembly();
      IsolatedStorageFileStream storage = new IsolatedStorageFileStream(filePath, FileMode.Create, s);
      storage.Write(Encoding.ASCII.GetBytes(dataString), 0, dataString.Length);
      storage.Close();
    }

    private static string GetIsolatedStorageFilePath()
    {
      int processId = Process.GetCurrentProcess().Id;
      return string.Format("SessionData.txt-{0}", processId);
    }

    private void SaveSessionObjectDataToCookie(SessionData data, HttpContextBase context)
    {
      //for performance reason, save session object data to http context
      SaveSessionObjectDataToHttpContext(data, context);


      BinaryFormatter bin = new BinaryFormatter();
      MemoryStream stream = new MemoryStream();
      bin.Serialize(stream, data);

      string dataString = Convert.ToBase64String(stream.ToArray());
      stream.Close();

      string sessionId = data.Username;
      string ticket = string.Format("{0}{1}", DateTime.Now.Ticks, Guid.NewGuid());

      string chksum = CreateChecksum(dataString, ticket, sessionId);

      string cookieValue = string.Format("{0}*****{1}*****{2}", dataString, ticket, chksum);
      context.AddCookie(_sessionDataCookieName, cookieValue);
    }

    private void SaveSessionObjectDataToHttpContext(SessionData data, HttpContextBase context)
    {
      if (context.Items.Contains(_sessionDataCookieName))
      {
        context.Items[_sessionDataCookieName] = data;
      }
      else
      {
        context.Items.Add(_sessionDataCookieName, data);
      }
    }

    /// <summary>
    /// Creates a check by concatenating the supplied values with the EncryptionSalt
    /// </summary>
    /// <param name="dataString"></param>
    /// <param name="ticket"></param>
    /// <param name="sessionId"></param>
    /// <returns></returns>
    private static string CreateChecksum(string dataString, string ticket, string sessionId)
    {
      string unencryptedString = string.Format("{0}*****{1}*****{2}", dataString, ticket, sessionId.ToLower() + SystemConfiguration.Instance.SecuritySettings.EncryptionSalt);
      return Cryptography.CreateSHAHash(unencryptedString);
    }

    #endregion
  }
}