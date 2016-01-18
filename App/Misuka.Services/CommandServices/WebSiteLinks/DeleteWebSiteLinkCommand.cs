using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.WebSiteLinks
{
  public class DeleteWebSiteLinkCommand
  {
    private IList<Guid> selectedIds;

    public DeleteWebSiteLinkCommand(IList<Guid> selectedIds)
    {
      // TODO: Complete member initialization
      this.selectedIds = selectedIds;
    }
  }
}
