using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Misuka.Web.Models
{
  public class ContentMenuModel
  {
    [Display(Name = "ContentMenuId", ResourceType = typeof(Resources.ContentMenu))]
    public Guid ContentMenuId { get; set; }

    [Display(Name = "Title", ResourceType = typeof(Resources.ContentMenu))]
    public string Title { get; set; }

    [Display(Name = "Description", ResourceType = typeof(Resources.ContentMenu))]
    public string Description { get; set; }

    [Display(Name = "MetaKeywork", ResourceType = typeof(Resources.ContentMenu))]
    public string MetaKeywork { get; set; }

    [Display(Name = "MetaDescription", ResourceType = typeof(Resources.ContentMenu))]
    public string MetaDescription { get; set; }

    [Display(Name = "Image", ResourceType = typeof(Resources.ContentMenu))]
    public string Image { get; set; }
  }
}