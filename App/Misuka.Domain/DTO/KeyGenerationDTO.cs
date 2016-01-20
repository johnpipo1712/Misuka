using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.DTO
{
  public class KeyGenerationDTO
  {
    public string KeyType { get; set; }
    public int CurrentIndex { get; set; }
    public string Prefix { get; set; }
    public string CurrentString { get; set; }
    public string PrefixString
    {
      get
      {
        if (!string.IsNullOrEmpty(Prefix))
          return Prefix + "-";
        return Prefix;
      }
    }
    public string CodeNew { get; set; }
  }
}
