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
  class OrderingDetailCommandService : IOrderingDetailCommandService
  {
    private readonly ICommandExecutor _executor;
    private readonly IOrderingDetailService _orderingDetailService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSession _userSession;
    public OrderingDetailCommandService(IOrderingDetailService orderingDetailService, IUnitOfWork unitOfWork, ICommandExecutor executor)
    {
      _orderingDetailService = orderingDetailService;
      _unitOfWork = unitOfWork;
      _executor = executor;
      _userSession = new UserSession();
    }
  }
}