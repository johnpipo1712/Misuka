using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.DTO;
using Misuka.Domain.SearchCriteria;
using Misuka.Domain.Utilities;

namespace Misuka.Services.ReportServices
{
  public interface IOrderingReportService
  {
    OrderingDTO GetById(Guid orderingId);
    List<OrderingDTO> GetAll();

    SearchResult<OrderingDTO> Search(OrderingSearchCriteria searchCriteria, int pageSize, int pageIndex);
    SearchResult<OrderingDTO> OrderingRetailOrders(OrderingSearchCriteria searchCriteria, int pageSize, int pageIndex);
  }
}

