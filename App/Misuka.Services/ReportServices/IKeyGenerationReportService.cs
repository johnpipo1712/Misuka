using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Misuka.Domain.DTO;

namespace Misuka.Services.ReportServices
{
  public interface IKeyGenerationReportService
  {
    KeyGenerationDTO GetCode(string keyType);
  }
}
