using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.DTO
{
  public class SliderDTO
  {
    public Guid SliderId { get; set; }

    public string ImageURL { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int? Type { get; set; }
  }
}
