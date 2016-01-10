namespace Misuka.Infrastructure.Configuration
{
  public interface IGeneralSettingGroup
  {
    string ConnectionString { get; set; }
    string DefaultPassword { get; set; }
    string UploadDocumentFolder { get; }
    string UploadImgValidFileExtensions { get; }
    string UploadDocumentValidFileExts { get; }
  }
}
