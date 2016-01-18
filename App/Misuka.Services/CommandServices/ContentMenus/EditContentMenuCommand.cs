using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misuka.Services.CommandServices.ContentMenus
{
  public class EditContentMenuCommand
  {
    private Guid guid;
    private string p1;
    private string p2;
    private string p3;
    private string p4;
    private string p5;

    public EditContentMenuCommand(Guid guid, string p1, string p2, string p3, string p4, string p5)
    {
      // TODO: Complete member initialization
      this.guid = guid;
      this.p1 = p1;
      this.p2 = p2;
      this.p3 = p3;
      this.p4 = p4;
      this.p5 = p5;
    }
  }
}
