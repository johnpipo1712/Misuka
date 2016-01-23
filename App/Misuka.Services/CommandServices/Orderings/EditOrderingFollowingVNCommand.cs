using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.Orderings
{
  public class EditOrderingFollowingVNCommand
  {
    public EditOrderingFollowingVNCommand(Guid orderingId)
    {
      OrderingId = orderingId;
   


    }
    public Guid OrderingId { get; set; }

   
 
  }
}
