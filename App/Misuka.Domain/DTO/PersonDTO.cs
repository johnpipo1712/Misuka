using System;
using System.Collections.Generic;
using Misuka.Domain.Entity;
using Misuka.Domain.Enum;
using Misuka.Infrastructure.Configuration;
using Misuka.Infrastructure.Utilities;

namespace Misuka.Domain.DTO
{
  public class PersonDTO
  {
    public Guid PersonId { get; set; }
    public string FullName { get; set; }
  
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
    public DateTime? ContractDate { get; set; }
    public string Comment { get; set; }
    public UserDTO LoginUser { get; set; }
    public string ImageUrl { get; set; }
    public string ImageCoverUrl { get; set; }
    public Double RealAnnualLeave { get; set; }
    public string FileCoverVirtualPath
    {
      get
      {
        if (!String.IsNullOrEmpty(ImageCoverUrl))
        {
          return string.Format("{0}/{1}", SystemConfiguration.Instance.GeneralSettings.UploadDocumentFolder, ImageCoverUrl);
        }
        else
        {
          return "/Content/img/default_cover.jpg";
        }

      }
    }
    public string FileVirtualPath
    {
      get
      {
        if (!String.IsNullOrEmpty(ImageUrl))
        {
          return string.Format("{0}/{1}", SystemConfiguration.Instance.GeneralSettings.UploadDocumentFolder, ImageUrl);
        }
        else
        {
          return "/Content/img/none-img.png";
        }
        
      }
    }
    public DateTime? JoinedDate { get; set; }
    public DateTime? TerminatedDate { get; set; }
    public string SocialLaborNo { get; set; }
    public string TaxNo { get; set; }
    public string NickChat { get; set; }
    public Guid TypeMemberId { get; set; }
    public string TypeMemberName { get; set; }
  }
}
