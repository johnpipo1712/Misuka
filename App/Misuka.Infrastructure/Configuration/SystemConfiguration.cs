using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace Misuka.Infrastructure.Configuration
{
  public class SystemConfiguration : ISystemConfiguration
  {
    public static readonly ISystemConfiguration Instance = new SystemConfiguration();

    #region Fields
    private bool _initialized;
    private readonly IConfigurationRetrievalStrategy _retrievalStrategy;
    private ISecurityGroup _securitySettings;
    private IGeneralSettingGroup _baseSettings;
    private SmtpSettingConfiguration _smtpSettings;
    #endregion

    #region  Properties
    public ISecurityGroup SecuritySettings
    {
      get
      {
        if (!_initialized)
          Initialize();
        return _securitySettings;
      }
    }

    public IGeneralSettingGroup GeneralSettings
    {
      get
      {
        if (!_initialized)
          Initialize();

        return _baseSettings;
      }
    }

    public SmtpSettingConfiguration SmtpSettings
    {
      get
      {
        if (!_initialized)
          Initialize();
        return _smtpSettings;
      } 
    }

    #endregion

    #region  Constructors

    public SystemConfiguration()
    {
      _retrievalStrategy = ConfigurationRetrievalStrategy.Instance;
    }

    #endregion

    #region  Methods

    public void Initialize()
    {
      if (!_initialized)
      {
        _retrievalStrategy.Initialize();
        _securitySettings = new SecurityGroup
        {
          EncryptionKey = _retrievalStrategy.GetCoreConfigurationValue<string>("Security", "EncryptionKey"),
          EncryptionSalt = _retrievalStrategy.GetCoreConfigurationValue<string>("Security", "EncryptionSalt"),
          EncryptionIV = _retrievalStrategy.GetCoreConfigurationValue<string>("Security", "EncryptionIV"),
          AccessDeniedRedirect = _retrievalStrategy.GetCoreConfigurationValue<string>("Security", "AccessDeniedRedirect"),
          EnableInternetUser = _retrievalStrategy.GetCoreConfigurationValue<bool>("Security", "EnableInternetUser"),
          EnableRememberMe = _retrievalStrategy.GetCoreConfigurationValue<bool>("Security", "EnableRememberMe"),

          AdministratorRole = _retrievalStrategy.GetCoreConfigurationValue<Guid>("Security", "AdministratorRole"),
          UseWindowAuthentication = _retrievalStrategy.GetCoreConfigurationValue<bool>("Security", "UseWindowAuthentication")
        };

        _baseSettings = new GeneralSettingGroup()
        {
          ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString,
          DefaultPassword = _retrievalStrategy.GetCoreConfigurationValue<string>("General", "DefaultPassword"),
          UploadDocumentFolder = _retrievalStrategy.GetCoreConfigurationValue<string>("General", "UploadDocumentFolder"),
          UploadImgValidFileExtensions = _retrievalStrategy.GetCoreConfigurationValue<string>("General", "UploadImgValidFileExts"),
          UploadDocumentValidFileExts = _retrievalStrategy.GetCoreConfigurationValue<string>("General", "UploadDocumentValidFileExts"),
        };


        _smtpSettings = new SmtpSettingConfiguration()
        {
          SmtpFrom = _retrievalStrategy.SmtpConfigurationSection.From
        };
        _initialized = true;
      }
    }

    public void ReInitialize()
    {
      _initialized = false;
      Initialize();
    }

    public Dictionary<string, string> GetConfigurationGroupDictionary(string groupName)
    {
      return _retrievalStrategy.GetConfigurationGroupDictionary(groupName);
    }

    public bool ConfigurationGroupExists(string groupName)
    {
      return _retrievalStrategy.ContainsConfigurationGroup(groupName);
    }
    #endregion
  }
}
