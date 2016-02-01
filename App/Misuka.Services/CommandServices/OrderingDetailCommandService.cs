using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.Entity;
using Misuka.Domain.Enum;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;
using Misuka.Services.CommandServices.OrderingDetails;
using Misuka.Services.ReportServices;
using Misuka.Services.Services;

namespace Misuka.Services.CommandServices
{
  class OrderingDetailCommandService : IOrderingDetailCommandService
  {
    private readonly ICommandExecutor _executor;
    private readonly IOrderingDetailService _orderingDetailService;
    private readonly IKeyGenerationReportService _keyGenerationReportService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSession _userSession;
    public OrderingDetailCommandService(IOrderingDetailService orderingDetailService, IKeyGenerationReportService keyGenerationReportService, IUnitOfWork unitOfWork, ICommandExecutor executor)
    {
      _orderingDetailService = orderingDetailService;
      _keyGenerationReportService = keyGenerationReportService;
      _unitOfWork = unitOfWork;
      _executor = executor;
      _userSession = new UserSession();
    }

    public Guid AddOrderingDetail(AddOrderingDetailCommand command)
    {
      string orderingDetailCode = _keyGenerationReportService.GetCode(KeyTypeObjects.OrderDetail).CodeNew;
      var orderingDetail = new OrderingDetail()
      {
        Brand = command.Brand,
        Color = command.Color,
        Link = command.Link,
        LinkUrl = command.LinkUrl,
        Name = command.Name,
        Note = command.Note,
        OrderingDetailId = Guid.NewGuid(),
        Price = command.Price,
        ProductCode = command.ProductCode,
        Quantity = command.Quantity,
        Size = command.Size,
        OrderingDetailCode = orderingDetailCode,
        OrderingId = command.OrderingId
      };
      _orderingDetailService.Insert(orderingDetail);
      _unitOfWork.SaveChanges();
      return orderingDetail.OrderingDetailId;
    }
  }
}