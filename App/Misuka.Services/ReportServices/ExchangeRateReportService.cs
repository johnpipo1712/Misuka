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
  class ExchangeRateReportService: IExchangeRateReportService
  {
    private readonly ICommandExecutor _executor;
    private readonly IExchangeRateService _exchangeRateService;
    private readonly IUserSession _userSession;

    public ExchangeRateReportService(ICommandExecutor executor, IExchangeRateService exchangeRateService)
    {
      _executor = executor;
      _exchangeRateService = exchangeRateService;
      _userSession = new UserSession();
    }

  
    public ExchangeRateDTO GetById(Guid exchangeRateId)
    {
      var exchangeRate = _exchangeRateService.Find(exchangeRateId);
      return Mapper.Map<Domain.Entity.ExchangeRate, ExchangeRateDTO>(exchangeRate);
    }

    public List<ExchangeRateDTO> GetAll()
    {
      var exchangeRates = _exchangeRateService.Queryable().ToList();
      return exchangeRates.Select(Mapper.Map<Domain.Entity.ExchangeRate, ExchangeRateDTO>).ToList();
    
    }
  }
}

