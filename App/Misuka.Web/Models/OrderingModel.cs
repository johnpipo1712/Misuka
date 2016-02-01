using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Misuka.Web.Models
{
  public class OrderingModel
  {
    [Display(Name = "OrderingId", ResourceType = typeof(Resources.Order))]
    public Guid OrderingId { get; set; }

    [Display(Name = "OrderingId", ResourceType = typeof(Resources.Order))]
    public string OrderingCode { get; set; }

    [Display(Name = "OrderingId", ResourceType = typeof(Resources.Order))]
    public Guid? PersonId { get; set; }

    [Display(Name = "OrderingId", ResourceType = typeof(Resources.Order))]
    public Guid ExchangeRateId { get; set; }

    [Display(Name = "OrderingId", ResourceType = typeof(Resources.Order))]
    public string Phone { get; set; }

    [Display(Name = "OrderingId", ResourceType = typeof(Resources.Order))]
    public string Address { get; set; }

    [Display(Name = "OrderingId", ResourceType = typeof(Resources.Order))]
    public string Note { get; set; }

    [Display(Name = "OrderingId", ResourceType = typeof(Resources.Order))]
    public string NoteCustomer { get; set; }

    [Display(Name = "OrderingId", ResourceType = typeof(Resources.Order))]
    public string NoteApproved { get; set; }

    [Display(Name = "TotalCustomFees", ResourceType = typeof(Resources.Order))]
    public decimal? TotalCustomFees { get; set; }

    [Display(Name = "TotalDomesticCharges", ResourceType = typeof(Resources.Order))]
    public decimal? TotalDomesticCharges { get; set; }

    [Display(Name = "TotalShipInternal", ResourceType = typeof(Resources.Order))]
    public decimal? TotalShipInternal { get; set; }

    [Display(Name = "TotalShipAbroad", ResourceType = typeof(Resources.Order))]
    public decimal? TotalShipAbroad { get; set; }

    [Display(Name = "TotalVat", ResourceType = typeof(Resources.Order))]
    public double? TotalVat { get; set; }

    [Display(Name = "TotalWage", ResourceType = typeof(Resources.Order))]
    public double? TotalWage { get; set; }

    [Display(Name = "TotalDiscuss", ResourceType = typeof(Resources.Order))]
    public double? TotalDiscuss { get; set; }

    [Display(Name = "TotalAmount", ResourceType = typeof(Resources.Order))]
    public decimal? TotalAmount { get; set; }

    [Display(Name = "TotalCount", ResourceType = typeof(Resources.Order))]
    public long? TotalCount { get; set; }

    [Display(Name = "TotalPrice", ResourceType = typeof(Resources.Order))]
    public decimal? TotalPrice { get; set; }

    [Display(Name = "TotalQuantity", ResourceType = typeof(Resources.Order))]
    public long? TotalQuantity { get; set; }

    [Display(Name = "TotalDownPayment", ResourceType = typeof(Resources.Order))]
    public decimal? TotalDownPayment { get; set; }

    [Display(Name = "IsDownPayment", ResourceType = typeof(Resources.Order))]
    public bool? IsDownPayment { get; set; }

    [Display(Name = "IsPaid", ResourceType = typeof(Resources.Order))]
    public bool IsPaid { get; set; }

    [Display(Name = "IsPayAtHome", ResourceType = typeof(Resources.Order))]
    public bool? IsPayAtHome { get; set; }

    [Display(Name = "IsDelivered", ResourceType = typeof(Resources.Order))]
    public bool IsDelivered { get; set; }

    [Display(Name = "IsDeposit", ResourceType = typeof(Resources.Order))]
    public bool? IsDeposit { get; set; }

    [Display(Name = "Type", ResourceType = typeof(Resources.Order))]
    public int? Type { get; set; }

    [Display(Name = "CreatedBy", ResourceType = typeof(Resources.Order))]
    public Guid? CreatedBy { get; set; }

    [Display(Name = "CreatedBy", ResourceType = typeof(Resources.Order))]
    public string CreatedByName { get; set; }

    [Display(Name = "CreatedDate", ResourceType = typeof(Resources.Order))]
    public DateTime? CreatedDate { get; set; }

    [Display(Name = "ApprovedDate", ResourceType = typeof(Resources.Order))]
    public DateTime? ApprovedDate { get; set; }

    [Display(Name = "UsdOfDate", ResourceType = typeof(Resources.Order))]
    public DateTime? UsdOfDate { get; set; }

    [Display(Name = "VndOfDate", ResourceType = typeof(Resources.Order))]
    public DateTime? VndOfDate { get; set; }

    [Display(Name = "CompleteDate", ResourceType = typeof(Resources.Order))]
    public DateTime? CompleteDate { get; set; }

    [Display(Name = "TransportFee", ResourceType = typeof(Resources.Order))]
    public double? TransportFee { get; set; }

    [Display(Name = "WeightFee", ResourceType = typeof(Resources.Order))]
    public decimal? WeightFee { get; set; }

    [Display(Name = "ProductCode", ResourceType = typeof(Resources.OrderingDetail))]
    public string ProductCode { get; set; }

    [Display(Name = "Name", ResourceType = typeof(Resources.OrderingDetail))]
    public string Name { get; set; }

    [Display(Name = "Brand", ResourceType = typeof(Resources.OrderingDetail))]
    public string Brand { get; set; }

    [Display(Name = "Price", ResourceType = typeof(Resources.OrderingDetail))]
    public decimal? Price { get; set; }

    [Display(Name = "Quantity", ResourceType = typeof(Resources.OrderingDetail))]
    public long? Quantity { get; set; }

    [Display(Name = "Link", ResourceType = typeof(Resources.OrderingDetail))]
    public string Link { get; set; }

    [Display(Name = "LinkUrl", ResourceType = typeof(Resources.OrderingDetail))]
    public string LinkUrl { get; set; }

    [Display(Name = "Color", ResourceType = typeof(Resources.OrderingDetail))]
    public string Color { get; set; }

    [Display(Name = "Size", ResourceType = typeof(Resources.OrderingDetail))]
    public string Size { get; set; }

    [Display(Name = "Status", ResourceType = typeof(Resources.Order))]
    public int Status { get; set; }

    [Display(Name = "Status", ResourceType = typeof(Resources.Order))]
    public int StatusName { get; set; }

    [Display(Name = "PriceAmount", ResourceType = typeof(Resources.Order))]
    public decimal PriceAmount { get; set; }


    [Display(Name = "PriceDiscuss", ResourceType = typeof(Resources.Order))]
    public decimal PriceDiscuss { get; set; }

    [Display(Name = "PriceVat", ResourceType = typeof(Resources.Order))]
    public decimal PriceVat { get; set; }

    [Display(Name = "PriceTransportFee", ResourceType = typeof(Resources.Order))]
    public decimal PriceTransportFee { get; set; }

    [Display(Name = "PriceTotalWage", ResourceType = typeof(Resources.Order))]
    public decimal PriceTotalWage { get; set; }


    [Display(Name = "RemainingAmount", ResourceType = typeof(Resources.Order))]
    public decimal RemainingAmount { get; set; }

    [Display(Name = "Total", ResourceType = typeof(Resources.Order))]
    public decimal Total { get; set; }

 
  }
}