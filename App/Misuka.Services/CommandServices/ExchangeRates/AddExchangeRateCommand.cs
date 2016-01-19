using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Services.CommandServices.ExchangeRates
{
  public class AddExchangeRateCommand
  {
    public string Name { get; set; }
    public Decimal Price { get; set; }

    public AddExchangeRateCommand(string name, Decimal price)
    {
      // TODO: Complete member initialization
      Name = name;
      Price = price;

    }
  }
}
