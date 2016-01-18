using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.WebSiteLinks
{
  public class EditWebSiteLinkCommand
  {
    private Guid guid;
    private string p1;
    private string p2;
    private string p3;

    public EditWebSiteLinkCommand(Guid guid, string p1, string p2, string p3)
    {
      // TODO: Complete member initialization
      this.guid = guid;
      this.p1 = p1;
      this.p2 = p2;
      this.p3 = p3;
    }
  }
}
