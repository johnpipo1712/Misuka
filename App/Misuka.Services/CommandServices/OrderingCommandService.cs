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
  class OrderingCommandService : IOrderingCommandService
  {
    private readonly ICommandExecutor _executor;
    private readonly IOrderingService _groupMemberService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSession _userSession;
    public OrderingCommandService(IOrderingService groupMemberService, IUnitOfWork unitOfWork, ICommandExecutor executor)
    {
      _groupMemberService = groupMemberService;
      _unitOfWork = unitOfWork;
      _executor = executor;
      _userSession = new UserSession();
    }
  }
}