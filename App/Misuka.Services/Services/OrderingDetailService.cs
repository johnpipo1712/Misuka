using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misuka.Domain.Entity;
using Misuka.Infrastructure.EntityFramework.Repositories;
using Misuka.Infrastructure.EntityFramework.Services;

namespace Misuka.Services.Services
{
  class OrderingDetailService : Service<Domain.Entity.OrderingDetail>, IOrderingDetailService
  {
    public OrderingDetailService(IRepositoryAsync<OrderingDetail> repository)
      : base(repository)
    {
    }
  }
}
