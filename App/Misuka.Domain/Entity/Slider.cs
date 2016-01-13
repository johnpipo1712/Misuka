using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.Entity
{

  public class Slider : Misuka.Infrastructure.EntityFramework.Entity

  {
    public Guid SliderId { get; set; }

    public string ImageURL { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int? Type { get; set; }
  }

  public class SliderMap : EntityTypeConfiguration<Slider>
  {
    public SliderMap()
    {
      this.HasKey(t => t.SliderId);
      this.ToTable("[dbo].[Slider]");
    }
  }
}
