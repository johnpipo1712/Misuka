using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Misuka.Domain.DTO;
using Misuka.Domain.SearchCriteria;
using Misuka.Domain.Security;
using Misuka.Domain.Utilities;
using Misuka.Infrastructure.Data;
using Misuka.Services.ReportServices.KeyCurrent;
using Misuka.Services.Services;

namespace Misuka.Services.ReportServices
{
  class KeyGenerationReportService : IKeyGenerationReportService
  {
    private readonly ICommandExecutor _executor;
    private readonly IUserSession _userSession;
   

    public KeyGenerationReportService(ICommandExecutor executor)
    {
      _executor = executor;
      _userSession = new UserSession();
    }

    public KeyGenerationDTO GetCode(string keyType)
    {
      return _executor.Execute(new GetCodeDbCommandService(keyType));
    }
  }
}

