using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.Entity;
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
      foreach (var item in command.SelectedIds)
      {
        _webSiteLinkService.Delete(item);
        _unitOfWork.SaveChanges();
      }
    }

    public Guid AddWebSiteLink(AddWebSiteLinkCommand command)
    {
      var webSiteLink = new WebSiteLink()
      {
        CreatedBy = _userSession.UserId,
        CreatedByName = _userSession.FullName,
        CreatedDate = DateTime.Now,
        ImageUrl = command.ImageUrl,
        Link = command.Link,
        Name = command.Name,
        WebSiteLinkId = Guid.NewGuid()
      };
      _webSiteLinkService.Insert(webSiteLink);
      _unitOfWork.SaveChanges();
      return webSiteLink.WebSiteLinkId;
    }

    public void EditWebSiteLink(EditWebSiteLinkCommand command)
    {
      var webSiteLink = _webSiteLinkService.Find(command.WebSiteLinkId);
      webSiteLink.Name = command.Name;
      webSiteLink.Link = command.Link;
      webSiteLink.ImageUrl = command.ImageUrl;
      _webSiteLinkService.Update(webSiteLink);
      _unitOfWork.SaveChanges();
    }
  }
}