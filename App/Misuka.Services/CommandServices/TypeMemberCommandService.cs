using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;
using Misuka.Services.CommandServices.TypeMembers;
using Misuka.Services.Services;

namespace Misuka.Services.CommandServices
{
  class TypeMemberCommandService : ITypeMemberCommandService
  {
    private readonly ICommandExecutor _executor;
    private readonly ITypeMemberService _typeMemberService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSession _userSession;
    public TypeMemberCommandService(ITypeMemberService typeMemberService, IUnitOfWork unitOfWork, ICommandExecutor executor)
    {
      _typeMemberService = typeMemberService;
      _unitOfWork = unitOfWork;
      _executor = executor;
      _userSession = new UserSession();
    }

    public void DeleteTypeMember(DeleteTypeMemberCommand command)
    {
      throw new NotImplementedException();
    }

    public void EditTypeMember(EditTypeMemberCommand command)
    {
      throw new NotImplementedException();
    }

    public Guid AddTypeMember(AddTypeMemberCommand command)
    {
      throw new NotImplementedException();
    }
  }
}