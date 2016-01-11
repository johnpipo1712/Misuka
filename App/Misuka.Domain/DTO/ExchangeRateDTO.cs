using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.DTO
{
  public class ExchangeRateDTO
  {
    public Guid ExchangeRateId { get; set; }

    public string Name { get; set; }

    public decimal? Price { get; set; }
  }
}
