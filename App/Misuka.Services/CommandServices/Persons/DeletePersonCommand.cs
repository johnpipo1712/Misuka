using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Services.CommandServices.Persons
{
  public class DeletePersonCommand
  {
    private IList<Guid> selectedIds;

    public DeletePersonCommand(IList<Guid> selectedIds)
    {
      // TODO: Complete member initialization
      this.selectedIds = selectedIds;
    }
  }
}
