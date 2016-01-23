using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.Orderings
{
  public class EditOrderingFollowingDoneCommand
  {
    public EditOrderingFollowingDoneCommand(Guid orderingId, bool isPaid, bool isDelivered)
    {
      OrderingId = orderingId;
      IsPaid = isPaid;
      IsDelivered = isDelivered;
    }
    public Guid OrderingId { get; set; }
 
    public bool? IsPaid { get; set; }

    public bool? IsDelivered { get; set; }
 
  }
}
