using System;
using System.Collections.Generic;
using System.Linq;
namespace Misuka.Domain.DTO
{
  public class RolePersonDTO
  {
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public Guid DepartmentId { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
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
    public int Gender { get; set; }

    public string FullName
    {
      get { return string.Format("{0}, {1}", FirstName, LastName); }
    }
  }
}
