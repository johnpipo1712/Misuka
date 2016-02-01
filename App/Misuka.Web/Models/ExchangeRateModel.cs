using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Misuka.Web.Models
{
  public class ExchangeRateModel
  {
    [Display(Name = "ExchangeRateId", ResourceType = typeof(Resources.ExchangeRate))]
    public Guid ExchangeRateId { get; set; }

    [Display(Name = "Name", ResourceType = typeof(Resources.ExchangeRate))]
    public string Name { get; set; }

    [Display(Name = "Price", ResourceType = typeof(Resources.ExchangeRate))]
    public decimal Price { get; set; }
  }
}