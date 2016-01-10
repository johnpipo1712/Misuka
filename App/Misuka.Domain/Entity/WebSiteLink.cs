using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.Entity
{

    public class WebSiteLink : Misuka.Infrastructure.EntityFramework.Entity
    {
        public Guid WebSiteLinkId { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public string CreatedByName { get; set; }
    }
    public class WebSiteLinkMap : EntityTypeConfiguration<WebSiteLink>
    {
      public WebSiteLinkMap()
      {
        this.HasKey(t => t.WebSiteLinkId);
        this.ToTable("[dbo].[WebSiteLink]");
      }
    }
}
