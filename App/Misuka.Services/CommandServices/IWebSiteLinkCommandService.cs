using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Services.CommandServices.WebSiteLinks;

namespace Misuka.Services.CommandServices
{
  public interface IWebSiteLinkCommandService
  {
    void DeleteWebSiteLink(DeleteWebSiteLinkCommand command);

    Guid AddWebSiteLink(AddWebSiteLinkCommand command);

    void EditWebSiteLink(EditWebSiteLinkCommand command);
  }
}
