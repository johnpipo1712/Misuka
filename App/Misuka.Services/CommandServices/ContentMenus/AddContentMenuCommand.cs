using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Services.CommandServices.ContentMenus
{
  public class AddContentMenuCommand
  {
    public string Title { get; set; }
    public string Image { get; set; }
    public string MetaKeywork { get; set; }
    public string MetaDescription { get; set; }
    public string Description { get; set; }
    public AddContentMenuCommand(string title, string image, string metaKeywork, string metaDescription, string description)
    {
      // TODO: Complete member initialization
      Title = title;
      Image = image;
      MetaKeywork = metaKeywork;
      MetaDescription = metaDescription;
      Description = description;
    }
  
  }

 
}
