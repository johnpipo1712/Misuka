using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Web.Security;
using Misuka.Domain;
using Misuka.Domain.Entity;
using Misuka.Domain.Enum;
using Misuka.Domain.Security;
using Misuka.Domain.Utilities;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.EntityFramework.Repositories;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;
using Misuka.Infrastructure.Utilities;
using Misuka.Services.CommandServices.Persons;
using Misuka.Services.Resources;
using Misuka.Services.Services;

namespace Misuka.Services.CommandServices
{
  class PersonCommandService : IPersonCommandService
  {
    private readonly ICommandExecutor _executor;
    private readonly IPersonService _personService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSession _userSession;
    public PersonCommandService(IPersonService personService, IUnitOfWork unitOfWork, ICommandExecutor executor)
    {
       _personService = personService;
      _unitOfWork = unitOfWork;
      _executor = executor;
      _userSession = new UserSession();
    }


    public void ChangePassword(ChangePasswordCommand command)
    {
      ThrowError.Against<ArgumentException>(string.IsNullOrEmpty(command.PasswordOld), "Vui lòng nhập mật khẩu cũ.");
      ThrowError.Against<ArgumentException>(string.IsNullOrEmpty(command.PasswordNew), "Vui lòng nhập mật khẩu mới.");
   
      var user =
       _unitOfWork.Repository<Domain.Entity.User>()
         .Query(u => u.PersonId == command.PersonId && u.Password == Cryptography.EncryptPassword(command.PasswordOld,""))
         .Select()
         .FirstOrDefault();
      ThrowError.Against<ApplicationException>(user == null, string.Format("Mật khẩu cũ không đúng."));
      user.Password = Cryptography.EncryptPassword(command.PasswordNew, "");
      user.Locked = false;
      _unitOfWork.Repository<Domain.Entity.User>().Update(user);
      _unitOfWork.SaveChanges();
    }

    public void EditInfomationPerson(EditInfomationPersonCommand command)
    {
      var person = _personService.Find(command.PersonId);
      person.FullName = command.FullName;
      _personService.Update(person);
      _unitOfWork.SaveChanges();
    }

    public void ForgotPassword(ForgotPasswordCommand command)
    {
     
    }

    public void Register(RegisterCommand command)
    {

      ISecurityUtility securityUtility = new SecurityUtility();
      ThrowError.Against<ArgumentException>(string.IsNullOrEmpty(command.UserName), String.Format(ErrorMessage.IsRequired, "Tên đăng nhập"));
      ThrowError.Against<ArgumentException>(string.IsNullOrEmpty(command.Password), String.Format(ErrorMessage.IsRequired, "Mật khẩu"));
      var user = securityUtility.GetUserByUsername(command.UserName);
      ThrowError.Against<ArgumentException>(user != null, String.Format(ErrorMessage.Exists, "Tên đăng nhập"));
      ThrowError.Against<ArgumentException>(_personService.Query(t=>t.Email == command.Email).Select().Any(), String.Format(ErrorMessage.Exists, "Email"));
    
      // ThrowError.Against<ArgumentException>(!securityUtility.IsPasswordValid(command.Password), String.Format(ErrorMessage.IsPassword));

      var person = new Person()
      {
        Email = command.Email,
        FullName = command.FullName,
        PersonId = Guid.NewGuid()
      };
      user = new User()
      {
        Type = command.Type,
        UserName = command.UserName,
        CreationDate = DateTime.Now,
        Locked = false,
        PersonId = person.PersonId,
        Password = Cryptography.EncryptPassword(command.Password, "")
      };

      _unitOfWork.Repository<Domain.Entity.User>().Insert(user);
      _personService.Insert(person);
      _unitOfWork.SaveChanges();
    }

    public void ResetPassword(ResetPasswordCommand command)
    {
      ThrowError.Against<ArgumentException>(string.IsNullOrEmpty(command.PasswordNew), "Vui lòng nhập mật khẩu mới.");

      var user =
        _unitOfWork.Repository<Domain.Entity.User>()
          .Query(u => u.PersonId == command.PersonId)
          .Select()
          .FirstOrDefault();

      user.Password = Cryptography.EncryptPassword(command.PasswordNew, "");
      user.Locked = false;
      _unitOfWork.Repository<Domain.Entity.User>().Update(user);
      _unitOfWork.SaveChanges();
    }
  }
}