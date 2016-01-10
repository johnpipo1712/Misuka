using System;

namespace Misuka.Infrastructure.Configuration
{
  public interface ISecurityGroup
  {
    string EncryptionKey { get; set; }
    string EncryptionSalt { get; set; }
    string EncryptionIV { get; set; }
    string AccessDeniedRedirect { get; set; }
    bool EnableInternetUser { get; set; }
    bool EnableRememberMe { get; set; }

    /// <summary>
    /// Can do everything. Add/edit/delete of vacation types, project types, categories
    /// </summary>
    Guid AdministratorRole { get; set; }
    /// <summary>
    /// Check if window authentication
    /// </summary>
    bool UseWindowAuthentication { get; set; }
  }
}