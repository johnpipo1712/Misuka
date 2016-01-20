using System;
using System.Configuration;
using System.Linq;
using System.Web.Security;
using Misuka.Domain.Context;
using Misuka.Domain.Entity;
using Misuka.Domain.Security;
using Misuka.Domain.Utilities;
using Misuka.Infrastructure.EntityFramework.Factories;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;
using Misuka.Infrastructure.Utilities;

namespace Misuka.Domain.Providers
{
  public class CustomMembershipProvider : MembershipProvider
  {
    private SecurityUtility _securityUtility;
    private static SecurityUtility _securityUtilityStatic;
    private readonly int _maxInvalidPasswordAttempts;
    private readonly string _providerName;
    private readonly bool _requiresUniqueEmail;
    private string _applicationName;
    private readonly bool _enablePasswordReset;
    private readonly bool _enablePasswordRetrieval;
    private readonly bool _requiresQuestionAndAnswer;
    private readonly MembershipPasswordFormat _passwordFormat;
    private readonly int _minRequiredPasswordLength;
    private readonly int _minRequiredNonAlphanumericCharacters;
    private readonly int _passwordAttemptWindow;
    private readonly string _passwordStrengthRegularExpression;

    public CustomMembershipProvider()
    {
      _requiresUniqueEmail = true;
      _applicationName = "/";
      _enablePasswordReset = false;
      _enablePasswordRetrieval = false;
      _maxInvalidPasswordAttempts = 0;
      _requiresQuestionAndAnswer = true;
      _minRequiredNonAlphanumericCharacters = 0;
      _minRequiredPasswordLength = 6;
      _passwordAttemptWindow = 5;
      _passwordFormat = MembershipPasswordFormat.Hashed;
      _passwordStrengthRegularExpression = "";

      ProviderSettings providerSettings = SecurityUtility.GetMembershipProviderSettings();
      _providerName = providerSettings.Name;
      foreach (string index in providerSettings.Parameters.AllKeys)
      {
        switch (index.ToLower())
        {
          case "applicationname":
            _applicationName = providerSettings.Parameters[index];
            break;

          case "enablepasswordreset":
            _enablePasswordReset = ValueUtilities.GetBoolean(providerSettings.Parameters[index], false);
            break;

          case "enablepasswordretrieval":
            _enablePasswordRetrieval = ValueUtilities.GetBoolean(providerSettings.Parameters[index], false);
            break;

          case "maxinvalidpasswordattempts":
            _maxInvalidPasswordAttempts = ValueUtilities.GetInt32(providerSettings.Parameters[index], 5);
            break;

          case "minrequirednonalphanumericcharacters":
            _minRequiredNonAlphanumericCharacters = ValueUtilities.GetInt32(providerSettings.Parameters[index], 3);
            break;

          case "minrequiredpasswordlength":
            _minRequiredPasswordLength = ValueUtilities.GetInt32(providerSettings.Parameters[index], 8);
            break;

          case "passwordattemptwindow":
            _passwordAttemptWindow = ValueUtilities.GetInt32(providerSettings.Parameters[index], 5);
            break;

          case "passwordformat":
            _passwordFormat = ValueUtilities.GetEnumerationValue<MembershipPasswordFormat>(providerSettings.Parameters[index]);
            break;

          case "passwordstrengthregularexpression":
            _passwordStrengthRegularExpression = providerSettings.Parameters[index] ?? string.Empty;
            break;

          case "requiresquestionandanswer":
            _requiresQuestionAndAnswer = ValueUtilities.GetBoolean(providerSettings.Parameters[index], true);
            break;

          case "requiresuniqueemail":
            _requiresUniqueEmail = ValueUtilities.GetBoolean(providerSettings.Parameters[index], true);
            break;
        }
      }
    }

    #region Properties

    private string ProviderName
    {
      get { return _providerName; }
    }
    public override bool EnablePasswordReset
    {
      get { return _enablePasswordReset; }
    }

    public override bool EnablePasswordRetrieval
    {
      get { return _enablePasswordRetrieval; }
    }

    public override string ApplicationName
    {
      get
      {
        return _applicationName;
      }
      set
      {
        _applicationName = value;
      }
    }

    public override int MaxInvalidPasswordAttempts
    {
      get { return _maxInvalidPasswordAttempts; }
    }

    public override int MinRequiredNonAlphanumericCharacters
    {
      get { return _minRequiredNonAlphanumericCharacters; }
    }

