using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.Entity
{

  public class ContentMenu : Misuka.Infrastructure.EntityFramework.Entity
  {
    public Guid ContentMenuId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string MetaKeywork { get; set; }

    public string MetaDescription { get; set; }

    public string Image { get; set; }
  }
  public class ContentMenuMap : EntityTypeConfiguration<ContentMenu>
  {
    public ContentMenuMap()
    {
      this.HasKey(t => t.ContentMenuId);
      this.ToTable("[dbo].[ContentMenu]");
    }
  }
}
