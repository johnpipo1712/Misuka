using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.Persons
{
  public class ForgotPasswordCommand
  {
    public ForgotPasswordCommand(string email)
    {
      Email = email;
    }
    public string Email { get; set; }
  }
}
