using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Misuka.Web.Models
{
  public class SliderModel
  {
    [Display(Name = "SliderId", ResourceType = typeof(Resources.Slider))]
    public Guid SliderId { get; set; }

    [Display(Name = "ImageURL", ResourceType = typeof(Resources.Slider))]
    public string ImageURL { get; set; }

    [Display(Name = "Name", ResourceType = typeof(Resources.Slider))]
    public string Name { get; set; }

    [Display(Name = "Description", ResourceType = typeof(Resources.Slider))]
    public string Description { get; set; }

    [Display(Name = "Type", ResourceType = typeof(Resources.Slider))]
    public int? Type { get; set; }
  }
}