using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Services.CommandServices.WebSiteLinks
{
  public class AddWebSiteLinkCommand
  {
    private string p1;
    private string p2;
    private string p3;

    public AddWebSiteLinkCommand(string p1, string p2, string p3)
    {
      // TODO: Complete member initialization
      this.p1 = p1;
      this.p2 = p2;
      this.p3 = p3;
    }
  }
}
