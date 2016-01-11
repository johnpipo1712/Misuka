using System;

namespace Misuka.Domain.DTO
{
  public class UserDTO
  {
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool Active { get; set; }
    public bool Locked { get; set; }
    public string Email { get; set; }
    public string Salt { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public int FailedLoginTimes { get; set; }
    public string CurrentLanguage { get; set; }
    public int Type { get; set; }
    public string TypeName { get; set; }
  }
}
