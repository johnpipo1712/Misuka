using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Services.CommandServices.TypeMembers
{
  public class AddTypeMemberCommand
  {
    public string Name { get; set; }
    public long ScoresFrom{ get; set; }
    public long ScoresTo{ get; set; }
    public Double PercentDownPayment { get; set; }
    public AddTypeMemberCommand(string name, long scoresFrom, long scoresTo, Double percentDownPayment)
    {
      // TODO: Complete member initialization
      Name = name;
      ScoresFrom = scoresFrom;
      ScoresTo = scoresTo;
      PercentDownPayment = percentDownPayment;
    }
  }
}
