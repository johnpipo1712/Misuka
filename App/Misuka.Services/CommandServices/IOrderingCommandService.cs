using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Services.CommandServices.Orderings;

namespace Misuka.Services.CommandServices
{
  public interface IOrderingCommandService
  {
    Guid AddOrdering(AddOrderingCommand command);

    void EditOrdering(EditOrderingCommand command);
  }
}
