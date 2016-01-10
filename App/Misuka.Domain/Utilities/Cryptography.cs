using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using Misuka.Infrastructure.Configuration;

namespace Misuka.Domain.Utilities
{
  public class Cryptography
  {
    /// <summary>
    /// This function creates a md5 hash string from input string.
    /// This function can be used if you need to create a scenario where 
    /// you have to be sure the posted values comes from a trusted source.
    /// </summary>
    /// <param name="unencryptedString">String to hash.</param>
    /// <returns>Returns a md5 checksum of the input string.</returns>
    public static string CreateMD5Hash(string unencryptedString)
    {
      var UTF8Encoder = new UTF8Encoding();
      byte[] aryBytes = UTF8Encoder.GetBytes(unencryptedString);
      MD5 md5 = new MD5CryptoServiceProvider();
      byte[] bHashedData = md5.ComputeHash(aryBytes);
      return BitConverter.ToString(bHashedData).Replace("-", "");
    }

    /// <summary>
    /// Creates a SHAHashed version of the supplied string
    /// </summary>
    /// <param name="unencryptedString"></param>
    /// <returns></returns>
    public static string CreateSHAHash(string unencryptedString)
    {
      UTF8Encoding UTF8Encoder = new UTF8Encoding();
      byte[] aryBytes = UTF8Encoder.GetBytes(unencryptedString);
      SHA512Managed sha = new SHA512Managed();
      byte[] bHashedData = sha.ComputeHash(aryBytes);
      return BitConverter.ToString(bHashedData).Replace("-", "");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string GenerateSimpleTicket()
    {
      string randomValue = Guid.NewGuid().ToString();
      string response = randomValue + "&" + CreateSHAHash(string.Format("{0}{1}", randomValue, SystemConfiguration.Instance.SecuritySettings.EncryptionSalt));

      return response;
    }

    public static bool VerifySimpleTicket(string ticket)
    {
      char[] separator = new char[1];
      separator[0] = char.Parse("&");
      string[] verifyKey = ticket.Split(separator);
      if (verifyKey.Length != 2) return false;

      string response = CreateSHAHash(string.Format("{0}{1}", verifyKey[0], SystemConfiguration.Instance.SecuritySettings.EncryptionSalt));
      return verifyKey[1] == response;
    }

    /// <summary>
    /// This method will encrypt and unencrypted password with the current password settings for dashboard.
    /// </summary>
    /// <param name="unencryptedPassword"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    public static string EncryptPassword(string unencryptedPassword, string salt)
    {
      MembershipProvider provider = new SecurityUtility().GetMembershipProvider();
      string password;

      switch (provider.PasswordFormat)
      {
        case MembershipPasswordFormat.Clear:
          password = unencryptedPassword;
          break;
        case MembershipPasswordFormat.Encrypted:
          password = EncryptData(string.Format("{0}{1}", unencryptedPassword, salt));
          break;
        case MembershipPasswordFormat.Hashed:
          password = CreateSHAHash(string.Format("{0}{1}", unencryptedPassword, salt));
          break;
        default:
          password = unencryptedPassword;
          break;
      }

      return password;
    }

    #region Encryption
    /// <summary>
    /// Encrypt a input string using builtin dashboard password/salt/IV
    /// </summary>
    /// <param name="data">String to encrypt</param>
    /// <returns>Returns a base64 encoded string with the encrypted data</returns>
    public static string EncryptData(string data)
    {
      byte[] b = UTF8Encoding.UTF8.GetBytes(data);
      string password = SystemConfiguration.Instance.SecuritySettings.EncryptionKey;
      string salt = SystemConfiguration.Instance.SecuritySettings.EncryptionSalt;
      return EncryptData(b, password, salt);
    }

    /// <summary>
    /// Encrypt a input byte array using builtin dashboard password/salt/IV
    /// </summary>
    /// <param name="data">Byte array to encrypt</param>
    /// <returns>Returns a base64 encoded string with the encrypted data</returns>
    public static string EncryptData(byte[] data)
    {
      string password = SystemConfiguration.Instance.SecuritySettings.EncryptionKey;
      string salt = SystemConfiguration.Instance.SecuritySettings.EncryptionSalt;
      return EncryptData(data, password, salt);
    }

    /// <summary>
    /// Encrypt a input string using user suplied password and salt.
    /// </summary>
    /// <param name="data">String to encrypt.</param>
    /// <param name="passwordPhrase">Password to use when encrypting the data</param>
    /// <param name="salt">Salt</param>
    /// <returns>Returns a base64 encoded string</returns>
    public static string EncryptData(string data, string passwordPhrase, string salt)
    {
      byte[] b = UTF8Encoding.UTF8.GetBytes(data);
      return EncryptData(b, passwordPhrase, salt);
    }

    /// <summary>
    /// Encrypts a byte array using user supplied password and salt
    /// </summary>
    /// <param name="data">byte array to encrypt</param>
    /// <param name="passwordPhrase">Password to use when encrypting the data</param>
    /// <param name="salt">Salt</param>
    /// <returns>Returns a base64 encoded string</returns>
    public static string EncryptData(byte[] data, string passwordPhrase, string salt)
    {
      byte[] passwordBytes = UTF8Encoding.UTF8.GetBytes(passwordPhrase);
      byte[] saltBytes = UTF8Encoding.UTF8.GetBytes(salt);
      byte[] IVBytes = UTF8Encoding.UTF8.GetBytes(SystemConfiguration.Instance.SecuritySettings.EncryptionIV);

      PasswordDeriveBytes password = new PasswordDeriveBytes(passwordBytes, saltBytes, "SHA1", 1);
      byte[] keyBytes = password.GetBytes(32);	// 256 bits key

      RijndaelManaged aes = new RijndaelManaged();
      aes.Mode = CipherMode.CBC;


      ICryptoTransform crypto = aes.CreateEncryptor(keyBytes, IVBytes);

      MemoryStream memoryStream = new MemoryStream();
      CryptoStream cryptoStream = new CryptoStream(memoryStream, crypto, CryptoStreamMode.Write);
      cryptoStream.Write(data, 0, data.Length);
      cryptoStream.FlushFinalBlock();

      byte[] encryptedData = memoryStream.ToArray();
      memoryStream.Close();
      cryptoStream.Close();


      return Convert.ToBase64String(encryptedData);
    }

    #endregion


    /// <summary>
    /// Decrypts a string which originally was encrypted using the Encrypt(string data) or Encrypt(byte[] data) function
    /// </summary>
    /// <param name="data">Encrypted Base64 encoded string</param>
    /// <returns>Returns a decoded byte array</returns>
    public static byte[] DecryptData(string data)
    {
      string password = SystemConfiguration.Instance.SecuritySettings.EncryptionKey;
      string salt = SystemConfiguration.Instance.SecuritySettings.EncryptionSalt;

      //Use a vague error message here just in case it ever gets displayed to an end user
      if (password == null)
      {
        throw new ConfigurationErrorsException("The encryption key could not be read. Make sure that the Configuration framework has been initialized (In a web application this should happen in global.asax.cs) and that web.config has been configured correctly");
      }
      if (salt == null)
      {
        throw new ConfigurationErrorsException("The salt could not be read. Make sure that the Configuration framework has been initialized (In a web application this should happen in global.asax.cs) and that web.config has been configured correctly");
      }
      return DecryptData(data, password, salt);
    }

    /// <summary>
    /// Decrypts a string which originally was encrypted using Encrypt(string/byte[] data, string passPhrase) functions
    /// </summary>
    /// <param name="data">Encrypted Base64 encoded string</param>
    /// <param name="passwordPhrase">Password to use for decrypting</param>
    /// <returns>Returns a decoded byte array</returns>
    public static byte[] DecryptData(string data, string passwordPhrase)
    {
      string salt = SystemConfiguration.Instance.SecuritySettings.EncryptionSalt;
      //Use a vague error message here just in case it ever gets displayed to an end user
      if (salt == null)
      {
        throw new ConfigurationErrorsException("The salt could not be read. Make sure that the Configuration framework has been initialized (In a web application this should happen in global.asax.cs) and that web.config has been configured correctly");
      }
      return DecryptData(data, passwordPhrase, salt);
    }

    /// <summary>
    /// Decrypts a string which originally was encrypted using Encrypt(string/byte[] data, string passPhrase) functions
    /// </summary>
    /// <param name="data">Encrypted Base64 encoded string</param>
    /// <param name="passwordPhrase">Password to use for decrypting</param>
    /// <param name="salt">Salt used to originally code the string</param>
    /// <returns>Returns a decoded byte array</returns>
    public static byte[] DecryptData(string data, string passwordPhrase, string salt)
    {
      byte[] passwordBytes = UTF8Encoding.UTF8.GetBytes(passwordPhrase);
      byte[] saltBytes = UTF8Encoding.UTF8.GetBytes(salt);
      byte[] encryptedData = Convert.FromBase64String(data);

      if (SystemConfiguration.Instance.SecuritySettings.EncryptionIV == null)
      {
        //Use a vague error message here just in case it ever gets displayed to an end user
        throw new ConfigurationErrorsException("The EncryptionIV could not be read. Make sure that the Configuration framework has been initialized (In a web application this should happen in global.asax.cs) and that web.config has been configured correctly");
      }
      byte[] IVBytes = UTF8Encoding.UTF8.GetBytes(SystemConfiguration.Instance.SecuritySettings.EncryptionIV);

      PasswordDeriveBytes password = new PasswordDeriveBytes(passwordBytes, saltBytes, "SHA1", 1);
      byte[] keyBytes = password.GetBytes(32);	// 256 bits key


      RijndaelManaged aes = new RijndaelManaged();
      aes.Mode = CipherMode.CBC;

      ICryptoTransform decryptor = aes.CreateDecryptor(keyBytes, IVBytes);


      MemoryStream memoryStream = new MemoryStream();
      CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write);

      try
      {
        cryptoStream.Write(encryptedData, 0, encryptedData.Length);
      }
      catch (Exception ex)
      {
        throw new Exception("LogErrorEvent while writing encrypted data to the stream: \n" + ex.Message);
      }
      cryptoStream.FlushFinalBlock();
      cryptoStream.Close();

      return memoryStream.ToArray();

    }

    /// <summary>
    /// Decrypts a input string using Base64
    /// </summary>
    /// <param name="data">String to encrypt</param>
    /// <returns>Returns a base64 encoded string with the encrypted data</returns>
    public static string Base64Encode(string data)
    {
      byte[] encDataByte = UTF8Encoding.UTF8.GetBytes(data);
      return Convert.ToBase64String(encDataByte);
    }

    /// <summary>
    /// Encrypt a input string using Base64
    /// </summary>
    /// <param name="data"></param>
    /// <returns>Return a string with the decrypted data</returns>
    public static string Base64Decode(string data)
    {
      byte[] decodeByte = Convert.FromBase64String(data);
      return Encoding.UTF8.GetString(decodeByte);
    }
  }
}
