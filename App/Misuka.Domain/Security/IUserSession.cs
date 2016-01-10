using System;
using System.Security.Principal;

namespace Misuka.Domain.Security
{
  public interface IUserSession
  {
    Guid UserId { get; }
    string Username { get; }
    string FirstName { get; }
    string LastName { get; }
    string Email { get; }
    string FullName { get;  }

    IPrincipal Principal { get; }
    bool IsAuthenticated();
    bool IsAdministrator();
    bool HasPermission(Guid roleId);
    bool HasPermissionOnPermission(int permissionId);
  }
}