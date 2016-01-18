using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.DTO;
using Misuka.Domain.SearchCriteria;
using Misuka.Domain.Utilities;

namespace Misuka.Services.ReportServices
{
  public interface ITypeMemberReportService
  {
    TypeMemberDTO GetById(Guid typeMemberId);
    List<TypeMemberDTO> GetAll();

    SearchResult<TypeMemberDTO> Search(TypeMemberSearchCriteria searchCriteria, int pageSize, int pageIndex);
  }
}
