using System;
using System.IO;
using Misuka.Infrastructure.Utilities;

namespace Misuka.Services.Services
{
  class FileService : IFileService
  {
    private const char PathSeparator = '\\';

    public string SaveFile(string folder, string filename, byte[] data)
    {
      return SaveFile(folder, filename, data, false);
    }

    public string SaveFile(string folder, string filename, byte[] data, bool isOverride)
    {
      if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
      string filePath = string.Format("{0}{1}{2}", folder, PathSeparator, filename);

      if (!File.Exists(filePath) || (File.Exists(filePath) && isOverride))
      {
        File.WriteAllBytes(filePath, data);
        return filename;
      }

      string newFilename = GenerateNewFileName(filename);
      string newFilePath = string.Format("{0}{1}{2}", folder, PathSeparator, newFilename);
      File.WriteAllBytes(newFilePath, data);
      return newFilename;

    }

    public void DeleteFile(string folder, string filename)
    {
      string filePath = string.Format("{0}{1}{2}", folder, PathSeparator, filename);
      DeleteFile(filePath);
    }

    public void DeleteFile(string filePath)
    {
      if (File.Exists(filePath)) File.Delete(filePath);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public string GenerateNewFileName(string filename)
    {
      return GenerateNewFileName(filename, true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="includingOldName"></param>
    /// <returns></returns>
    public string GenerateNewFileName(string filename, bool includingOldName)
    {
      string name, extension;
      FileUtilities.ParseFileName(filename, out name, out extension);
      return !includingOldName ? string.Format("{0}.{1}", DateTime.Now.Ticks, extension) : string.Format("{0}_{1}.{2}", name, DateTime.Now.Ticks, extension);
    }
  }
}