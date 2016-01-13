using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;
using Misuka.Services.Services;

namespace Misuka.Services.CommandServices
{
  class ContentMenuCommandService : IContentMenuCommandService
  {
    private readonly ICommandExecutor _executor;
    private readonly IContentMenuService _contentMenuService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSession _userSession;
    public ContentMenuCommandService(IContentMenuService contentMenuService, IUnitOfWork unitOfWork, ICommandExecutor executor)
    {
      _contentMenuService = contentMenuService;
      _unitOfWork = unitOfWork;
      _executor = executor;
      _userSession = new UserSession();
    }
  }
}