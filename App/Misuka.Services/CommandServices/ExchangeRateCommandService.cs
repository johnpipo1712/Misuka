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
  class ExchangeRateCommandService : IExchangeRateCommandService
  {
    private readonly ICommandExecutor _executor;
    private readonly IExchangeRateService _exchangeRateService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSession _userSession;
    public ExchangeRateCommandService(IExchangeRateService exchangeRateService, IUnitOfWork unitOfWork, ICommandExecutor executor)
    {
      _exchangeRateService = exchangeRateService;
      _unitOfWork = unitOfWork;
      _executor = executor;
      _userSession = new UserSession();
    }
  }
}