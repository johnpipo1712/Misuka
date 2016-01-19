using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Services.CommandServices.WebSiteLinks
{
  public class AddWebSiteLinkCommand
  {
    public string Name { get; set; }
    public string Link { get; set; }
    public string ImageUrl { get; set; }
    public AddWebSiteLinkCommand(string name, string link, string imageUrl)
    {
      Name = name;
      Link = link;
      ImageUrl = imageUrl;
    }
  }
}
