using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Services.CommandServices.Orderings
{
  public class EditStatusRejectCommand
  {
    public EditStatusRejectCommand(Guid orderingId)
    {
      OrderingId = orderingId;
    }
    public Guid OrderingId { get; set; }

  }
}
