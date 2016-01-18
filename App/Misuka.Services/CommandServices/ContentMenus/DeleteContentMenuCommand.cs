using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.ContentMenus
{
  public class DeleteContentMenuCommand
  {
    private IList<Guid> selectedIds;

    public DeleteContentMenuCommand(IList<Guid> selectedIds)
    {
      // TODO: Complete member initialization
      this.selectedIds = selectedIds;
    }
  }
}
