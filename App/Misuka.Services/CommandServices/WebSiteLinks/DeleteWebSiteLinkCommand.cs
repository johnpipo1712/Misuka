using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.WebSiteLinks
{
  public class DeleteWebSiteLinkCommand
  {
    public IList<Guid> SelectedIds { get; set; }

    public DeleteWebSiteLinkCommand(IList<Guid> selectedIds)
    {
      // TODO: Complete member initialization
      SelectedIds = selectedIds;
    }
  }
}
