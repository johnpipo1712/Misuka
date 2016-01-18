using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;
using Misuka.Services.CommandServices.WebSiteLinks;
using Misuka.Services.Services;

namespace Misuka.Services.CommandServices
{
  class WebSiteLinkCommandService : IWebSiteLinkCommandService
  {
    private readonly ICommandExecutor _executor;
    private readonly IWebSiteLinkService _webSiteLinkService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSession _userSession;
    public WebSiteLinkCommandService(IWebSiteLinkService webSiteLinkService, IUnitOfWork unitOfWork, ICommandExecutor executor)
    {
      _webSiteLinkService = webSiteLinkService;
      _unitOfWork = unitOfWork;
      _executor = executor;
      _userSession = new UserSession();
    }

    public void DeleteWebSiteLink(DeleteWebSiteLinkCommand command)
    {
      throw new NotImplementedException();
    }

    public Guid AddWebSiteLink(AddWebSiteLinkCommand command)
    {
      throw new NotImplementedException();
    }

    public void EditWebSiteLink(EditWebSiteLinkCommand command)
    {
      throw new NotImplementedException();
    }
  }
}