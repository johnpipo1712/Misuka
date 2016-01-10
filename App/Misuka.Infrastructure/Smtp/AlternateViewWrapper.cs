using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace Misuka.Infrastructure.Smtp
{
    [Serializable]
    internal class AlternateViewWrapper
    {
        Uri _baseUri;
        String _contentId;
        Stream _contentStream;
        ContentTypeWrapper _contentType;
        readonly IList<LinkedResourceWrapper> _linkedResources = new List<LinkedResourceWrapper>();
        TransferEncoding _transferEncoding;

        internal static AlternateViewWrapper GetSerializeableAlternateView(AlternateView av)
        {
            if (av == null)
                return null;

            AlternateViewWrapper sav = new AlternateViewWrapper();

            sav._baseUri = av.BaseUri;
            sav._contentId = av.ContentId;

            if (av.ContentStream != null)
            {
                byte[] bytes = new byte[av.ContentStream.Length];
                av.ContentStream.Read(bytes, 0, bytes.Length);
                sav._contentStream = new MemoryStream(bytes);
            }   

            sav._contentType = ContentTypeWrapper.GetSerializeableContentType(av.ContentType);

            foreach (LinkedResource lr in av.LinkedResources)
                sav._linkedResources.Add(LinkedResourceWrapper.GetLinkedResourceWrapper(lr));

            sav._transferEncoding = av.TransferEncoding;
            return sav;
        }

        internal AlternateView GetAlternateView()
        {

            AlternateView sav = new AlternateView(_contentStream);

            sav.BaseUri = _baseUri;
            sav.ContentId = _contentId;

            sav.ContentType = _contentType.GetContentType();

            foreach (LinkedResourceWrapper lr in _linkedResources)
                sav.LinkedResources.Add(lr.GetLinkedResource());

            sav.TransferEncoding = _transferEncoding;
            return sav;
        }
    }
}