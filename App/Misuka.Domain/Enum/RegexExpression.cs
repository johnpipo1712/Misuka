using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misuka.Domain.Enum
{
  public struct RegexExpressions
  {
    private const string DOMAIN_VALIDATION = @"([a-zA-Z]+([-.]?[a-zA-Z0-9]+)*)";
    private const string USERNAME_VALIDATION = @"([a-zA-Z0-9_-]+\.?[a-zA-Z0-9_-]+){1,100}";
    private const string EMAIL_VALIDATION = @"[a-zA-Z0-9+_-]+(?:\.[a-zA-Z0-9+_-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?";

    /// <summary>
    /// Expression used to validate a single email address
    /// </summary>
    public static readonly string SingleEmailValidation = string.Format(@"^{0}$", EMAIL_VALIDATION);

    /// <summary>
    /// Expression used to validate a list of email address separated by ',' or ';'
    /// </summary>
    public static readonly string EmailListValidation = string.Format(@"^\s*{0}\s*([,;]\s*{0}\s*[,;]?\s*)*$", EMAIL_VALIDATION);

    /// <summary>
    /// Expression used to validate an empty string
    /// </summary>
    public const string EmptyString = @"^\s*$";

    /// <summary>
    /// White spaces expression
    /// </summary>
    public const string WhiteSpaces = @"\s*";

    /// <summary>
    /// Expression to validate domain for this system. Not allow special character excepts: - .
    /// Includes: 
    ///          upheads
    ///          upheads-hcm.01
    ///          mail@upheads.no
    ///          upheads\mail@upheads.no
    ///          upheads-hcm-01\mail
    ///          upheads-hcm-01\mail@upheads.no
    ///          upheads.desktop\mail
    ///          upheads.desktop\mail@upheads.no
    /// </summary>
    public static readonly string DomainValidation = string.Format(@"^{0}$", DOMAIN_VALIDATION);

    /// <summary>
    /// Expression to validate username for this system. Not allow special character excepts: + - . and email
    /// Includes: 
    ///          upheads
    ///          upheads-hcm.01
    ///          mail@upheads.no
    /// </summary>
    public static readonly string UsernameValidation = string.Format(@"^{0}$", USERNAME_VALIDATION);
  }
}