using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.WebSiteLinks
{
  public class EditWebSiteLinkCommand
  {
    public Guid WebSiteLinkId { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
    public string ImageUrl { get; set; }

    public EditWebSiteLinkCommand(Guid webSiteLinkId, string name, string link, string imageUrl)
    {
      // TODO: Complete member initialization
      WebSiteLinkId = webSiteLinkId;
      Name = name;
      Link = link;
      ImageUrl = imageUrl;
    }
  }
}
