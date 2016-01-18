using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.DTO;
using Misuka.Domain.SearchCriteria;
using Misuka.Domain.Utilities;

namespace Misuka.Services.ReportServices
{
  public interface IPersonReportService
  {
    PersonDTO GetById(Guid personId);
    List<PersonDTO> GetAll();

    SearchResult<PersonDTO> Search(PersonSearchCriteria searchCriteria, int pageSize, int pageIndex);
  }
}
