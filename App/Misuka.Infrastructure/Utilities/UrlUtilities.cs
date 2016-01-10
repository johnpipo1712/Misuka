using System.Text.RegularExpressions;

namespace Misuka.Infrastructure.Utilities
{
  public class UrlUtilities
  {
    private static readonly Regex ResourceFileRegex = new Regex("^([abcdefghijklmnopqrstuvwxyz_\\-\\.0123456789\\+\\/\\:\\%]*)\\.(jpg|jpeg|png|gif|bmp|ico|xls|htm|html|xlsx|doc|docx|js|css|tiff|tif|ttf|fnt|fon|otf|swf|mkv|flv|mov|wmf|wmv|mpeg|mpg|axd)(\\?.*)?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);


    /// <summary>
    /// Checks if requested url is for static resource
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static bool IsCommonResource(string url)
    {
      return ResourceFileRegex.IsMatch(url ?? string.Empty);
    }
  }
}