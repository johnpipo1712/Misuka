using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misuka.Infrastructure.Extensions;

namespace Misuka.Domain.Enum
{
  public enum StatusOrderingEnum
  {
    [LocalizedDescription(DescriptionResourceKey = "New", DescriptionResourceType = typeof(Resources.StatusOrderingEnum))]
    New = 1,
    [LocalizedDescription(DescriptionResourceKey = "InProcess", DescriptionResourceType = typeof(Resources.StatusOrderingEnum))]
    InProcess = 2,
    [LocalizedDescription(DescriptionResourceKey = "ComeUsd", DescriptionResourceType = typeof(Resources.StatusOrderingEnum))]
    ComeUsd = 3,
    [LocalizedDescription(DescriptionResourceKey = "ComeVn", DescriptionResourceType = typeof(Resources.StatusOrderingEnum))]
    ComeVn = 4,
    [LocalizedDescription(DescriptionResourceKey = "Done", DescriptionResourceType = typeof(Resources.StatusOrderingEnum))]
    Done = 5,
    [LocalizedDescription(DescriptionResourceKey = "Reject", DescriptionResourceType = typeof(Resources.StatusOrderingEnum))]
    Reject = 6
  }
}
