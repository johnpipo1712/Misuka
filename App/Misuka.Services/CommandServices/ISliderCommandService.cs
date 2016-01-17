using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Services.CommandServices.Sliders;

namespace Misuka.Services.CommandServices
{
  public interface ISliderCommandService
  {
    void EditSlider(EditSliderCommand command);

    Guid AddSlider(AddSliderCommand command);

    void DeleteSlider(DeleteSliderCommand command);
  }
}
