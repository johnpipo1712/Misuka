using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Misuka.Infrastructure.Utilities
{
  public class FileUtilities
  {
    private const char DefaultPathSeparator = '\\';
    private const string DefaultFolder = "C:";
    private static readonly Regex InvalidFileNameCharacterRegex = new Regex("[^abcdefghijklmnopqrstuvwxyzæøå0123456789_\\.\\s]+", RegexOptions.IgnoreCase | RegexOptions.Multiline);

    public static void ParseFilePath(string filePath, out string folder, out string filename)
    {
      ParseFilePath(filePath, DefaultPathSeparator, out folder, out filename);
    }

    public static void ParseFilePath(string filePath, char pathSeparator, out string folder, out string filename)
    {
      if (string.IsNullOrEmpty(filePath))
      {
        folder = filename = string.Empty;
        return;
      }

      int lastSeparatorIndex = filePath.LastIndexOf(pathSeparator);
      if (lastSeparatorIndex >= 0)
      {
        folder = filePath.Substring(0, lastSeparatorIndex);
        filename = filePath.Substring(lastSeparatorIndex + 1, filePath.Length - lastSeparatorIndex - 1);
      }
      else
      {
        folder = DefaultFolder;
        filename = filePath;
      }
    }

    public static void ParseFileName(string fileName, out string name, out string extension)
    {
      if (string.IsNullOrEmpty(fileName))
      {
        name = extension = string.Empty;
        return;
      }

      string examinedFileName = fileName;
      if (fileName.IndexOf(DefaultPathSeparator) >= 0)
      {
        string path;
        ParseFilePath(fileName, out path, out examinedFileName);
      }

      int lastDot = examinedFileName.LastIndexOf('.');
      if (lastDot >= 0)
      {
        name = examinedFileName.Substring(0, lastDot);
        extension = examinedFileName.Substring(lastDot + 1, examinedFileName.Length - lastDot - 1);
      }
      else
      {
        name = examinedFileName;
        extension = string.Empty;
      }
    }

    /// <summary>
    /// Removes all invalid filename characters, such as \ and /
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string RemoveInvalidFileNameCharacters(string fileName)
    {
      if (string.IsNullOrEmpty(fileName)) return null;
      return InvalidFileNameCharacterRegex.Replace(fileName, "_").Trim();
    }

    /// <summary>
    /// Read bytes array from an input stream and returns a bytes array
    /// </summary>
    /// <param name="stream">Stream input that is expected to read to bytes array</param>
    /// <returns>Return bytes array of input stream</returns>
    public static byte[] ReadBytesFromStream(Stream stream)
    {
      int length = (int)stream.Length;
      byte[] buffer = new byte[length];

      int offset = 0;
      int byteRead = 0;
      int remainBytes = length;
      stream.Position = 0;
      while (remainBytes > 0)
      {
        byteRead = (remainBytes > 1024) ? stream.Read(buffer, offset, 1024) : stream.Read(buffer, offset, remainBytes);
        if (byteRead < 1024) break;

        offset += byteRead;
        remainBytes -= byteRead;
      }

      return buffer;
    }
    public static string GetFileName(HttpPostedFileBase postedFile)
    {
      return postedFile == null ? string.Empty : postedFile.FileName;
    }

    public static Stream GetFileStream(HttpPostedFileBase postedFile)
    {
      return postedFile == null ? null : postedFile.InputStream;
    }

    public static string NormalizeFileName(string value)
    {
      if (value == null) return string.Empty;
      return new Regex(string.Format("[{0}]", Regex.Escape(@"<>:""/\|?*#"))).Replace(value, string.Empty).Trim();
    }
  }
}