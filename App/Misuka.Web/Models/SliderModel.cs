using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Misuka.Web.Models
{
  public class SliderModel
  {
    public Guid SliderId { get; set; }

    public string ImageURL { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int? Type { get; set; }
  }
}