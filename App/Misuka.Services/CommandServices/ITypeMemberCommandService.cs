using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misuka.Services.CommandServices.TypeMembers;

namespace Misuka.Services.CommandServices
{
  public interface ITypeMemberCommandService
  {
    void DeleteTypeMember(DeleteTypeMemberCommand command);

    void EditTypeMember(EditTypeMemberCommand command);

    Guid AddTypeMember(AddTypeMemberCommand command);
  }
}
