using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Domain.Entity;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;
using Misuka.Infrastructure.Utilities;
using Misuka.Services.CommandServices.TypeMembers;
using Misuka.Services.Resources;
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
      foreach (var item in command.SelectedIds)
      {
        _typeMemberService.Delete(item);
        _unitOfWork.SaveChanges();
      }
    }

    public void EditTypeMember(EditTypeMemberCommand command)
    {
      ThrowError.Against<ArgumentException>(string.IsNullOrEmpty(command.Name), String.Format(ErrorMessage.IsRequired, "Tên"));
      ThrowError.Against<ArgumentException>(_typeMemberService.Query(t => t.Name.ToUpper().Trim() == command.Name.ToUpper().Trim() && t.TypeMemberId != command.TypeMemberId).Select().Any(), String.Format(ErrorMessage.Exists, "Tên"));
   
      var count = _typeMemberService.Query(t => t.TypeMemberId != command.TypeMemberId && ((t.ScoresFrom < command.ScoresFrom && command.ScoresFrom < t.ScoresTo) || (t.ScoresFrom < command.ScoresTo && command.ScoresTo < t.ScoresTo) || (command.ScoresFrom < t.ScoresFrom && t.ScoresFrom < command.ScoresTo) || (command.ScoresFrom < t.ScoresTo && t.ScoresTo < command.ScoresTo) || (command.ScoresFrom == t.ScoresFrom && command.ScoresTo == t.ScoresTo))).Select().Count();
      ThrowError.Against<ArgumentException>(count > 0, String.Format(ErrorMessage.IsNotTypemMember));
  
      var typeMember = _typeMemberService.Find(command.TypeMemberId);
      typeMember.Name = command.Name;
      typeMember.PercentDownPayment = command.PercentDownPayment;
      typeMember.ScoresFrom = command.ScoresFrom;
      typeMember.ScoresTo = command.ScoresTo;
      _typeMemberService.Update(typeMember);
      _unitOfWork.SaveChanges();
    }

    public Guid AddTypeMember(AddTypeMemberCommand command)
    {
      ThrowError.Against<ArgumentException>(string.IsNullOrEmpty(command.Name), String.Format(ErrorMessage.IsRequired,"Tên"));
      ThrowError.Against<ArgumentException>(_typeMemberService.Query(t => t.Name.ToUpper().Trim() == command.Name.ToUpper().Trim()).Select().Any(), String.Format(ErrorMessage.Exists, "Tên"));
   
      var count = _typeMemberService.Query(t => ((t.ScoresFrom < command.ScoresFrom && command.ScoresFrom < t.ScoresTo) || (t.ScoresFrom < command.ScoresTo && command.ScoresTo < t.ScoresTo) || (command.ScoresFrom < t.ScoresFrom && t.ScoresFrom < command.ScoresTo) || (command.ScoresFrom < t.ScoresTo && t.ScoresTo < command.ScoresTo) || (command.ScoresFrom == t.ScoresFrom && command.ScoresTo == t.ScoresTo))).Select().Count();
      ThrowError.Against<ArgumentException>(count > 0, String.Format(ErrorMessage.IsNotTypemMember));
     
      var typeMember = new TypeMember()
      {
        Name = command.Name,
        PercentDownPayment = command.PercentDownPayment,
        ScoresFrom = command.ScoresFrom,
        ScoresTo = command.ScoresTo,
        TypeMemberId = Guid.NewGuid()
      };
      _typeMemberService.Insert(typeMember);
      _unitOfWork.SaveChanges();
      return typeMember.TypeMemberId;
    }
  }
}