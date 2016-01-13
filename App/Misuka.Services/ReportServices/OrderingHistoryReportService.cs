using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Misuka.Domain.DTO;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Data;
using Misuka.Services.Services;

namespace Misuka.Services.ReportServices
{
  class OrderingHistoryReportService: IOrderingHistoryReportService
  {
    private readonly ICommandExecutor _executor;
    private readonly IOrderingHistoryService _orderingHistoryService;
    private readonly IUserSession _userSession;

    public OrderingHistoryReportService(ICommandExecutor executor, IOrderingHistoryService orderingHistoryService)
    {
      _executor = executor;
      _orderingHistoryService = orderingHistoryService;
      _userSession = new UserSession();
    }

  
    public OrderingHistoryDTO GetById(Guid orderingHistoryId)
    {
      var orderingHistory = _orderingHistoryService.Find(orderingHistoryId);
      return Mapper.Map<Domain.Entity.OrderingHistory, OrderingHistoryDTO>(orderingHistory);
    }

    public List<OrderingHistoryDTO> GetAll()
    {
      var orderingHistorys = _orderingHistoryService.Queryable().ToList();
      return orderingHistorys.Select(Mapper.Map<Domain.Entity.OrderingHistory, OrderingHistoryDTO>).ToList();
    
    }
  }
}

