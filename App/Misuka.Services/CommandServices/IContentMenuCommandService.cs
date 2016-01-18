using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices
{
  public interface IContentMenuCommandService
  {
    void DeleteContentMenu(ContentMenus.DeleteContentMenuCommand command);

    Guid AddContentMenu(ContentMenus.AddContentMenuCommand command);

    void EditContentMenu(ContentMenus.EditContentMenuCommand command);
  }
}
