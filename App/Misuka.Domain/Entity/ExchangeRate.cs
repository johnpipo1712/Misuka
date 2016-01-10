using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.Entity
{
  public class ExchangeRate : Misuka.Infrastructure.EntityFramework.Entity
  {
    public Guid ExchangeRateId { get; set; }

    public string Name { get; set; }

    public decimal? Price { get; set; }
  }
  public class ExchangeRateMap : EntityTypeConfiguration<ExchangeRate>
  {
    public ExchangeRateMap()
    {
      this.HasKey(t => t.ExchangeRateId);
      this.ToTable("[dbo].[ExchangeRate]");
    }
  }
}
