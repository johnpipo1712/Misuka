using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Misuka.Services.CommandServices.Persons;

namespace Misuka.Services.CommandServices
{
  public interface IPersonCommandService
  {
    void ChangePassword(ChangePasswordCommand command);
    void EditInfomationPerson(EditInfomationPersonCommand command);
    void ForgotPassword(ForgotPasswordCommand command);
    void Register(RegisterCommand command);
    void ResetPassword(ResetPasswordCommand command);
  }
}
