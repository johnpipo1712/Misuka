using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.ExchangeRates
{
  public class EditExchangeRateCommand
  {
    public Guid ExchangeRateId { get; set; }
    public string Name { get; set; }
    public Decimal Price { get; set; }

    public EditExchangeRateCommand(Guid exchangeRateId, string name, Decimal price)
    {
      // TODO: Complete member initialization
      ExchangeRateId = exchangeRateId;
      Name = name;
      Price = price;
    }
  }
}
