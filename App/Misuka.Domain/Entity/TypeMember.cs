using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.Entity
{
    public class TypeMember : Misuka.Infrastructure.EntityFramework.Entity
    {
        public Guid TypeMemberId { get; set; }

        public string Name { get; set; }

        public long? ScoresTo { get; set; }

        public long? ScoresFrom { get; set; }

        public double? PercentDownPayment { get; set; }
    }
    public class TypeMemberMap : EntityTypeConfiguration<TypeMember>
    {
      public TypeMemberMap()
      {
        this.HasKey(t => t.TypeMemberId);
        this.ToTable("[dbo].[TypeMember]");
      }
    }
}
