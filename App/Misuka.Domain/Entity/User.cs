using System;
using System.Data.Entity.ModelConfiguration;

namespace Misuka.Domain.Entity
{
  public class User : Misuka.Infrastructure.EntityFramework.Entity
  {
    public Guid PersonId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public bool Locked { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? LastLoginTime { get; set; }
    public int FailedLoginTimes { get; set; }
    public string Domain { get; set; }
    public string CurrentLanguage { get; set; }
    public int Type { get; set; }
    public virtual Person PersonInfo { get; set; }
  }


  public class UserMap : EntityTypeConfiguration<User>
  {
    public UserMap()
    {
      this.HasKey(t => t.PersonId);
      this.ToTable("[dbo].[LoginUser]");
    }
  }
}
