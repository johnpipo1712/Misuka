using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.Sliders
{
  public class EditSliderCommand
  {
    public Guid SliderId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }

    public EditSliderCommand(Guid sliderId, string name, string description, string imageUrl)
    {
      // TODO: Complete member initialization
      SliderId = sliderId;
      Name = name;
      Description = description;
      ImageUrl = imageUrl;
    }
  }
}
