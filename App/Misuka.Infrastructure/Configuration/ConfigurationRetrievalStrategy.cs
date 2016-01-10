using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Configuration;
using System.Xml;

namespace Misuka.Infrastructure.Configuration
{
  internal class ConfigurationRetrievalStrategy : IConfigurationRetrievalStrategy
  {
    public static readonly IConfigurationRetrievalStrategy Instance = new ConfigurationRetrievalStrategy();
    private bool _initialized;
    private Dictionary<string, Dictionary<string, string>> _configurationGroups;
    private SmtpSection _smtpSection;

    private ConfigurationRetrievalStrategy()
    {
    }

    public void Initialize()
    {
      if (!_initialized)
      {
        XmlNode section = (XmlNode)ConfigurationManager.GetSection("UPSTimeSettings");
        _configurationGroups = ParseSection(section);
        _smtpSection = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection;
        _initialized = true;
      }
    }

    public void ReInitialize()
    {
      _initialized = false;
      Initialize();
    }

    public T GetCoreConfigurationValue<T>(string groupName, string key)
    {
      if (!_configurationGroups.ContainsKey(groupName.ToLower()))
      {
        throw new ConfigurationErrorsException(string.Format("The Group \"{0}\" does not exist in the section of web.config", groupName));
      }
      Dictionary<string, string> configurationGroup = _configurationGroups[groupName.ToLower()];
      if (!configurationGroup.ContainsKey(key.ToLower()))
      {
        throw new ConfigurationErrorsException(string.Format("The Property \"{0}\" does not exist in the Group \"{1}\" of the section in web.config", key, groupName));
      }
      string value = configurationGroup[key.ToLower()];
      return (T)ConvertValue(typeof(T), value);
    }

    public Dictionary<string, string> GetConfigurationGroupDictionary(string groupName)
    {
      if (!_configurationGroups.ContainsKey(groupName.ToLower()))
      {
        throw new ConfigurationErrorsException(string.Format("The Group \"{0}\" does not exist in the section of web.config", groupName));
      }
      return _configurationGroups[groupName.ToLower()];
    }

    public bool ContainsConfigurationGroup(string groupName)
    {
      return _configurationGroups.ContainsKey(groupName.ToLower());
    }

    private object ConvertValue(Type targetType, string value)
    {
      if (targetType == typeof(string))
      {
        return value;
      }
      try
      {
        object obj = typeof(Guid) == targetType ? new Guid(value) : Convert.ChangeType(value, targetType);
        return obj;
      }
      catch (Exception e)
      {
        throw new Exception(string.Format("Could not convert from '{0}' to {1}", value, targetType.FullName), e);
      }
    }

    public Dictionary<string, Dictionary<string, string>> ParseSection(XmlNode section)
    {
      Dictionary<string, Dictionary<string, string>> configurationGroups = new Dictionary<string, Dictionary<string, string>>();
      XmlNodeList groups = section.SelectNodes("Group");
      if (groups != null)
      {
        foreach (XmlNode group in groups)
        {
          if (group.NodeType == XmlNodeType.Element)
          {
            if (group.Attributes != null)
            {
              XmlAttribute groupNameAttribute = group.Attributes["name"];
              if (groupNameAttribute != null)
              {
                Dictionary<string, string> groupValues = new Dictionary<string, string>();
                string groupName = groupNameAttribute.Value.ToLower();
                foreach (XmlNode groupVariable in group.ChildNodes)
                {
                  if (groupVariable.NodeType == XmlNodeType.Element)
                  {
                    if (groupVariable.Attributes != null)
                    {
                      XmlAttribute propertyName = groupVariable.Attributes["name"];
                      XmlAttribute propertyValue = groupVariable.Attributes["value"];
                      string variableName = propertyName.Value.ToLower();
                      string value = propertyValue.Value;
                      if (!groupValues.ContainsKey(variableName))
                      {
                        groupValues.Add(variableName, value);
                      }
                      else
                      {
                        groupValues[variableName] = value;
                      }
                    }
                  }
                }
                if (!configurationGroups.ContainsKey(groupName))
                {
                  configurationGroups.Add(groupName, groupValues);
                }
                else
                {
                  configurationGroups[groupName] = groupValues;
                }
              }
            }
          }
        }
      }
      return configurationGroups;
    }


    public SmtpSection SmtpConfigurationSection
    {
      get { return _smtpSection; }
    }
  }
}