using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace Misuka.Infrastructure.Smtp
{
    [Serializable]
    internal class LinkedResourceWrapper
    {

        String _contentId;
        Uri _contentLink;
        Stream _contentStream;
        ContentTypeWrapper _contentType;
        TransferEncoding _transferEncoding;

        internal static LinkedResourceWrapper GetLinkedResourceWrapper(LinkedResource lr)
        {
            if (lr == null)
                return null;

            LinkedResourceWrapper slr = new LinkedResourceWrapper();
            slr._contentId = lr.ContentId;
            slr._contentLink = lr.ContentLink;

            if (lr.ContentStream != null)
            {
                byte[] bytes = new byte[lr.ContentStream.Length];
                lr.ContentStream.Read(bytes, 0, bytes.Length);
                slr._contentStream = new MemoryStream(bytes);
            }

            slr._contentType = ContentTypeWrapper.GetSerializeableContentType(lr.ContentType);
            slr._transferEncoding = lr.TransferEncoding;
            return slr;
        }

        internal LinkedResource GetLinkedResource()
        {
            LinkedResource slr = new LinkedResource(_contentStream);
            slr.ContentId = _contentId;
            slr.ContentLink = _contentLink;

            slr.ContentType = _contentType.GetContentType();
            slr.TransferEncoding = _transferEncoding;

            return slr;
        }
    }
}