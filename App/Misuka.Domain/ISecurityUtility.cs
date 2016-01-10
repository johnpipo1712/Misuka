using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Security;
using Misuka.Domain.Entity;

namespace Misuka.Domain
{
  public interface ISecurityUtility
  {
    bool IsPasswordEqual(string password, string encryptedPassword, string salt);
    String CreateProvideUserKey(Guid personId, bool locked);
    ProviderSettings GetMembershipProviderSettings();
    MembershipProvider GetMembershipProvider();
    string AuthenticationCookieName { get; }
    string AuthenticationCookieValue { get; }
    string SessionDataCookieName { get; }
    string GetCookieValueByName(string cookieName);
    IList<Guid> GetRoleIds(Guid userId);

    bool IsUsernameValid(string username);
    bool UsernameContainsDomain(string username);
    bool IsPasswordValid(string password);
    string ExtractUsername(string baseUsername);
    string ExtractDomain(string baseUsername);

    bool IsMemberOfRole(Guid userId, Guid roleId);
    User GetUserByUsername(string userName);
    void UpdateUserInformation(User loggingUser);

    IList<int> GetPermissions(Guid userId);
  }
}
