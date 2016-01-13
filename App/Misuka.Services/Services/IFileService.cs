namespace Misuka.Services.Services
{
  public interface IFileService
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="filename"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    string SaveFile(string folder, string filename, byte[] data);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="filename"></param>
    /// <param name="data"></param>
    /// <param name="overwrite"></param>
    /// <returns></returns>
    string SaveFile(string folder, string filename, byte[] data, bool overwrite);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    string GenerateNewFileName(string filename);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="includingOldName"></param>
    /// <returns></returns>
    string GenerateNewFileName(string filename, bool includingOldName);

    void DeleteFile(string folder, string filename);
    void DeleteFile(string filePath);
  }
}
