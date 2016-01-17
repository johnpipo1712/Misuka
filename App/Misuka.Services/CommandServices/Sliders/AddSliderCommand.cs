using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Services.CommandServices.Sliders
{
  public class AddSliderCommand
  {
    public string Name {get; set;}
    public string Description {get; set;}
    public string ImageURL {get; set;}

    public AddSliderCommand(string name, string description, string imageURL)
    {
      // TODO: Complete member initialization
      Name = name;
      Description = description;
      ImageURL = imageURL;
    }
  }
}
