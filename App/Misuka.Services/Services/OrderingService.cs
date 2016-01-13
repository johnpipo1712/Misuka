﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misuka.Domain.Entity;
using Misuka.Infrastructure.EntityFramework.Repositories;
using Misuka.Infrastructure.EntityFramework.Services;

namespace Misuka.Services.Services
{
  class OrderingService : Service<Domain.Entity.Ordering>, IOrderingService
  {
    public OrderingService(IRepositoryAsync<Ordering> repository)
      : base(repository)
    {
    }
  }
}
