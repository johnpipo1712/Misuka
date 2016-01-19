using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.ContentMenus
{
  public class EditContentMenuCommand
  {
    public Guid ContentMenuId { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public string MetaKeywork { get; set; }
    public string MetaDescription { get; set; }
    public string Description { get; set; }

    public EditContentMenuCommand(Guid contentMenuId, string title, string image, string metaKeywork, string metaDescription, string description)
    {
      // TODO: Complete member initialization
      ContentMenuId = contentMenuId;
      Title = title;
      Image = image;
      MetaKeywork = metaKeywork;
      MetaDescription = metaDescription;
      Description = description;
    }
  }
}
