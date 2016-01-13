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
  class WebSiteLinkReportService: IWebSiteLinkReportService
  {
    private readonly ICommandExecutor _executor;
    private readonly IWebSiteLinkService _webSiteLinkService;
    private readonly IUserSession _userSession;

    public WebSiteLinkReportService(ICommandExecutor executor, IWebSiteLinkService webSiteLinkService)
    {
      _executor = executor;
      _webSiteLinkService = webSiteLinkService;
      _userSession = new UserSession();
    }

  
    public WebSiteLinkDTO GetById(Guid webSiteLinkId)
    {
      var webSiteLink = _webSiteLinkService.Find(webSiteLinkId);
      return Mapper.Map<Domain.Entity.WebSiteLink, WebSiteLinkDTO>(webSiteLink);
    }

    public List<WebSiteLinkDTO> GetAll()
    {
      var webSiteLinks = _webSiteLinkService.Queryable().ToList();
      return webSiteLinks.Select(Mapper.Map<Domain.Entity.WebSiteLink, WebSiteLinkDTO>).ToList();
    
    }
  }
}

