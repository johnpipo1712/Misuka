using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misuka.Domain.Entity;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;
using Misuka.Services.CommandServices.ContentMenus;
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

    public void DeleteContentMenu(DeleteContentMenuCommand command)
    {
      foreach (var item in command.SelectedIds)
      {
        _contentMenuService.Delete(item);
        _unitOfWork.SaveChanges();
      }
    }

    public Guid AddContentMenu(AddContentMenuCommand command)
    {
      var contentMenu = new ContentMenu()
      {
        ContentMenuId = Guid.NewGuid(),
        Description = command.Description,
        Image = command.Image,
        Title = command.Title,
        MetaDescription = command.MetaDescription,
        MetaKeywork = command.MetaKeywork
      };
      _contentMenuService.Insert(contentMenu);
      _unitOfWork.SaveChanges();
      return contentMenu.ContentMenuId;
    }

    public void EditContentMenu(EditContentMenuCommand command)
    {
      var contentMenu = _contentMenuService.Find(command.ContentMenuId);
      contentMenu.Description = command.Description;
      contentMenu.Image = command.Image;
      contentMenu.MetaDescription = command.MetaDescription;
      contentMenu.MetaKeywork = command.MetaKeywork;
      contentMenu.Title = command.Title;
      _contentMenuService.Update(contentMenu);
      _unitOfWork.SaveChanges();
    }
  }
}