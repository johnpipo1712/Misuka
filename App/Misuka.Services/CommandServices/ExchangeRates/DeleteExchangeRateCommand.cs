using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.ExchangeRates
{
  public class DeleteExchangeRateCommand
  {
    public IList<Guid> SelectedIds { get; set; }

    public DeleteExchangeRateCommand(IList<Guid> selectedIds)
    {
      // TODO: Complete member initialization
      SelectedIds = selectedIds;
    }
  }
}
