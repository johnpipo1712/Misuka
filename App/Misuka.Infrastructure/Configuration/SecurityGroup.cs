using System;

namespace Misuka.Infrastructure.Configuration
{
  class SecurityGroup : ISecurityGroup
  {
    public string EncryptionKey { get; set; }
    public string EncryptionSalt { get; set; }
    public string EncryptionIV { get; set; }
    public string AccessDeniedRedirect { get; set; }
    public bool EnableInternetUser { get; set; }
    public bool EnableRememberMe { get; set; }

    public Guid AdministratorRole { get; set; }
    public bool UseWindowAuthentication { get; set; }
  }
}
