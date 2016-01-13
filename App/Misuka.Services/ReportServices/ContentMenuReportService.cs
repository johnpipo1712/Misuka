using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Misuka.Domain.DTO;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Data;
using Misuka.Services.Services;

namespace Misuka.Services.ReportServices
{
  class ContentMenuReportService : IContentMenuReportService
  {
    private readonly ICommandExecutor _executor;
    private readonly IContentMenuService _contentMenuService;
    private readonly IUserSession _userSession;

    public ContentMenuReportService(ICommandExecutor executor, IContentMenuService contentMenuService)
    {
      _executor = executor;
      _contentMenuService = contentMenuService;
      _userSession = new UserSession();
    }

  
    public ContentMenuDTO GetById(Guid contentMenuId)
    {
      var contentMenu = _contentMenuService.Find(contentMenuId);
      return Mapper.Map<Domain.Entity.ContentMenu, ContentMenuDTO>(contentMenu);
    }

    public List<ContentMenuDTO> GetAll()
    {
      var contentMenus = _contentMenuService.Queryable().ToList();
      return contentMenus.Select(Mapper.Map<Domain.Entity.ContentMenu, ContentMenuDTO>).ToList();
    
    }
  }
}

