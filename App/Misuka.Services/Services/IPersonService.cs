﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misuka.Infrastructure.EntityFramework.Services;

namespace Misuka.Services.Services
{
  public interface IPersonService : IService<Domain.Entity.Person>
  {
  }
}
