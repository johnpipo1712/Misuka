using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Misuka.Domain.DTO;

namespace Misuka.Web.Models
{
  public class PersonModel
  {
    [Display(Name = "PersonId", ResourceType = typeof(Resources.Person))]
    public Guid PersonId { get; set; }
    [Display(Name = "FullName", ResourceType = typeof(Resources.Person))]
    public string FullName { get; set; }
    public string SocialSecurityNo { get; set; }
    [Display(Name = "EmployeeNo", ResourceType = typeof(Resources.Person))]
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
    public DateTime? ContractDate { get; set; }
    public string Comment { get; set; }
    public string ImageUrl { get; set; }
    public string ImageCoverUrl { get; set; }
    public Double RealAnnualLeave { get; set; }
    public string FileCoverVirtualPath { get; set; }
    public DateTime? JoinedDate { get; set; }
    public DateTime? TerminatedDate { get; set; }
    public string SocialLaborNo { get; set; }
    public string TaxNo { get; set; }
    public string NickChat { get; set; }
    [Display(Name = "TypeMemberId", ResourceType = typeof(Resources.Person))]
    public Guid TypeMemberId { get; set; }
    public string TypeMemberName { get; set; }
  }
}