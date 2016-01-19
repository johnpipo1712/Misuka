using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.TypeMembers
{
  public class EditTypeMemberCommand
  {
    public Guid TypeMemberId { get; set; }
    public string Name { get; set; }
    public long ScoresFrom { get; set; }
    public long ScoresTo { get; set; }
    public Double PercentDownPayment { get; set; }

    public EditTypeMemberCommand(Guid typeMemberId, string name, long scoresFrom, long scoresTo, Double percentDownPayment)
    {
      // TODO: Complete member initialization
      TypeMemberId = typeMemberId;
      Name = name;
      ScoresFrom = scoresFrom;
      ScoresTo = scoresTo;
      PercentDownPayment = percentDownPayment;
    }
  }
}
