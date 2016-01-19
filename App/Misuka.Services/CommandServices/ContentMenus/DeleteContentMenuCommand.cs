using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.ContentMenus
{
  public class DeleteContentMenuCommand
  {
    public IList<Guid> SelectedIds { get; set; }

    public DeleteContentMenuCommand(IList<Guid> selectedIds)
    {
      // TODO: Complete member initialization
      SelectedIds = selectedIds;
    }
  }
}
