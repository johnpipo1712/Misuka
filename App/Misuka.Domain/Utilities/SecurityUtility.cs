using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using Misuka.Domain.Context;
using Misuka.Domain.Entity;
using Misuka.Domain.Enum;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.Data.ADO;
using Misuka.Infrastructure.EntityFramework.Factories;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;

namespace Misuka.Domain.Utilities
{
  public class SecurityUtility : ISecurityUtility
  {
    private MembershipSection GetMembershipProviderSection()
    {
      var membershipSection = (MembershipSection)ConfigurationManager.GetSection("system.web/membership");
      if (membershipSection == null)
        throw new ConfigurationErrorsException("Attempting to use MembershipProvider without a valid configuration section. Check that your web.config contains: system.web/membership section.");

      return membershipSection;
    }

    public bool IsPasswordEqual(string password, string encryptedPassword, string salt)
    {
      string newEncryptPassword = Cryptography.EncryptPassword(password, salt);
      return encryptedPassword.Equals(newEncryptPassword);
    }

    public ProviderSettings GetMembershipProviderSettings()
    {
      MembershipSection section = GetMembershipProviderSection();
      ProviderSettings settings = section.Providers[section.DefaultProvider];
      return settings;
    }

    public MembershipProvider GetMembershipProvider()
    {
      return (MembershipProvider)Activator.CreateInstance(Type.GetType(GetMembershipProviderSettings().Type));
    }

    public string AuthenticationCookieName
    {
      get { return FormsAuthentication.FormsCookieName; }
    }

    public string AuthenticationCookieValue
    {
      get { return GetCookieValueByName(AuthenticationCookieName); }
    }

    public string SessionDataCookieName
    {
      get { return AuthenticationCookieName + "SessionData"; }
    }

    public string GetCookieValueByName(string cookieName)
    {
      var request = HttpContext.Current.Request;
      if (request.Cookies[cookieName] == null)
      {
        return string.Empty;
      }
      return request.Cookies[cookieName].Value;
    }

    public IList<Guid> GetRoleIds(Guid userId)
    {
      IRepositoryProvider repositoryProvider = new RepositoryProvider(new RepositoryFactories());
      var unitofWork = new UnitOfWork(new MisukaDBContext(), repositoryProvider);
      return new List<Guid>();
    }

    public string CreateProvideUserKey(Guid personId, bool locked)
    {
      string providerUserKey = string.Format("{0}{1}", personId.ToString(), locked);
      providerUserKey = providerUserKey.Replace("-", "");
      return providerUserKey;
    }

    public static bool ValidateUserName(string username)
    {
      if (string.IsNullOrEmpty(username)) return false;
      var regExp = username.Contains("@") ? new Regex(RegexExpressions.SingleEmailValidation) : new Regex(RegexExpressions.UsernameValidation);
      return regExp.IsMatch(username) && username.Length <= 100 && username.Length >= 1;
    }

    public bool IsUsernameValid(string username)
    {
      if (username == null)
      {
        return false;
      }
      var realUsername = username;
      if (username.Contains("\\"))
      {
        var splitUsername = username.Split('\\');
        #region Validate domain
        var domain = splitUsername[0];
        var regExp = new Regex(RegexExpressions.DomainValidation);
        if (string.IsNullOrEmpty(domain) || regExp.IsMatch(domain) == false)
        {
          return false;
        }
        #endregion

        realUsername = splitUsername[1];
      }
      #region Validate username
      return ValidateUserName(realUsername);
      #endregion
    }

    public bool UsernameContainsDomain(string username)
    {
      Regex regExp = new Regex("^[^\\\\]+\\\\\\S+$");
      return regExp.IsMatch(username);
    }

    public bool IsPasswordValid(string password)
    {
      MembershipProvider provider = GetMembershipProvider();
      if (provider.MinRequiredNonAlphanumericCharacters > 0)
      {
        Regex regExp = new Regex("[^a-zA-Z0-9]");
        if (regExp.Matches(password).Count < provider.MinRequiredNonAlphanumericCharacters)
        {
          return false;
        }
      }

      if (password.Length < provider.MinRequiredPasswordLength)
      {
        return false;
      }

      if (password.Length > 256)
      {
        // Database maximum length is 256
        return false;
      }

      if (provider.PasswordStrengthRegularExpression.Trim() != string.Empty)
      {
        Regex regExp = new Regex(provider.PasswordStrengthRegularExpression.Trim());
        if (!regExp.IsMatch(password))
        {
          return false;
        }
      }

      return true;
    }

    /// <summary>
    /// Extracts the pure username from a username with both domain and username
    /// format domain\username
    /// </summary>
    /// <param name="baseUsername"></param>
    /// <returns></returns>
    public string ExtractUsername(string baseUsername)
    {
      if (!UsernameContainsDomain(baseUsername))
      {
        return baseUsername;
      }

      Regex regExp = new Regex("^[^\\\\]+\\\\(\\S+)$");
      string username = "";
      if (regExp.IsMatch(baseUsername))
      {
        username = regExp.Match(baseUsername).Groups[1].Value;
      }

      return username;
    }

