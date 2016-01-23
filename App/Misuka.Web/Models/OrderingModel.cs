using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Misuka.Web.Models
{
  public class OrderingModel
  {
    public Guid OrderingId { get; set; }

    public string OrderingCode { get; set; }

    public Guid? PersonId { get; set; }

    public Guid? ExchangeRateId { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public string Note { get; set; }

    public string NoteCustomer { get; set; }

    public string NoteApproved { get; set; }

    public decimal? TotalCustomFees { get; set; }

    public decimal? TotalDomesticCharges { get; set; }

    public decimal? TotalShipInternal { get; set; }

    public decimal? TotalShipAbroad { get; set; }

    public double? TotalVat { get; set; }

    public double? TotalWage { get; set; }

    public double? TotalDiscuss { get; set; }

    public decimal? TotalAmount { get; set; }

    public long? TotalCount { get; set; }

    public decimal? TotalPrice { get; set; }

    public long? TotalQuantity { get; set; }

    public decimal? TotalDownPayment { get; set; }

    public bool? IsDownPayment { get; set; }

    public bool? IsPaid { get; set; }

    public bool? IsPayAtHome { get; set; }

    public bool? IsDelivered { get; set; }

    public bool? IsDeposit { get; set; }

    public int? Type { get; set; }

    public Guid? CreatedBy { get; set; }

    public string CreatedByName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public DateTime? UsdOfDate { get; set; }

    public DateTime? VndOfDate { get; set; }

    public DateTime? CompleteDate { get; set; }

    public int Status { get; set; }

    public int StatusName { get; set; }
  }
}