using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Services.CommandServices.TypeMembers
{
  public class AddTypeMemberCommand
  {
    private string p;
    private long? nullable1;
    private long? nullable2;
    private double? nullable3;

    public AddTypeMemberCommand(string p, long? nullable1, long? nullable2, double? nullable3)
    {
      // TODO: Complete member initialization
      this.p = p;
      this.nullable1 = nullable1;
      this.nullable2 = nullable2;
      this.nullable3 = nullable3;
    }
  }
}
