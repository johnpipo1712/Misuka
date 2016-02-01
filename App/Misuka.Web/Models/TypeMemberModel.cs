using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Misuka.Web.Models
{
  public class TypeMemberModel
  {
    [Display(Name = "TypeMemberId", ResourceType = typeof(Resources.TypeMember))]
    public Guid TypeMemberId { get; set; }

    [Display(Name = "Name", ResourceType = typeof(Resources.TypeMember))]
    public string Name { get; set; }

    [Display(Name = "ScoresTo", ResourceType = typeof(Resources.TypeMember))]
    public long ScoresTo { get; set; }

    [Display(Name = "ScoresFrom", ResourceType = typeof(Resources.TypeMember))]
    public long ScoresFrom { get; set; }

    [Display(Name = "PercentDownPayment", ResourceType = typeof(Resources.TypeMember))]
    public double PercentDownPayment { get; set; }
  }
}