using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.DTO;

namespace Misuka.Services.ReportServices
{
  public interface ISliderReportService
  {
    SliderDTO GetById(Guid sliderId);
    List<SliderDTO> GetAll();
  }
}
