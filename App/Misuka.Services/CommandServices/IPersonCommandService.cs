using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Services.CommandServices.Persons;

namespace Misuka.Services.CommandServices
{
  public interface IPersonCommandService
  {
    Guid AddPerson(AddPersonCommand command);

    void DeletePerson(DeletePersonCommand command);
  }
}
