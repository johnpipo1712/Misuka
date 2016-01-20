using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Services.CommandServices.Persons
{
  public class RegisterCommand
  {
    public RegisterCommand(string fullName, string password, string userName, string email,int type)
    {
      Email = email;
      FullName = fullName;
      Password = password;
      UserName = userName;
      Type = type;
    }

    public string FullName { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public int Type { get; set; }
  }
}
