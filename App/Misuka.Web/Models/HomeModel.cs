using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Misuka.Domain.DTO;

namespace Misuka.Web.Models
{
  public class HomeModel
  {
    List<SliderDTO> Sliders { get; set; }
    ContentMenuDTO ContentMenu { get; set; }
    List<WebSiteLinkDTO> WebSiteLink { get; set; }
  }
}