using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Misuka.Web.Models
{
  public class ExchangeRateModel
  {
    public Guid ExchangeRateId { get; set; }

    public string Name { get; set; }

    public decimal? Price { get; set; }
  }
}