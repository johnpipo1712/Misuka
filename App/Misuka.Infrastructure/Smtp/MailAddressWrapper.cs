using System;
using System.Net.Mail;

namespace Misuka.Infrastructure.Smtp
{
    [Serializable]
    internal class MailAddressWrapper
    {
        String _address;
        String _displayName;

        internal static MailAddressWrapper GetSerializeableMailAddress(MailAddress ma)
        {
            if (ma == null)
                return null;
            MailAddressWrapper sma = new MailAddressWrapper();
            sma._address = ma.Address;
            sma._displayName = ma.DisplayName;
            return sma;
        }

        internal MailAddress GetMailAddress()
        {
            return new MailAddress(_address, _displayName);
        }
    }
}