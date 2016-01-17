using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.Sliders
{
  public class DeleteSliderCommand
  {
    public IList<Guid> SelectedIds { get; set; }

    public DeleteSliderCommand(IList<Guid> selectedIds)
    {
      // TODO: Complete member initialization
      SelectedIds = selectedIds;
    }
  }
}
