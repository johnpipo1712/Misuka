using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.DTO
{
  public class TypeMemberDTO
  {
    public Guid TypeMemberId { get; set; }

    public string Name { get; set; }

    public long? ScoresTo { get; set; }

    public long? ScoresFrom { get; set; }

    public double? PercentDownPayment { get; set; }
  }
}
