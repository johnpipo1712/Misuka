using System.Collections.Generic;
using System.Net.Configuration;

namespace Misuka.Infrastructure.Configuration
{
  public interface IConfigurationRetrievalStrategy
  {
    void Initialize();
    void ReInitialize();
    T GetCoreConfigurationValue<T>(string groupName, string key);
    Dictionary<string, string> GetConfigurationGroupDictionary(string groupName);
    bool ContainsConfigurationGroup(string groupName);
    SmtpSection SmtpConfigurationSection { get; }
  }
}