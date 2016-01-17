using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.DTO;
using Misuka.Domain.SearchCriteria;
using Misuka.Domain.Utilities;

namespace Misuka.Services.ReportServices
{
  public interface ISliderReportService
  {
    SliderDTO GetById(Guid sliderId);
    List<SliderDTO> GetAll();

    SearchResult<SliderDTO> Search(SliderSearchCriteria searchCriteria, int pageSize, int pageIndex);
  }
}
