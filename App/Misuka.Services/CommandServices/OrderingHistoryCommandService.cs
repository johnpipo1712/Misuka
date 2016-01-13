using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;
using Misuka.Services.Services;

namespace Misuka.Services.CommandServices
{
  class OrderingHistoryCommandService : IOrderingHistoryCommandService
  {
    private readonly ICommandExecutor _executor;
    private readonly IOrderingHistoryService _orderingHistoryService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSession _userSession;
    public OrderingHistoryCommandService(IOrderingHistoryService orderingHistoryService, IUnitOfWork unitOfWork, ICommandExecutor executor)
    {
      _orderingHistoryService = orderingHistoryService;
      _unitOfWork = unitOfWork;
      _executor = executor;
      _userSession = new UserSession();
    }
  }
}