    /// <summary>
    /// Extracts the domain part of a username containing both username and domain.
    /// format: domain\username
    /// </summary>
    /// <param name="baseUsername"></param>
    /// <returns></returns>
    public string ExtractDomain(string baseUsername)
    {
      if (!UsernameContainsDomain(baseUsername))
      {
        return String.Empty;
      }

      Regex regExp = new Regex("^([^\\\\]+)\\\\\\S+$");
      string domain = "";
      if (regExp.IsMatch(baseUsername))
      {
        domain = regExp.Match(baseUsername).Groups[1].Value;
      }

      return domain;
    }

    public bool IsMemberOfRole(Guid userId, Guid roleId)
    {
      IRepositoryProvider repositoryProvider = new RepositoryProvider(new RepositoryFactories());
      var unitofWork = new UnitOfWork(new MisukaDBContext(), repositoryProvider);
     // var user = unitofWork.RepositoryAsync<RolePerson>().Query(rp => rp.PersonId == userId && rp.RoleId == roleId).Select();

      return true;
    }

    public User GetUserByUsername(string userName)
    {
      bool containDomain = UsernameContainsDomain(userName);
      if (!containDomain) return GetUserWithoutDomain(userName);
      else
      {
        var domain = ExtractDomain(userName);
        var normalUsername = ExtractUsername(userName);
        return GetUserWithDomain(normalUsername, domain);
      }

    }

    
    private User GetUserWithoutDomain(string username,int type)
    {
      IRepositoryProvider repositoryProvider = new RepositoryProvider(new RepositoryFactories());
      var unitofWork = new UnitOfWork(new MisukaDBContext(), repositoryProvider);
      var user = unitofWork.RepositoryAsync<User>()
         .Query(m => string.Compare(m.UserName, username, StringComparison.InvariantCultureIgnoreCase) == 0 && m.Type == type)
         .Select()
         .FirstOrDefault();

      return user;
    }
    private User GetUserWithDomain(string username, string domain, int type)
    {
      IRepositoryProvider repositoryProvider = new RepositoryProvider(new RepositoryFactories());
      var unitofWork = new UnitOfWork(new MisukaDBContext(), repositoryProvider);
      var user = unitofWork.RepositoryAsync<User>()
         .Query(
                  m => string.Compare(m.UserName, username, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                  string.Compare(m.Domain, domain, StringComparison.InvariantCultureIgnoreCase) == 0 && m.Type == type
           )
         .Select()
         .FirstOrDefault();

      return user;
    }
    private User GetUserWithoutDomain(string username)
    {
      IRepositoryProvider repositoryProvider = new RepositoryProvider(new RepositoryFactories());
      var unitofWork = new UnitOfWork(new MisukaDBContext(), repositoryProvider);
      var user = unitofWork.RepositoryAsync<User>()
         .Query(m => string.Compare(m.UserName, username, StringComparison.InvariantCultureIgnoreCase) == 0)
         .Select()
         .FirstOrDefault();

      return user;
    }
    private User GetUserWithDomain(string username, string domain)
    {
      IRepositoryProvider repositoryProvider = new RepositoryProvider(new RepositoryFactories());
      var unitofWork = new UnitOfWork(new MisukaDBContext(), repositoryProvider);
      var user = unitofWork.RepositoryAsync<User>()
         .Query(
                  m => string.Compare(m.UserName, username, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                  string.Compare(m.Domain, domain, StringComparison.InvariantCultureIgnoreCase) == 0
           )
         .Select()
         .FirstOrDefault();

      return user;
    }

    public void UpdateUserInformation(User loggingUser)
    {
      IRepositoryProvider repositoryProvider = new RepositoryProvider(new RepositoryFactories());
      var unitofWork = new UnitOfWork(new MisukaDBContext(), repositoryProvider);
      unitofWork.RepositoryAsync<User>().Update(loggingUser);
      unitofWork.SaveChanges();
    }

    public User GetUserByUsernameAndType(string userName, int type)
    {
      bool containDomain = UsernameContainsDomain(userName);
      if (!containDomain) return GetUserWithoutDomain(userName,type);
      else
      {
        var domain = ExtractDomain(userName);
        var normalUsername = ExtractUsername(userName);
        return GetUserWithDomain(normalUsername, domain, type);
      };
    }

    public IList<int> GetPermissions(Guid userId)
    {
      var permissions = new List<int>();
      ADO.ExecuteDataReader(CommandType.StoredProcedure, "Misuka.GetUserAccessiblePermissions", dataReader =>
      {
        using (var reader = new SafeDataReader(dataReader))
        {
          while (reader.Read())
          {
            permissions.Add(reader.GetInt32("PermissionId"));
          }
        }
      },
      SQLParameter.CreateParameter("UserId", DbType.Guid, userId));

      return permissions;
    }

    public User GetUserByUsername(string username, int type)
    {
      bool containDomain = UsernameContainsDomain(username);
      if (!containDomain) return GetUserWithoutDomain(username, type);
      else
      {
        var domain = ExtractDomain(username);
        var normalUsername = ExtractUsername(username);
        return GetUserWithDomain(normalUsername, domain, type);
      }
    }
  }
}
