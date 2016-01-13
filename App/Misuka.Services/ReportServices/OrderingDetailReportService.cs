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
  class OrderingDetailReportService: IOrderingDetailReportService
  {
    private readonly ICommandExecutor _executor;
    private readonly IOrderingDetailService _orderingDetailService;
    private readonly IUserSession _userSession;

    public OrderingDetailReportService(ICommandExecutor executor, IOrderingDetailService orderingDetailService)
    {
      _executor = executor;
      _orderingDetailService = orderingDetailService;
      _userSession = new UserSession();
    }

  
    public OrderingDetailDTO GetById(Guid orderingDetailId)
    {
      var orderingDetail = _orderingDetailService.Find(orderingDetailId);
      return Mapper.Map<Domain.Entity.OrderingDetail, OrderingDetailDTO>(orderingDetail);
    }

    public List<OrderingDetailDTO> GetAll()
    {
      var orderingDetails = _orderingDetailService.Queryable().ToList();
      return orderingDetails.Select(Mapper.Map<Domain.Entity.OrderingDetail, OrderingDetailDTO>).ToList();
    
    }
  }
}

