using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Misuka.Infrastructure.Utilities
{
  public class EmailUtilities
  {
    private const string EMAIL_VALIDATION = @"[a-zA-Z0-9+_-]+(?:\.[a-zA-Z0-9+_-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?";
    /// <summary>
    /// Expression used to validate a single email address
    /// </summary>
    public static readonly string SingleEmailValidation = string.Format(@"^{0}$", EMAIL_VALIDATION);
    /// <summary>
    /// Expression used to validate a list of email address separated by ',' or ';'
    /// </summary>
    public static readonly string EmailListValidation = string.Format(@"^\s*{0}\s*([,;]\s*{0}\s*[,;]?\s*)*$", EMAIL_VALIDATION);

    private static readonly Regex SingleEmailValidationRegex = new Regex(SingleEmailValidation, RegexOptions.IgnoreCase);
    private static readonly Regex EmailListValidationRegex = new Regex(EmailListValidation, RegexOptions.IgnoreCase);

    /// <summary>
    /// Validate single email address
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool ValidateEmailAddress(string email)
    {
      if (string.IsNullOrEmpty(email)) return false;
      return SingleEmailValidationRegex.IsMatch(email);
    }

    /// <summary>
    /// Validate list of email addresses, separated by comma or semi-colon
    /// </summary>
    /// <param name="emailAddresses"></param>
    /// <returns></returns>
    public static bool ValidateEmailAddresses(string emailAddresses)
    {
      if (string.IsNullOrEmpty(emailAddresses)) return false;
      return EmailListValidationRegex.IsMatch(emailAddresses);
    }


  
  }
}