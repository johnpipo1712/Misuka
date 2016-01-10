using AutoMapper;
using Misuka.Web.Helpers;

namespace Misuka.Web
{
  public class WebObjectMapper
  {
    private bool _mapped;

    public void Map()
    {
      if (!_mapped)
      {
       
        _mapped = true;
      }
    }
  }
}