    public override int MinRequiredPasswordLength
    {
      get { return _minRequiredPasswordLength; }
    }

    public override int PasswordAttemptWindow
    {
      get { return _passwordAttemptWindow; }
    }

    public override MembershipPasswordFormat PasswordFormat
    {
      get { return _passwordFormat; }
    }

    public override string PasswordStrengthRegularExpression
    {
      get { return _passwordStrengthRegularExpression; }
    }

    public override bool RequiresQuestionAndAnswer
    {
      get { return _requiresQuestionAndAnswer; }
    }

    public override bool RequiresUniqueEmail
    {
      get { return _requiresUniqueEmail; }
    }
    #endregion

    #region Methods
    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
      oldPassword = oldPassword.Trim();
      if (!SecurityUtility.IsPasswordValid(newPassword))
      {
        return false;
      }
      var user = SecurityUtility.GetUserByUsername(username);
      if (user == null)
      {
        return false;
      }
      var feedbackMessage = new System.Text.StringBuilder();
      if (user.Password != oldPassword && user.Password != Cryptography.EncryptPassword(oldPassword, user.Salt))
      {
        return false;
      }

      user.Password = Cryptography.EncryptPassword(newPassword, user.Salt);
      IRepositoryProvider _repositoryProvider = new RepositoryProvider(new RepositoryFactories());
      var unitofWork = new UnitOfWork(new MisukaDBContext(), _repositoryProvider);

      unitofWork.Repository<Domain.Entity.User>().Update(user);
      var ret = unitofWork.SaveChanges();
      return ret > 0;
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
      throw new NotImplementedException();
    }

