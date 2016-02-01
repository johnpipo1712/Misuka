using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Misuka.Web.Models
{
  public class WebSiteLinkModel
  {
    [Display(Name = "WebSiteLinkId", ResourceType = typeof(Resources.WebSiteLink))]
    public Guid WebSiteLinkId { get; set; }

    [Display(Name = "Name", ResourceType = typeof(Resources.WebSiteLink))]
    public string Name { get; set; }

    [Display(Name = "Link", ResourceType = typeof(Resources.WebSiteLink))]
    public string Link { get; set; }

    [Display(Name = "ImageUrl", ResourceType = typeof(Resources.WebSiteLink))]
    public string ImageUrl { get; set; }

    [Display(Name = "CreatedDate", ResourceType = typeof(Resources.WebSiteLink))]
    public DateTime? CreatedDate { get; set; }

    [Display(Name = "CreatedBy", ResourceType = typeof(Resources.WebSiteLink))]
    public Guid? CreatedBy { get; set; }

    [Display(Name = "CreatedBy", ResourceType = typeof(Resources.WebSiteLink))]
    public string CreatedByName { get; set; }
  }
}