namespace Misuka.Domain.Utilities
{
  public static class PhoneUtility
  {
    public static string GetStandardVNPhoneNumber(string inputPhoneNumber)
    {
      if (string.IsNullOrEmpty(inputPhoneNumber))
        return string.Empty;

      if (inputPhoneNumber.StartsWith("84"))
        return string.Format("0{0}", inputPhoneNumber.Remove(0, 2));

      if (inputPhoneNumber.StartsWith("+84"))
        return string.Format("0{0}", inputPhoneNumber.Remove(0, 3));

      return inputPhoneNumber;
    }
  }
}
