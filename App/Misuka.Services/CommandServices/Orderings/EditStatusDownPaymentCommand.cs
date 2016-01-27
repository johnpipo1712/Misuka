using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.Orderings
{
  public class EditStatusDownPaymentCommand
  {
    public EditStatusDownPaymentCommand(Guid orderingId)
    {
      OrderingId = orderingId;
    }
    public Guid OrderingId { get; set; }
  
  }
}
