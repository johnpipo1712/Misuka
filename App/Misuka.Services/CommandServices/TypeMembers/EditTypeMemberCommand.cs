using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.TypeMembers
{
  public class EditTypeMemberCommand
  {
    private Guid guid;
    private string p;
    private long? nullable1;
    private long? nullable2;
    private double? nullable3;

    public EditTypeMemberCommand(Guid guid, string p, long? nullable1, long? nullable2, double? nullable3)
    {
      // TODO: Complete member initialization
      this.guid = guid;
      this.p = p;
      this.nullable1 = nullable1;
      this.nullable2 = nullable2;
      this.nullable3 = nullable3;
    }
  }
}
