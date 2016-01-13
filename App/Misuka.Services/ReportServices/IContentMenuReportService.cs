﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.DTO;
using Misuka.Domain.Utilities;

namespace Misuka.Services.ReportServices
{
  public interface IContentMenuReportService
  {
    ContentMenuDTO GetById(Guid contentMenuId);
    List<ContentMenuDTO> GetAll();
  }
}
