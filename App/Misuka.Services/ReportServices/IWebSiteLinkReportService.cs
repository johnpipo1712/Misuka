using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.DTO;
using Misuka.Domain.SearchCriteria;
using Misuka.Domain.Utilities;

namespace Misuka.Services.ReportServices
{
  public interface IWebSiteLinkReportService
  {
    WebSiteLinkDTO GetById(Guid webSiteLinkId);
    List<WebSiteLinkDTO> GetAll();

    SearchResult<WebSiteLinkDTO> Search(WebSiteLinkSearchCriteria searchCriteria, int pageSize, int pageIndex);
  }
}
