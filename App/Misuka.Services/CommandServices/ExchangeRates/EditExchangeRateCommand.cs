using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.ExchangeRates
{
  public class EditExchangeRateCommand
  {
    private Guid guid;
    private string p;
    private decimal? nullable;

    public EditExchangeRateCommand(Guid guid, string p, decimal? nullable)
    {
      // TODO: Complete member initialization
      this.guid = guid;
      this.p = p;
      this.nullable = nullable;
    }
  }
}
