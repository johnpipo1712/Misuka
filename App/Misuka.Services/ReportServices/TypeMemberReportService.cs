using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Misuka.Domain.DTO;
using Misuka.Domain.SearchCriteria;
using Misuka.Domain.Security;
using Misuka.Domain.Utilities;
using Misuka.Infrastructure.Data;
using Misuka.Services.ReportServices.TypeMembers;
using Misuka.Services.Services;

namespace Misuka.Services.ReportServices
{
  class TypeMemberReportService: ITypeMemberReportService
  {
    private readonly ICommandExecutor _executor;
    private readonly ITypeMemberService _typeMemberService;
    private readonly IUserSession _userSession;

    public TypeMemberReportService(ICommandExecutor executor, ITypeMemberService typeMemberService)
    {
      _executor = executor;
      _typeMemberService = typeMemberService;
      _userSession = new UserSession();
    }

  
    public TypeMemberDTO GetById(Guid typeMemberId)
    {
      var typeMember = _typeMemberService.Find(typeMemberId);
      return Mapper.Map<Domain.Entity.TypeMember, TypeMemberDTO>(typeMember);
    }

    public List<TypeMemberDTO> GetAll()
    {
      var typeMembers = _typeMemberService.Queryable().ToList();
      return typeMembers.Select(Mapper.Map<Domain.Entity.TypeMember, TypeMemberDTO>).ToList();
    
    }

    public SearchResult<TypeMemberDTO> Search(TypeMemberSearchCriteria searchCriteria, int pageSize, int pageIndex)
    {
      return _executor.Execute(new GetTypeMemberDTOBySearchCriteriaDbCommand(searchCriteria, pageIndex, pageSize));
    }
  }
}


