using System.Configuration;
using System.Xml;

namespace Misuka.Infrastructure.Configuration
{
  public class TimeSection : IConfigurationSectionHandler
  {
    public object Create(object parent, object configContext, XmlNode section)
    {
      return section;
    }
  }
}