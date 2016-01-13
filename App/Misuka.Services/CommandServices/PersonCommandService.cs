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
  class PersonCommandService : IPersonCommandService
  {
    private readonly ICommandExecutor _executor;
    private readonly IPersonService _personService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSession _userSession;
    public PersonCommandService(IPersonService personService, IUnitOfWork unitOfWork, ICommandExecutor executor)
    {
      _personService = personService;
      _unitOfWork = unitOfWork;
      _executor = executor;
      _userSession = new UserSession();
    }
  }
}