using System.Collections.Generic;

namespace Misuka.Infrastructure.Configuration
{
  public interface ISystemConfiguration
  {
    ISecurityGroup SecuritySettings { get; }
    IGeneralSettingGroup GeneralSettings { get; }
    SmtpSettingConfiguration SmtpSettings { get; }
    void Initialize();
    void ReInitialize();
    Dictionary<string, string> GetConfigurationGroupDictionary(string groupName);
    bool ConfigurationGroupExists(string groupName);
  }
}
