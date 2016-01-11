using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Misuka.Web.Models
{
  public class ContentMenuModel
  {
    public Guid ContentMenuId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string MetaKeywork { get; set; }

    public string MetaDescription { get; set; }

    public string Image { get; set; }
  }
}