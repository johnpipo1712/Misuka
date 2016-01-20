using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.Persons
{
  public class ResetPasswordCommand
  {
    public ResetPasswordCommand(Guid personId,string passwordNew)
    {
      PasswordNew = passwordNew;
      PersonId = personId;
    }
    public string PasswordNew { get; set; }


    public Guid PersonId { get; set; }
  }
}
