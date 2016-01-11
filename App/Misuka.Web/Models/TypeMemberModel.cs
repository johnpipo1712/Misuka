using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Misuka.Web.Models
{
  public class TypeMemberModel
  {
    public Guid TypeMemberId { get; set; }

    public string Name { get; set; }

    public long? ScoresTo { get; set; }

    public long? ScoresFrom { get; set; }

    public double? PercentDownPayment { get; set; }
  }
}