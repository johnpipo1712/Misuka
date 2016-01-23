using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Services.CommandServices.Orderings
{
  public class AddOrderingCommand
  {
    public AddOrderingCommand(Guid personId, Guid exchangeRateId, string phone, string address, string note, double totalDiscuss)
    {
      PersonId = personId;
      ExchangeRateId = exchangeRateId;
      Phone = phone;
      Address = address;
      Note = note;
      TotalDiscuss = totalDiscuss;
    }
    public Guid PersonId { get; set; }

    public Guid ExchangeRateId { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public string Note { get; set; }

    public double TotalDiscuss { get; set; }


  }
}
