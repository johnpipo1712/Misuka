using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.Orderings
{
  public class EditOrderingFollowingOrderCommand
  {
    public EditOrderingFollowingOrderCommand(Guid orderingId, string noteApproved, decimal totalCustomFees, decimal totalDomesticCharges
       , decimal totalShipInternal, decimal totalShipAbroad,double totalVat, double totalWage)
    {
      OrderingId = orderingId;
      TotalCustomFees = totalCustomFees;
      TotalDomesticCharges = totalDomesticCharges;
      TotalShipAbroad = totalShipAbroad;
      TotalShipInternal = totalShipInternal;
      TotalVat = totalVat;
      TotalWage = totalWage;
      NoteApproved = noteApproved;
    }
    public Guid OrderingId { get; set; } 

    public string NoteApproved { get; set; }

    public decimal TotalCustomFees { get; set; }

    public decimal TotalDomesticCharges { get; set; }

    public decimal TotalShipInternal { get; set; }

    public decimal TotalShipAbroad { get; set; }

    public double TotalVat { get; set; }

    public double TotalWage { get; set; }

 
  }
}
