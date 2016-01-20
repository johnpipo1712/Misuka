using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.Persons
{
  public class EditInfomationPersonCommand
  {
    public string FullName { get; set; }

    public EditInfomationPersonCommand(string fullName, Guid personId)
    {
      FullName = fullName;
      PersonId = personId;
    }

    public Guid PersonId { get; set; }
  }
}
