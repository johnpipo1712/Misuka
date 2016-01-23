using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.Orderings
{
  public class EditStatusDownPaymentCommand
  {
    public EditStatusDownPaymentCommand(Guid orderingId, bool isDownPayment)
    {
      OrderingId = orderingId;
      IsDownPayment = isDownPayment;
    }
    public Guid OrderingId { get; set; }
    public bool IsDownPayment { get; set; }
  
  }
}
