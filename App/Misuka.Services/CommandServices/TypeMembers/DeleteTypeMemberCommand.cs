﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Services.CommandServices.TypeMembers
{
  public class DeleteTypeMemberCommand
  {
    public IList<Guid> SelectedIds { get; set; }

    public DeleteTypeMemberCommand(IList<Guid> selectedIds)
    {
      SelectedIds = selectedIds;
    }
  }
}
