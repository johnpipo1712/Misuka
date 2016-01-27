using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misuka.Domain.Enum;
using Misuka.Infrastructure.Utilities;

namespace Misuka.Domain.DTO
{
  public class OrderingDTO
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

    public double? TransportFee { get; set; }

    public decimal? WeightFee { get; set; }

    public string ProductCode { get; set; }

    public string Name { get; set; }

    public string Brand { get; set; }

    public decimal? Price { get; set; }

    public long? Quantity { get; set; }

    public string Link { get; set; }

    public string LinkUrl { get; set; }

    public string Color { get; set; }

    public string Size { get; set; }

    public decimal PriceAmount
    {
      get
      {
        if (Price == null)
          Price = 0;
        if (Quantity == null)
          Quantity = 0;
        return (decimal)(Price * Quantity);
      }
    }

    public decimal PriceDiscuss
    {
      get
      {
        if (Price == null)
          Price = 0;
        if (Quantity == null)
          Quantity = 0;
        if (TotalDiscuss == null)
          TotalDiscuss = 0;
        return PriceAmount * (decimal)TotalDiscuss / 100;
      }
    }
    public decimal PriceVat
    {
      get
      {
        if (Price == null)
          Price = 0;
        if (Quantity == null)
          Quantity = 0;
        if (TotalVat == null)
          TotalVat = 0;
        return PriceAmount * (decimal)TotalVat / 100;
      }
    }
    public decimal PriceTransportFee
    {
      get
      {
        if (Price == null)
          Price = 0;
        if (Quantity == null)
          Quantity = 0;
        if (TransportFee == null)
          TransportFee = 0;
        return PriceAmount*(decimal)TransportFee/100;
      }
    }
    public decimal PriceTotalWage
    {
      get
      {
        if (Price == null)
          Price = 0;
        if (Quantity == null)
          Quantity = 0;
        if (TotalWage == null)
          TotalWage = 0;
        return PriceAmount *(decimal) TotalWage/100;
      }
    }

    public decimal RemainingAmount
    {
      get
      {
        if (TotalDownPayment == null)
          TotalDownPayment = 0;
        return Total - (decimal)TotalDownPayment;
      }
    }
    public decimal Total
    {
      get
      {

       
        if (TotalShipAbroad == null)
          TotalShipAbroad = 0;
        if (TotalShipInternal == null)
          TotalShipInternal = 0;
        if (TotalDomesticCharges == null)
          TotalDomesticCharges = 0;
        if (TotalCustomFees == null)
          TotalCustomFees = 0;
        if (TotalDownPayment == null)
          TotalDownPayment = 0;
        if (WeightFee == null)
          WeightFee = 0;
        var total =  PriceAmount
                    + PriceVat
                    + PriceTotalWage
                    + PriceTransportFee
                    + (decimal) TotalCustomFees
                    + (decimal) TotalShipInternal
                    + (decimal) TotalShipAbroad
                    + (decimal) TotalDomesticCharges
                    + (decimal) TotalCustomFees
                    + (decimal) WeightFee
                    - PriceDiscuss
                    - TotalDownPayment;
        return (decimal)total;
      }
    }
    public string StatusName
    {
      get
      {
        return EnumUtilities.GetTextOfSelectedItem<StatusOrderingEnum>(Status);
      }
    }
  }
}
