using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.Orderings
{
  public class EditOrderingFollowingUSDCommand
  {
    public EditOrderingFollowingUSDCommand(Guid orderingId, double transportFee, decimal weightFee)
    {
      OrderingId = orderingId;
      TransportFee = transportFee;
      WeightFee = weightFee;
    }
    public Guid OrderingId { get; set; }

    public double TransportFee { get; set; }

    public decimal WeightFee { get; set; }
 
  }
}
