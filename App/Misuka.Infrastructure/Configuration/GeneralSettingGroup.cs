namespace Misuka.Infrastructure.Configuration
{
  class GeneralSettingGroup : IGeneralSettingGroup
  {
    public string ConnectionString
    {
      get;
      set;
    }
  
    public string DefaultPassword { get; set; }
    public string UploadDocumentFolder { get; set; }
    public string UploadImgValidFileExtensions { get; set; }
    public string UploadDocumentValidFileExts { get; set; }
  }
}
