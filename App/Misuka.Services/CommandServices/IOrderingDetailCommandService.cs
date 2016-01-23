using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Services.CommandServices.OrderingDetails;

namespace Misuka.Services.CommandServices
{
  public interface IOrderingDetailCommandService
  {
    Guid AddOrderingDetail(AddOrderingDetailCommand command);
  }
}
