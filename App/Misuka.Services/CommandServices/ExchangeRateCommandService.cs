using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.Entity;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;
using Misuka.Services.CommandServices.ExchangeRates;
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

    public void DeleteExchangeRate(DeleteExchangeRateCommand command)
    {
      foreach (var item in command.SelectedIds)
      {
        _exchangeRateService.Delete(item);
        _unitOfWork.SaveChanges();
      }
    }

    public Guid AddExchangeRate(AddExchangeRateCommand command)
    {
      var exchangeRate = new ExchangeRate()
      {
        ExchangeRateId = Guid.NewGuid(),
        Price = command.Price,
        Name = command.Name
      };
      _exchangeRateService.Insert(exchangeRate);
      _unitOfWork.SaveChanges();
      return exchangeRate.ExchangeRateId;
    }

    public void EditExchangeRate(EditExchangeRateCommand command)
    {
      var exchangeRate = _exchangeRateService.Find(command.ExchangeRateId);
      exchangeRate.Name = command.Name;
      exchangeRate.Price = command.Price;
      _exchangeRateService.Update(exchangeRate);
      _unitOfWork.SaveChanges();
    }
  }
}