    //public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
    //{
    //  throw new NotImplementedException();
    //}

    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
    {
      #region Check valid username
      if (!SecurityUtility.IsUsernameValid(username))
      {
        status = MembershipCreateStatus.InvalidUserName;
        return null;
      }
      #endregion

      #region Check for valid PersonId

      string _username = username;
      string _domain = string.Empty;

      try
      {
        if (SecurityUtility.UsernameContainsDomain(username))
        {
          _username = SecurityUtility.ExtractUsername(username);
          _domain = SecurityUtility.ExtractDomain(username);
        }
      }
      catch
      {
        // Username was flagged as having a domain attached, but the extraction failed for unknown reason
        status = MembershipCreateStatus.InvalidUserName;
        return null;
      }

      Guid personId;
      bool locked = false;
      try
      {
        if (providerUserKey != null && providerUserKey.ToString().Length >= 32)
        {
          personId = new Guid(providerUserKey.ToString().Substring(0, 32));
          locked = ValueUtilities.GetBoolean(providerUserKey.ToString().Substring(32), false);
        }
        else if (providerUserKey == null || providerUserKey.ToString().Length == 0)
        {
          personId = Guid.Empty;
        }
        else
        {
          status = MembershipCreateStatus.InvalidProviderUserKey;
          return null;
        }
      }
      catch
      {
        status = MembershipCreateStatus.InvalidProviderUserKey;
        return null;
      }

      #endregion

      #region Test for valid email
      //if ((RequiresUniqueEmail || EnablePasswordRetrieval || EnablePasswordReset) && !password.Equals(SOCIAL_LOGIN_DEFAULT_PASSWORD))
      //{

      //  if (email == null || EmailUtilities.ValidateEmailAddress(email) == false)
      //  {
      //    status = MembershipCreateStatus.InvalidEmail;
      //    return null;
      //  }
      //}
      #endregion

      #region Test for valid password


      if (!SecurityUtility.IsPasswordValid(password))
      {
        status = MembershipCreateStatus.InvalidPassword;
        return null;
      }


      #endregion

      IRepositoryProvider _repositoryProvider = new RepositoryProvider(new RepositoryFactories());
      var unitofWork = new UnitOfWork(new MisukaDBContext(), _repositoryProvider);

      #region Check for unique username
      Domain.Entity.User user = unitofWork.Repository<User>().Query(u => String.Compare(u.UserName, username, StringComparison.InvariantCultureIgnoreCase) == 0).Select().FirstOrDefault();
      if (user != null)
      {
        status = MembershipCreateStatus.DuplicateUserName;
        return null;
      }
      #endregion

      #region Test for valid question/answer
      if (RequiresQuestionAndAnswer)
      {
        if (passwordQuestion == null || passwordQuestion.Length > 200 || passwordQuestion.Length < 1)
        {
          status = MembershipCreateStatus.InvalidQuestion;
          return null;
        }

        if (passwordAnswer == null || passwordAnswer.Length > 200 || passwordAnswer.Length < 1)
        {
          status = MembershipCreateStatus.InvalidAnswer;
          return null;
        }
      }

      #endregion

      DateTime dt = DateTime.Now;
      user = new User
      {
        UserName = _username,
        CreationDate = dt,
        Domain = _domain,
        PersonId = personId,
        Locked = locked,
        FailedLoginTimes = 0,
        CurrentLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.ToString()
      };


      user.Password = Cryptography.EncryptPassword(password, user.Salt);
      try
      {
        unitofWork.RepositoryAsync<Domain.Entity.User>().Insert(user);
        unitofWork.SaveChanges();
      }
      catch
      {
        status = MembershipCreateStatus.UserRejected;
        //  Log.Debug(this, string.Format("Create new user: {0} - failed", identity.Username));
        return null;
      }

      status = MembershipCreateStatus.Success;
      //Log.Debug(this, string.Format("Create new user: {0} - successfully", identity.Username));
      return new MembershipUser(_providerName, username, providerUserKey, email, passwordQuestion, "", isApproved, false, dt, dt, dt, dt, DateTime.MinValue);

    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
      throw new NotImplementedException();
    }

    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotImplementedException();
    }

    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotImplementedException();
    }

    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotImplementedException();
    }

    public override int GetNumberOfUsersOnline()
    {
      throw new NotImplementedException();
    }

    public override string GetPassword(string username, string answer)
    {
      throw new NotImplementedException();
    }

    public override MembershipUser GetUser(string username, bool userIsOnline)
    {
      var user = SecurityUtility.GetUserByUsername(username);
      if (user != null)
      {
        var memUser = new MembershipUser(_providerName, username, user.PersonId, string.Empty,
                                                    string.Empty, string.Empty,
                                                    true, false, DateTime.MinValue,
                                                    DateTime.MinValue,
                                                    DateTime.MinValue,
                                                    DateTime.Now, DateTime.Now);
        return memUser;
      }

      return null;
    }

    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
      throw new NotImplementedException();
    }

    public override string GetUserNameByEmail(string email)
    {
      throw new NotImplementedException();
    }
    #endregion

    public override string ResetPassword(string username, string answer)
    {
      throw new NotImplementedException();
    }

    public override bool UnlockUser(string userName)
    {
      throw new NotImplementedException();
    }

    public override void UpdateUser(MembershipUser user)
    {
      throw new NotImplementedException();
    }

    protected ISecurityUtility SecurityUtility
    {
      get
      {
        return _securityUtility ?? (_securityUtility = new SecurityUtility());
      }
    }
    static protected ISecurityUtility SecurityUtilityStatic
    {
      get
      {
        return _securityUtilityStatic ?? (_securityUtilityStatic = new SecurityUtility());
      }
    }
    public static  bool ValidateUser(string username, string password,int type)
    {
      password = password.Trim();
      var user = SecurityUtilityStatic.GetUserByUsername(username, type);
      if (user != null)
      {
        ////  if (!user.Active || user.AccountLocked)
        //if (user.Locked)
        //  return false;

        if (SecurityUtilityStatic.IsPasswordEqual(password, user.Password, user.Salt))
        {
          //Stored valid logged user to session
          new UserSession(user);
          user.LastLoginTime = DateTime.Now;
          user.FailedLoginTimes = 0;
          SecurityUtilityStatic.UpdateUserInformation(user);
          return true;
        }

        user.LastLoginTime = DateTime.Now;
        user.FailedLoginTimes++;


        SecurityUtilityStatic.UpdateUserInformation(user);
      }

      return false;
    }
    public override bool ValidateUser(string username, string password)
    {
      password = password.Trim();
      var user = SecurityUtility.GetUserByUsername(username);
      if (user != null)
      {
        ////  if (!user.Active || user.AccountLocked)
        //if (user.Locked)
        //  return false;

        if (SecurityUtility.IsPasswordEqual(password, user.Password, user.Salt))
        {
          //Stored valid logged user to session
          new UserSession(user);
          user.LastLoginTime = DateTime.Now;
          user.FailedLoginTimes = 0;
          SecurityUtility.UpdateUserInformation(user);
          return true;
        }

        user.LastLoginTime = DateTime.Now;
        user.FailedLoginTimes++;

       
        SecurityUtility.UpdateUserInformation(user);
      }

      return false;
    }
  }
}
