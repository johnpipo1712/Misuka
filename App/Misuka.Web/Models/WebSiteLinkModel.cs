using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Misuka.Web.Models
{
  public class WebSiteLinkModel
  {
    public Guid WebSiteLinkId { get; set; }

    public string Name { get; set; }

    public string Link { get; set; }

    public string ImageUrl { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public string CreatedByName { get; set; }
  }
}