using System;
using System.Data.Entity.ModelConfiguration;

namespace Misuka.Domain.Entity
{
  public class Person : Misuka.Infrastructure.EntityFramework.Entity
  {
    public Guid PersonId { get; set; }
    public string FullName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string SocialSecurityNo { get; set; }
    public string EmployeeNo { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string PhoneWork { get; set; }
    public string PhoneHome { get; set; }
    public string PhoneMobile { get; set; }
    public string Fax { get; set; }
    public string JobTitle { get; set; }
    public string POBox { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Comment { get; set; }
    public virtual User LogginUser { get; set; }
    public string ImageUrl { get; set; }
    public string ImageCoverUrl { get; set; }
    public DateTime? JoinedDate { get; set; }
    public DateTime? TerminatedDate { get; set; }
    public string SocialLaborNo { get; set; }
    public string TaxNo { get; set; }
    public string NickChat { get; set; }
    public DateTime? ContractDate { get; set; }
    public Double? RealAnnualLeave { get; set; }
    public Guid TypeMemberId { get; set; }
  }

  public class PersonMap : EntityTypeConfiguration<Person>
  {
    public PersonMap()
    {
      this.HasKey(t => t.PersonId);
      HasRequired(t => t.LogginUser).WithRequiredPrincipal(t => t.PersonInfo).WillCascadeOnDelete(true);
      this.ToTable("[dbo].[Person]");
    }
  }
}
