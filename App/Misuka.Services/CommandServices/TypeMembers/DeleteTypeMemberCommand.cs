using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Services.CommandServices.TypeMembers
{
  public class DeleteTypeMemberCommand
  {
    private IList<Guid> selectedIds;

    public DeleteTypeMemberCommand(IList<Guid> selectedIds)
    {
     this.selectedIds = selectedIds;
    }
  }
}
