using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Misuka.Domain.DTO;

namespace Misuka.Web.Models
{
  public class HomeModel
  {
    public List<SliderDTO> Sliders { get; set; }
    public ContentMenuDTO ContentMenu { get; set; }
    public List<WebSiteLinkDTO> WebSiteLink { get; set; }
  }
}