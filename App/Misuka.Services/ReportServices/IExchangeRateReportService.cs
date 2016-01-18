using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.DTO;
using Misuka.Domain.SearchCriteria;
using Misuka.Domain.Utilities;

namespace Misuka.Services.ReportServices
{
  public interface IExchangeRateReportService
  {
    ExchangeRateDTO GetById(Guid exchangeRateId);
    List<ExchangeRateDTO> GetAll();

    SearchResult<ExchangeRateDTO> Search(ExchangeRateSearchCriteria searchCriteria, int pageSize, int pageIndex);
  }
}

