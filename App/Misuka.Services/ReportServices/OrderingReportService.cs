using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Misuka.Domain.DTO;
using Misuka.Domain.SearchCriteria;
using Misuka.Domain.Security;
using Misuka.Domain.Utilities;
using Misuka.Infrastructure.Data;
using Misuka.Services.ReportServices.Orderings;
using Misuka.Services.Services;

namespace Misuka.Services.ReportServices
{
  class OrderingReportService: IOrderingReportService
  {
    private readonly ICommandExecutor _executor;
    private readonly IOrderingService _orderingService;
    private readonly IUserSession _userSession;

    public OrderingReportService(ICommandExecutor executor, IOrderingService orderingService)
    {
      _executor = executor;
      _orderingService = orderingService;
      _userSession = new UserSession();
    }

  
    public OrderingDTO GetById(Guid orderingId)
    {
      var ordering = _orderingService.Find(orderingId);
      return Mapper.Map<Domain.Entity.Ordering, OrderingDTO>(ordering);
    }

    public List<OrderingDTO> GetAll()
    {
      var orderings = _orderingService.Queryable().ToList();
      return orderings.Select(Mapper.Map<Domain.Entity.Ordering, OrderingDTO>).ToList();
    
    }

    public SearchResult<OrderingDTO> Search(OrderingSearchCriteria searchCriteria, int pageSize, int pageIndex)
    {
      return _executor.Execute(new GetOrderingDTOBySearchCriteriaDbCommand(searchCriteria, pageIndex, pageSize));
    }

    public SearchResult<OrderingDTO> OrderingRetailOrders(OrderingSearchCriteria searchCriteria, int pageSize, int pageIndex)
    {
      return _executor.Execute(new GetOrderingRetailOrdersDTOSearchCriteriaDbCommand(searchCriteria, pageIndex, pageSize));
 
    }
  }
}

