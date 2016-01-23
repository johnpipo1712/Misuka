using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Misuka.Services.CommandServices.Orderings
{
  public class EditOrderingCommand
  {
    public EditOrderingCommand(Guid orderingId, string phone, string address, string note)
    {
      OrderingId = orderingId;
      Phone = phone;
      Address = address;
      Note = note;
    
    }
    public Guid OrderingId { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public string Note { get; set; }

  }
}
