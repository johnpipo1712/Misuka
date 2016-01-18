using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Services.CommandServices.ExchangeRates;

namespace Misuka.Services.CommandServices
{
  public interface IExchangeRateCommandService
  {
    void DeleteExchangeRate(DeleteExchangeRateCommand command);

    Guid AddExchangeRate(AddExchangeRateCommand command);

    void EditExchangeRate(EditExchangeRateCommand command);
  }
}
