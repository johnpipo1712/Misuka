using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.ExchangeRates
{
  public class DeleteExchangeRateCommand
  {
    private IList<Guid> selectedIds;

    public DeleteExchangeRateCommand(IList<Guid> selectedIds)
    {
      // TODO: Complete member initialization
      this.selectedIds = selectedIds;
    }
  }
}
