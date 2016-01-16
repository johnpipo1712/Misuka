using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices
{
  public interface ISliderCommandService
  {
    void EditSlider(Sliders.EditSliderCommand updateCommand);

    object AddSlider(Sliders.AddSliderCommand createCommand);

    void DeleteSlider(Sliders.DeleteSliderCommand deleteSliderCommand);
  }
}
