using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Services.CommandServices.ExchangeRates
{
  public class AddExchangeRateCommand
  {
    private string p;
    private decimal? nullable;

    public AddExchangeRateCommand(string p, decimal? nullable)
    {
      // TODO: Complete member initialization
      this.p = p;
      this.nullable = nullable;
    }

  }
}
