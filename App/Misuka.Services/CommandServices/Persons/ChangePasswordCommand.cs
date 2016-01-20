using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.Persons
{
  public class ChangePasswordCommand
  {
    public ChangePasswordCommand(string passwordOld, string passwordNew, Guid personId)
    {
      PasswordOld = passwordOld;
      PasswordNew = passwordNew;
      PersonId = personId;
    }
    public string PasswordOld { get; set; }
    public string PasswordNew { get; set; }
    public Guid PersonId { get; set; }
  }
}
